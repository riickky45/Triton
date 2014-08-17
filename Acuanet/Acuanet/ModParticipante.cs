using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Acuanet
{
    class ModParticipante
    {
        MySqlConnection dbConn;

        //crea la conexion y prepara para operaciones sobre los usuarios
        public ModParticipante(string strConexion)
        {                       
            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();
        }


        public Participante recuperaPxId(int id)
        {
            Participante par = new Participante();

            //codigo que conecta y query para recuperar participante por id

            string sql = "SELECT nombre,id,numero,id_tag FROM participante WHERE id=" + id;
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                par.nombre = rdr.GetString(0);
                par.id = System.Convert.ToInt32(rdr.GetString(1));
                par.snumero = rdr.GetString(2);
                par.id_tag=rdr.GetString(3);

            }
            rdr.Close();

            return par;
        }

        //metodo que recupera el participante por su tag
        public Participante recuperaPxTag(string stag_id)
        {
            Participante par = new Participante();

            //codigo que conecta y query para recuperar participante por id_tag

            string sql = "SELECT participante.nombre,participante.id,numero,pais,sexo,email,direccion,id_tag,prueba,club, categoria.nombre as categroria FROM participante,categoria WHERE id_tag='"+stag_id+"' AND participante.id_categoria=categoria.id_categoria ";


            //string sql = "SELECT participante.nombre,participante.id,numero FROM participante WHERE id_tag='" + stag_id + "'";
            
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                par.nombre = rdr.GetString(0);
                par.id = System.Convert.ToInt32(rdr.GetString(1));
                par.snumero = rdr.GetString(2);
              /*  par.pais=rdr.GetString(3);
                par.sexo=rdr.GetString(4);
                par.email=rdr.GetString(5);
                par.direcc=rdr.GetString(6);
                par.id_tag=rdr.GetString(7);
                par.prueba=rdr.GetString(8);
                par.sclub=rdr.GetString(9);
                par.categoria=rdr.GetString(10);*/

            }
            rdr.Close();

            return par;
        }


        //metodo que inserta - crea un participante en la BD
        public bool crearP(Participante p)
        {
            //codigo que se conecta y query de insercion

            if (p.nombre.Length == 0) return false;

            string sql = "INSERT INTO participante (nombre,numero,prueba,club,direccion,id_tag,pais,sexo,email) VALUES ('" + 
                p.nombre + "','" + p.snumero + "','" + p.prueba + "','" + p.sclub + "','" + p.direcc + "','" + p.id_tag + "','"+p.pais+"','"+p.sexo+"','"+p.email+"')";
            System.Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
          int res=  cmd.ExecuteNonQuery();
          return (res == 1) ? true : false;
        }


        public void ActualizaP(Participante p)
        {
            // codigo que actualiza un participante 

            string sql = "UPDATE participante SET nombre='" + p.nombre + "' WHERE id=" + p.id;
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();

        }


        // destructor libera la memoria y en este caso la conexión a la BD 
        ~ModParticipante()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }
    }
}
