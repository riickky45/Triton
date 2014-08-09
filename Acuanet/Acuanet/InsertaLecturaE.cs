using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

using CSL;

namespace Acuanet
{
    //esta clase inserta las Lecturas (en formato Lista) del Evento correspondiente
    class InsertaLecturaE
    {
        private List<Lectura> aLectura;
        private string strConexion;
        private int evento_id;

        //constructor
        public InsertaLecturaE(List<Lectura> aLectura,string strConexion,int evento_id)
        {
            this.aLectura = aLectura;
            this.strConexion=strConexion;
            this.evento_id=evento_id;
        }

        //metodo que inserta el array de lectura en la BD
        protected void insertaLecturaL()
        {
            

            MySqlConnection dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            foreach (Lectura d in aLectura)
            {               
                string sql = "INSERT INTO TTTTTT (evento_id,id_tag,fecha_hora,milis) VALUES (" + d.t.ToString() + ",'" + d.t.ApiTimeStampUTC + "')";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
            }
            dbConn.Close();
            dbConn = null;
        }

    }
}
