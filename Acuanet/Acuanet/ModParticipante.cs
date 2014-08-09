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


        public Participante recuperaP(int id)
        {
            Participante par = new Participante();

            //codigo que conecta y query para recuperar participante por id

            string sql = "SELECT nombre,id,numero FROM participante WHERE id=" + id;
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                par.nombre = rdr.GetString(0);
                par.id = System.Convert.ToInt32(rdr.GetString(1));
                par.snumero = rdr.GetString(2);

            }
            rdr.Close();

            return par;
        }


        public void crearP(Participante p)
        {
            //codigo que se conecta y query de insercion

            string sql = "INSERT INTO participante (nombre,numero) VALUES ('" + p.nombre + "','" + p.snumero + "')";
            System.Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();


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
            dbConn.Close();
            dbConn = null;
        }
    }
}
