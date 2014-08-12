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
        private int id_oleada;
        private TAG t;

        //constructor
        public InsertaLecturaE(List<Lectura> aLectura,string strConexion,int evento_id)
        {
            this.aLectura = aLectura;
            this.strConexion=strConexion;
            this.id_oleada=evento_id;
        }

        //constructor
        public InsertaLecturaE(TAG t, int id_oleada, string strConexion)
        {
            this.t = t;
            this.id_oleada = id_oleada;
            this.strConexion = strConexion;
        }


        //metodo que inserta el array de lectura en la BD
        public void insertaLecturaL()
        {
           
            MySqlConnection dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            foreach (Lectura d in aLectura)
            {               
                string sql = "INSERT INTO pics (id_oleada,id_tag,fecha_hora,milis) VALUES (" + d.tag.ToString() + ",'" + d.tag.ApiTimeStampUTC + "')";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
            }
            dbConn.Close();
            dbConn = null;
        }


        //metodo que inserta un solo tag en la BD
        public void insertaTag()
        {
            MySqlConnection dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            string sql = "INSERT INTO pics (id_oleada,id_tag,fecha_hora,milis) VALUES (" +id_oleada+",'"+ t.TagOrigId + "','" + t.ApiTimeStampUTC + "',"+t.ApiTimeStampUTC.Millisecond+")";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();

            dbConn.Close();
            dbConn = null;
        }

    }
}
