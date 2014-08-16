using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
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

        //Obtiene el listado de las categorias en formato List
        public List<Categoria> obtenCategorias()
        {
            List<Categoria> lcat = new List<Categoria>();

            string sql = "SELECT nombre,id,descripcion FROM categoria ORDER BY nombre ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Categoria cat = new Categoria();
                cat.nombre = rdr.GetString(0);
                cat.id = System.Convert.ToInt32(rdr.GetString(1));
                cat.desc = rdr.GetString(2);

                lcat.Add(cat);

            }
            rdr.Close();

            return lcat;
        }

        //Obtiene una sola Categoria en objeto recuperado por su ID
        public Categoria obtenCategoriaxId(int id)
        {
            Categoria cat = new Categoria();

            string sql = "SELECT nombre,id,descripcion FROM categoria WHERE id='" + id + "'";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                cat.nombre = rdr.GetString(0);
                cat.id = System.Convert.ToInt32(rdr.GetString(1));
                cat.desc = rdr.GetString(2);

            }
            rdr.Close();

            return cat;
        }

        
        public void creaCategoria(Categoria c)
        {
            //codigo que se conecta y query de insercion

            string sql = "INSERT INTO categoria (nombre,desc) VALUES ('" + c.nombre + "','" + c.desc + "')";            
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();

        }


        public DataTable obtenDTC(){

            string sql = "SELECT nombre,id,descripcion FROM categoria ORDER BY nombre ";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Categoria cat = new Categoria();
                cat.nombre = rdr.GetString(0);
                cat.id = System.Convert.ToInt32(rdr.GetString(1));
                cat.desc = rdr.GetString(2);

            }
            rdr.Close();

            return null;
        }

        ~ModCategoria()
        {
            dbConn.Close();
            dbConn = null;
        }

    }
}
