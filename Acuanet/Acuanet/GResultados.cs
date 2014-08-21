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
        List<Resultado> lres = new List<Resultado>();
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


        //metodo que obtiene a los participantes de cada oleada distintos, que previamente poseen un registro en la BD
        private void obtenParDistxOleada()
        {

            string sql = "SELECT DISTINCT participante.id FROM tags,participante WHERE participante.id_tag=tags.id_tag ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Resultado res = new Resultado();
                res.id_participante = System.Convert.ToInt32(rdr.GetString(0));

                lres.Add(res);

            }
            rdr.Close();


        }


        private void obtenLecturasxParId(int id)
        {
            string sql = "SELECT rssi,fecha_hora,milis FROM tags,participante WHERE participante.id_tag=tags.id_tag AND participanete_id="+id;

        }


         // destructor libera la memoria y en este caso la conexión a la BD 
        ~GResultados()
        {
            if(this.dbConn!=null)dbConn.Close();
            dbConn = null;
        }

    }
}
