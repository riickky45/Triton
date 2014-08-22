using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{

    //Esta clase genera la tabla final de resultados 
    class GResultados
    {
        LecConfigXML cxml = new LecConfigXML();

        MySqlConnection dbConn=null;
        List<Resultado> lRes = new List<Resultado>();
        private int id_oleada;

        string strConexion;



        public GResultados(int id_oleada)
        {
            strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
               + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
               + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
               + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();
        }


        //método que obtiene a los participantes de cada oleada distintos, que previamente poseen un registro en la BD
        private int obtenParDistxOleada()
        {
            string sql = "SELECT DISTINCT participante.id FROM tags,participante WHERE participante.id_tag=tags.id_tag ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Resultado res = new Resultado();
                res.id_participante = System.Convert.ToInt32(rdr.GetString(0));

                lRes.Add(res);

            }
            rdr.Close();

            return lRes.Count;
        }


        //método que obtiene las lecturas cronologicas de cada competidor
        private void obtenLecturasxParId(Resultado r)
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
                auxl.dist = this.estimaDist(auxl.rssi);

                r.aLec.Add(auxl);
            }
            rdr.Close();
        }


        //método que obtiene la distancia estimada de acuerdo a la intensidad de la respuesta
        private double estimaDist(double rssi)
        {
            double d=0;


            return d;
        }


        //metodo que estima tiempo cruce de meta 
        private long estimaTC(Resultado r)
        {



            return 0;
        }

        private void EscribeRes()
        {

        }



         // destructor libera la memoria y en este caso la conexión a la BD 
        ~GResultados()
        {
            if(this.dbConn!=null)dbConn.Close();
            dbConn = null;
        }

    }
}
