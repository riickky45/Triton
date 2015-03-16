using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{

    //Esta clase genera la tabla final de resultados realiza todos los calculos necesarios 
    public partial class GResultados
    {
        LecConfigXML cxml = new LecConfigXML("config_oleada.xml");

        MySqlConnection dbConn = null;
        List<Resultado> lRes ;


        private string strConexion;

        //para metros del modelo
        private double A;
        private double n;
        private double h;
        private bool bcomp;

        //parametros para calcular el avance relativo e informar a la interfaz sobre su avance
        public string trabajo_accion;
        public int trabajo_tot;
        public int trabajo_tot_factor = 5;
        public int trabajo_rea;

        private int numero_filtro;


        //constructor
        public GResultados()
        {
            strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
               + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
               + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
               + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");


            A = cxml.Double("ACUANET/MODELO/A", -69.190);
            n = cxml.Double("ACUANET/MODELO/n", 2);
            h = cxml.Int16("ACUANET/MODELO/h", 3);
            bcomp = cxml.Boolean("ACUANET/MODELO/compensado", false);

            // MessageBox.Show("bcomp: " + bcomp+" A: "+A+" h: "+h+" n: "+n);

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            //configuracion para reportar avance del trabajo
            this.trabajo_accion = "Conexión a BD";
            this.trabajo_tot = 0;
            this.trabajo_rea = 0;
        }


        //método que obtiene a los participantes de cada salida distintos, 
        //que previamente poseen un registro en la BD
        public int obtenParDistxOleada()
        {

            this.trabajo_accion = "Obtenemos participantes";
            lRes = new List<Resultado>();
            string sql = "SELECT DISTINCT participante.id,participante.categoria,participante.id_tag,UNIX_TIMESTAMP(fecha_hora_ini_local) as tiempo_ini_local,milis_ini_local,oleadacat.oleada,participante.numero,participante.nombre FROM tags,participante,oleadacat,salida WHERE salida.oleada=oleadacat.oleada AND oleadacat.categoria=participante.categoria AND participante.id_tag=tags.id_tag ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Resultado res = new Resultado();
                res.id_participante = System.Convert.ToInt32(rdr.GetString(0));
                res.categoria = rdr.GetString(1);
                res.id_tag = rdr.GetString(2);

                res.tiempo_ini_local = System.Convert.ToInt64(rdr.GetString(3));
                res.milis_ini_local = System.Convert.ToInt16(rdr.GetString(4));
                res.oleada = rdr.GetString(5);
                res.snumero = rdr.GetString(6);
                res.nombre = rdr.GetString(7);


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

                string sql = "SELECT rssi,UNIX_TIMESTAMP(fecha_hora) as tiempo,milis,UNIX_TIMESTAMP(fecha_hora_ini_local) as tiempo_ini_local,milis_ini_local  FROM tags,participante,salida,oleadacat WHERE salida.oleada=oleadacat.oleada AND oleadacat.categoria=participante.categoria AND participante.id_tag=tags.id_tag AND participante.id=" + r.id_participante + " ORDER BY fecha_hora,milis";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Lectura auxlec = new Lectura();
                    auxlec.rssi = System.Convert.ToDouble(rdr.GetString(0));
                    auxlec.tiempo = System.Convert.ToInt64(rdr.GetString(1));
                    auxlec.milis = System.Convert.ToInt32(rdr.GetString(2));

                    //tiempo origen de la antena
                    double dtori = System.Convert.ToInt64(rdr.GetString(3)) + (System.Convert.ToInt32(rdr.GetString(4)) / 1000.00);


                    //tiempo en segundos incluyendo los milisegundos relativo al momento de inicio de la antena
                    auxlec.tms = auxlec.tiempo + (auxlec.milis / 1000.00) - dtori;

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

                // MessageBox.Show(r.id_participante + " " + r.aLec.Count);

                //this.CalculaMax();
                this.trabajo_rea++;
            }
        }


        //método que obtiene la distancia estimada de acuerdo a la intensidad de la respuesta
        private double estimaDist(double rssi)
        {

            return Math.Pow(10, (A - rssi) / (10 * n))/100.00;

        }


        //metodo que estima la distancia compensando desalineacion angular debido a la altura en la que se encuentra la antena
        private double estimaDistCA(double rssi)
        {
            return Math.Pow(10, (20 * Math.Log10(h) + A - rssi) / (10 * n + 20));
        }


        //método que estima tiempo cruce de meta (promedio de todos los tiempos, con buen orden y sin nodos desordenados)
        private void estimaTCM(Resultado r)
        {
            int numlec = r.aLec.Count - 1;
            int numlec_efe = 0;
            double res = 0;

            for (int i = 0; i < numlec; i++)
            {
                Lectura leci = r.aLec[i];
                if (leci.bdatoc == true)
                {
                    Lectura lecj = r.aLec[i + 1];
                    if (lecj.bdatoc == true && lecj.a_dist != leci.a_dist)
                    {
                        res += lecj.tms + Math.Abs((lecj.tms - leci.tms) * lecj.a_dist / (lecj.a_dist - leci.a_dist));
                        numlec_efe++;
                    }
                }
            }

            if (numlec_efe > 0)
            {
                r.tc_meta = res / numlec_efe;
            }
            else
            {
                r.tc_meta = 0;
            }
               
            r.tc_meta_local = r.tc_meta + r.tiempo_ini_local + r.milis_ini_local / 1000;
            
            this.trabajo_rea++;
        }


        //metodo que escribe resultados en la BD
        public void escribeRes()
        {
            this.trabajo_accion = "Escribiendo en BD resultados";
            foreach (Resultado r in lRes)
            {
                
                this.calculoTiemposFinales(r);


                string sql = "INSERT INTO resultado (id_participante,tiempo_meta,fecha_hora_ini,milis_ini,fecha_hora_fin,milis_fin,tiempo,tiempo_ini) VALUES ("
                    + r.id_participante + ",'" + r.tc_meta_local + "','" + r.sfecha_hora_ini + "'," + r.milis_ini_local + ",'" + r.sfecha_hora_fin + "'," + r.milis_fin + ",'" + r.stiempo + "'," + r.ts_ini + ")";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
                this.trabajo_rea++;
            }

        }

        //metodo que calcula todos los tiempos involucrados y los prepara para su insercion
        private void calculoTiemposFinales(Resultado r)
        {
            
            r.dt_ini = GResultados.UnixTimeStampToDateTime(r.tiempo_ini_local);

            r.dt_fin = GResultados.UnixTimeStampToDateTime(Math.Truncate(r.tc_meta_local));

            r.ts_ini = (r.tiempo_ini_local + (double)r.milis_ini_local / 1000.00);


            r.sfecha_hora_ini = r.dt_ini.ToString("yyyy-MM-dd HH:mm:ss") + "." + Math.Round(r.milis_ini_local / 10.0, 0);


            r.milis_fin = (int)Math.Truncate((r.tc_meta_local - Math.Truncate(r.tc_meta_local)) * 1000);

            r.sfecha_hora_fin = r.dt_fin.ToString("yyyy-MM-dd HH:mm:ss") + "." + Math.Round(r.milis_fin / 10.0, 0);


            r.stiempo = GResultados.ConvierteUTS2String(r.tc_meta_local - r.ts_ini);

        }

        //método que borra los anteriores calculos 
        public void borraResP()
        {
            string sql = "DELETE FROM resultado;";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
        }


        //método principal que realiza todos los calculos para cada categoria
        public void estimaTCTodos()
        {
            this.trabajo_accion = "Calculando TCM";
            foreach (Resultado r in lRes)
            {
                if (r.cantidad_aLec > 1)
                    this.estimaTCM(r);
                else
                {
                 
                    //if(lRes.Count>1)this.estimaTCM2(r);
                    this.estimaTCM2(r);
                }
            }

        }

        //método estatico que permite obtener el tiempo Unix Time a DateTime
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        //método que convierte una diferencia timestamp en horas:minutos:segundos.centesimas(redondeadas al entero mas cercano)
        public static string ConvierteUTS2String(double unixTimeStamp)
        {

            StringBuilder sb = new StringBuilder();


            double parte_dec = unixTimeStamp - Math.Truncate(unixTimeStamp);
            double parte_ent = Math.Truncate(unixTimeStamp);

            double horas = Math.Truncate(parte_ent / 3600);

            double parte_min_seg = parte_ent - 3600 * horas;

            double minutos = Math.Truncate(parte_min_seg / 60);

            double segundos = parte_min_seg - 60 * minutos;


            sb.Append(String.Format("{0}:{1:00}:{2:00}.{3:00}", horas, minutos, segundos, Math.Round(parte_dec * 100)));

            return sb.ToString();
        }

        //metodo que calcula dinamicamente con resultados preliminares toda la algoritmia involucrada para estimar resultado,
        //reporta 1 solo dato por participante y aproxima consistentemente el resultado
        public DataSet obtenResPreliminares()
        {

            this.obtenParDistxOleada();
            if (lRes.Count == 0) return null;
            this.obtenLecturasPar();
           int clBO=this.marcaLecturasBOrden();

           if (clBO <= 2) return null;

            this.estimaTCTodos();

            DataTable dtable = new DataTable("resultados_preliminares");

            dtable.Columns.Add("Número");
            dtable.Columns.Add("Nombre");
            dtable.Columns.Add("Categoría");
            dtable.Columns.Add("Tag");
            dtable.Columns.Add("Tiempo ECM");

            

            foreach (Resultado r in this.lRes)
            {
                if (numero_filtro > 0)
                {
                    if (numero_filtro == System.Convert.ToInt64(r.snumero))
                    {
                        this.calculoTiemposFinales(r);
                        dtable.Rows.Add(r.snumero, r.nombre, r.categoria, r.id_tag, r.stiempo);
                    }
                }
                else
                {
                    this.calculoTiemposFinales(r);
                    dtable.Rows.Add(r.snumero, r.nombre, r.categoria, r.id_tag, r.stiempo);
                }

            }


            DataSet dtset = new DataSet();
            dtset.Tables.Add(dtable);

            lRes.Clear();
            lRes = null;

            return dtset;
        }


        public void ponNumero(int num)
        {
            this.numero_filtro = num;
        }

        //destructor libera la memoria y en este caso la conexión a la BD 
        ~GResultados()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }

    }
}
