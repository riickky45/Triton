using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{
    //Esta clase lee las llegadas y genere un DataSet para informar de ellas 
    class LectorLL
    {


        private LecConfigXML cxml = new LecConfigXML();
        MySqlConnection dbConn;
        private int id_oleada;

        public LectorLL()
        {

            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();


        }

        public void ponOleada(int id)
        {
            this.id_oleada = id;
        }

        //Metodo que obtiene los datos en Formato DataSet
        public DataSet obtenDatos()
        {

            string sql = "SELECT participante.nombre,participante.id_tag, fecha_hora,milis as ms FROM participante,tags WHERE participante.id_tag=tags.id_tag ORDER BY  fecha_hora DESC,milis DESC LIMIT 20;";

            MySqlDataAdapter datad = new MySqlDataAdapter(sql, dbConn);
            DataSet dt = new DataSet();

            datad.Fill(dt);

            return dt;
        }

        ~LectorLL()
        {
            if (dbConn != null)
            {
                dbConn.Close();
            }
            dbConn = null;
        }

    }
}
