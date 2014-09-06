using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Acuanet
{
    class ModOleada
    {

         MySqlConnection dbConn;

        public ModOleada(string strConexion)
        {
            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();
        }

        // Metodo que recupera las categoria en un DataTable (util para trabajar con ComboBox)
        public DataTable obtenDTC()
        {

            string sql = "SELECT nombre,id,descripcion FROM oleada ORDER BY nombre ";
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

        //destructor de oleada
        ~ModOleada()
        {
            if (dbConn != null)
            {
                dbConn.Close();
            }
            dbConn = null;
        }
    }
}
