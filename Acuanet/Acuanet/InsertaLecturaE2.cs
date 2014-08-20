using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

using CSL;

namespace Acuanet
{
    class InsertaLecturaE2
    {
        private MySqlConnection dbConn;
        private TAG tag;
     


        public InsertaLecturaE2(MySqlConnection dbConn, TAG tag)
        {
            this.dbConn = dbConn;
            this.tag = tag;
           
        }

        //metodo que inserta un solo tag en la BD
        public void insertaTag()
        {

            string sql = "INSERT INTO pics (id_tag,fecha_hora,milis,rssi,frecuencia) VALUES ('" + tag.TagOrigId + "','" + tag.ApiTimeStampUTC + "'," + tag.ApiTimeStampUTC.Millisecond + "'"+tag.RSSI+"','"+tag.Frequency+"')";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
          
        }
    }
}
