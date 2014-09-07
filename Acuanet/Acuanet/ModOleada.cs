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

            string sql = "SELECT nombre FROM oleada ORDER BY nombre ";
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


        public bool crearOleada(string snombre_oleada, List<string> lista_cat)
        {


            string sql = "INSERT INTO  oleada (nombre) VALUES ('" + snombre_oleada + "')";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();


            foreach (string scat in lista_cat)
            {
                sql = "INSERT INTO  oleadacat (oleada,categoria) VALUES ('"+snombre_oleada+"','"+scat+"')";
             cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();

            }


            return true;
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
