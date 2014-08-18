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
        private int id_oleada;


        public InsertaLecturaE2(MySqlConnection dbConn, TAG tag, int id_oleada)
        {
            this.dbConn = dbConn;
            this.tag = tag;
            this.id_oleada = id_oleada;
        }

        //metodo que inserta un solo tag en la BD
        public void insertaTag()
        {

            string sql = "INSERT INTO pics (id_oleada,id_tag,fecha_hora,milis) VALUES (" + id_oleada + ",'" + tag.TagOrigId + "','" + tag.ApiTimeStampUTC + "'," + tag.ApiTimeStampUTC.Millisecond + ")";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.ExecuteNonQuery();

        }
    }
}
