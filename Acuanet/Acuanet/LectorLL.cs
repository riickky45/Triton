using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{
    class LectorLL
    {

 
        private LecConfigXML cxml = new LecConfigXML();
        MySqlConnection dbConn;

        public LectorLL(int id_oleada)
        {

            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

          
        }

        public DataTable obtenDatos()
        {

            string sql = "SELECT participante.nombre,participante.id_tag, fecha_hora,milis FROM participantes,tag WHERE participante.id_tag=tag.id_tag ORDER BY  fecha_hora DESC,milis DESC;";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            for (int i = 0; i < rdr.FieldCount; i++)
            {
                dt.Columns.Add(rdr.GetName(i));
            }

            dt.Load(rdr);

            rdr.Close();

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
