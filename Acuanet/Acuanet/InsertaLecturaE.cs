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
        
        private string strConexion;
   
        private TAG tag;

      

        //constructor
        public InsertaLecturaE(TAG tag, string strConexion)
        {
            this.tag = tag;          
            this.strConexion = strConexion;
        }


        //metodo que inserta el array de lectura en la BD
        #region Insertador de Buffer
        public void insertaLecturaL()
        {
           
            MySqlConnection dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            foreach (Lectura d in aLectura)
            {

                string sql = "INSERT INTO tags (id_tag,fecha_hora,milis,rssi,frecuencia) VALUES ('" + tag.TagOrigId + "','" + tag.ApiTimeStampUTC + "','" + tag.ApiTimeStampUTC.Millisecond + "','" + tag.RSSI + "','" + tag.Frequency + "')";
           
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
            }
            dbConn.Close();
            dbConn = null;
        }
        #endregion

        //metodo que inserta un solo tag en la BD
        public void insertaTag()
        {
            MySqlConnection dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            string sql = "INSERT INTO tags (id_tag,fecha_hora,milis,rssi) VALUES ('" + tag.TagOrigId + "','" + tag.ApiTimeStampUTC + "','" + tag.ApiTimeStampUTC.Millisecond + "','" + tag.RSSI + "')";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();

            dbConn.Close();
            dbConn = null;
        }

    }
}
