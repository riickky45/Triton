using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{
    class ModCategoria
    {
        MySqlConnection dbConn;

        public ModCategoria(string strConexion)
        {
            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();
        }


        public List<Categoria> obtenCategorias()
        {
            List<Categoria> lcat = new List<Categoria>();

            return lcat;
        }


        public Categoria obtenCategoriaxId(int id)
        {

        }



        ~ModCategoria()
        {
            dbConn.Close();
            dbConn = null;
        }

    }
}
