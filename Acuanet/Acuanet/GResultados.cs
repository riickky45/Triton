using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{

    //Esta clase genera la tabla final de resultados realiza todos los calculos necesarios 
    public partial class GResultados
    {
        LecConfigXML cxml = new LecConfigXML("config_oleada.xml");

        MySqlConnection dbConn = null;
        List<Resultado> lRes = new List<Resultado>();


        private string strConexion;

        //para metros del modelo
        private double A;
        private int n;
        private double h;
        private bool bcomp;

        //parametros para calcular el avance relativo e informar a la interfaz sobre su avance
        public string trabajo_accion;
        public int trabajo_tot;
        public int trabajo_tot_factor = 5;
        public int trabajo_rea;


        //constructor
        public GResultados()
        {
            strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
               + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
               + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
               + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");


            A = cxml.Double("ACUANET/MODELO/A", -69.190);
            n = cxml.Int16("ACUANET/MODELO/n", 2);
            h = cxml.Int16("ACUANET/MODELO/h", 3);
            bcomp = cxml.Boolean("ACUANET/MODELO/compensado", false);

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            //configuracion para reportar avnace del trabajo
            this.trabajo_accion = "Conexión a BD";
            this.trabajo_tot = 0;
            this.trabajo_rea = 0;
        }


        //método que obtiene a los participantes de cada oleada distintos, que previamente poseen un registro en la BD
        public int obtenParDistxOleada()
        {
            this.trabajo_accion = "Obtenemos participantes";

            string sql = "SELECT DISTINCT participante.id,id_categoria,participante.id_tag FROM tags,participante WHERE participante.id_tag=tags.id_tag ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Resultado res = new Resultado();
                res.id_participante = System.Convert.ToInt32(rdr.GetString(0));
                res.id_categoria = System.Convert.ToInt32(rdr.GetString(1));
                res.id_tag = rdr.GetString(2);

                res.aLec = new List<Lectura>();

                lRes.Add(res);
                this.trabajo_rea++;
            }
            rdr.Close();
            this.trabajo_tot = this.trabajo_tot_factor * lRes.Count;
            return lRes.Count;
        }


        //método que obtiene las lecturas cronologicas de cada competidor
        public void obtenLecturasPar()
        {

            foreach (Resultado r in lRes)
            {

                string sql = "SELECT rssi,UNIX_TIMESTAMP(fecha_hora) as tiempo,milis FROM tags,participante WHERE participante.id_tag=tags.id_tag AND participante.id=" + r.id_participante + " ORDER BY fecha_hora,milis";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Lectura auxlec = new Lectura();
                    auxlec.rssi = System.Convert.ToDouble(rdr.GetString(0));
                    auxlec.tiempo = System.Convert.ToInt64(rdr.GetString(1));
                    auxlec.milis = System.Convert.ToInt32(rdr.GetString(2));

                    //tiempo en segundos incluyendo los milisegundos
                    auxlec.tms = auxlec.tiempo + auxlec.milis / 1000.00;

                    //estimación de la distancia por la intensidad de la señal de respuesta
                    if (this.bcomp)
                    {
                        auxlec.d_dist = estimaDistCA(auxlec.rssi);
                    }
                    else
                    {
                        auxlec.d_dist = estimaDist(auxlec.rssi);
                    }

                    // se calcula la distancia horizontal a la meta
                    auxlec.a_dist = Math.Sqrt(auxlec.d_dist * auxlec.d_dist - h * h);

                   

                    r.aLec.Add(auxlec);
                }
                rdr.Close();

                //this.CalculaMax();
                this.trabajo_rea++;
            }
        }


        //método que obtiene la distancia estimada de acuerdo a la intensidad de la respuesta
        private double estimaDist(double rssi)
        {
            return Math.Pow(10, (A - rssi) / (10 * n));
        }


        //metodo que estima la distancia compensando desalineacion angular debido a la altura en la que se encuentra la antena
        private double estimaDistCA(double rssi)
        {
            return Math.Pow(10, (20 * Math.Log10(h) + A - rssi) / (10 * n + 20));
        }


        //metodo que estima tiempo cruce de meta (promedio de todos los tiempos, con buen orden y sin nodos desordenados)
        private void estimaTCM(Resultado r)
        {
            int numlec = r.aLec.Count - 1;
            double res = 0;

            for (int i = 0; i < numlec; i++)
            {
                Lectura leci = r.aLec[i];
                if (leci.bdatoc == true)
                {
                    Lectura lecj = r.aLec[i + 1];
                    if (lecj.bdatoc == true)
                    {
                        res += lecj.tms + Math.Abs((lecj.tms - leci.tms) * lecj.a_dist / (lecj.a_dist - leci.a_dist));
                    }
                }
            }

            r.tc_meta = (decimal)res / (decimal)numlec;
            this.trabajo_rea++;
        }


        //metodo que escribe resultados en la BD
        public void escribeRes()
        {
            this.trabajo_accion = "Escribiendo en BD resultados";
            foreach (Resultado r in lRes)
            {
                string sql = "INSERT INTO resultado (id_participante,tiempo_meta) VALUES (" + r.id_participante + ",'" + r.tc_meta + "')";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
                this.trabajo_rea++;
            }

        }


        public void borraResP()
        {
            string sql = "DELETE FROM resultado;";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
        }


        //metodo que determina el valor maximo de rssi y el tiempo y la minima distancia puede ser no utilizado
        private void CalculaMax()
        {

            foreach (Resultado r in this.lRes)
            {
                bool bpv = true;
                double rssi_max = 0;
                double tms_max = 0;
                foreach (Lectura lec in r.aLec)
                {
                    if (bpv)
                    {
                        rssi_max = lec.rssi;
                        tms_max = lec.tms;
                        bpv = false;
                    }
                    else
                    {
                        if (rssi_max < lec.rssi)
                        {
                            rssi_max = lec.rssi;
                            tms_max = lec.tms;
                        }
                    }
                }

                r.rssi_max = rssi_max;
                r.tms_max = tms_max;
                r.d_min = estimaDist(r.rssi_max);
            }

        }


        //metodo principal que realiza todos los calculos para cada categoria
        public void estimaTCTodos()
        {
            this.trabajo_accion = "Calculando TCM";
            foreach (Resultado r in lRes)
            {                                
                if (r.cantidad_aLec > 1)
                    this.estimaTCM(r);
                else
                    this.estimaTCM2(r);
            }

        }


        // destructor libera la memoria y en este caso la conexión a la BD 
        ~GResultados()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }

    }
}
