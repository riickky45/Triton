using System;
using System.Collections.Generic;
using System.Text;
//using System.Windows.Forms;
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

            string sql = "INSERT INTO tags (id_tag,fecha_hora,milis,rssi,frecuencia) VALUES ('" + tag.TagOrigId + "','" + tag.ApiTimeStampUTC + "','" + tag.ApiTimeStampUTC.Millisecond + "','"+tag.RSSI+"','"+tag.Frequency+"')";
            //MessageBox.Show(sql);
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
        }
    }
}
