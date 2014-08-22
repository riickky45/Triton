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
