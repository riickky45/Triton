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

        MySqlConnection dbConn=null;
        List<Resultado> lRes = new List<Resultado>();
       

        private string strConexion;

        //para metros del modelo
        private double A;
        private int n;
        private double h;
        private bool bcomp;


        //constructor
        public GResultados(int id_oleada)
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
        }


        //método que obtiene a los participantes de cada oleada distintos, que previamente poseen un registro en la BD
        private int obtenParDistxOleada()
        {
            string sql = "SELECT DISTINCT participante.id,id_categoria,id_tag FROM tags,participante WHERE participante.id_tag=tags.id_tag ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Resultado res = new Resultado();
                res.id_participante = System.Convert.ToInt32(rdr.GetString(0));
                res.id_categoria = System.Convert.ToInt32(rdr.GetString(1));
                res.id_tag = rdr.GetString(2);

                lRes.Add(res);

            }
            rdr.Close();

            return lRes.Count;
        }


        //método que obtiene las lecturas cronologicas de cada competidor
        private void obtenLecturasPar(Resultado r)
        {
            string sql = "SELECT rssi,UNIX_TIMESTAMP(fecha_hora) as tiempo,milis FROM tags,participante WHERE participante.id_tag=tags.id_tag AND participanete_id=" + r.id_participante+" ORDER BY fecha_hora,milis";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Lectura auxl = new Lectura();
                auxl.rssi = System.Convert.ToDouble(rdr.GetString(0));
                auxl.tiempo = System.Convert.ToInt64(rdr.GetString(1));
                auxl.milis = System.Convert.ToInt32(rdr.GetString(2));

                //tiempo en segundos incluyendo los milisegundos
                auxl.tms = auxl.tiempo + auxl.milis / 1000.00;

                //estimación de la distancia por la intensidad de la señal de respuesta
                if (this.bcomp)
                {
                    auxl.d_dist = estimaDistCA(auxl.rssi);
                }
                else
                {
                    auxl.d_dist = estimaDist(auxl.rssi);
                }

                // se calcula la distnacia horizontal a la meta
                auxl.a_dist = Math.Sqrt(auxl.d_dist * auxl.d_dist - h * h);

                r.aLec.Add(auxl);
            }
            rdr.Close();

            this.CalculaMax();
        }


        //método que obtiene la distancia estimada de acuerdo a la intensidad de la respuesta
        private double estimaDist(double rssi)
        {            
            return Math.Pow(10,(A-rssi)/(10*n));
        }


        //metodo que estima la distancia compensando desalineacion angular debido a la altura en la que se encuentra la antena
        private double estimaDistCA(double rssi)
        {
            return Math.Pow(10,(20*Math.Log10(h)+A-rssi)/(10*n+20));
        }


        //metodo que estima tiempo cruce de meta (promedio de todos los tiempos, con buen orden y sin nodos desordenados)
        private void estimaTCM(Resultado r)
        {
            int numlec = r.aLec.Count-1;
            double res = 0;

            for (int i = 0; i < numlec; i++)
            {
                Lectura leci = r.aLec[i];
                if (leci.bdatoc == true)
                {                                  
                        Lectura lecj = r.aLec[i+1];
                        if (lecj.bdatoc == true)
                        {
                            res +=lecj.tms+ Math.Abs((lecj.tms - leci.tms) * lecj.a_dist / (lecj.a_dist - leci.a_dist));
                        }                    
                }
            }

            r.tc_meta=(decimal) res/numlec;
        }


        //metodo que escribe resultados en la BD
        private void escribeRes()
        {
            foreach (Resultado r in lRes)
            {
                string sql = "INSERT INTO resultado (id_participante,tiempo_meta) VALUES (" + r.id_participante + ",'" + r.tc_meta + "')";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
            }

        }


        public void borraResP()
        {

        }


        //metodo que determina el valor maximo de rssi y el tiempo y la minima distancia puede ser no utilizado
        private void CalculaMax() {

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
        public void realizaCalculos()
        {
            this.obtenParDistxOleada();
            foreach (Resultado r in lRes)
            {
                this.obtenLecturasPar(r);
                this.marcaLecturasBOrden(r);
                if (r.cantidad_aLec == 1)
                {
                    this.estimaTCM2(r);
                }
                else
                {
                    this.estimaTCM(r);
                }
            }

            this.escribeRes();
        }


         // destructor libera la memoria y en este caso la conexión a la BD 
        ~GResultados()
        {
            if(this.dbConn!=null)dbConn.Close();
            dbConn = null;
        }

    }
}
