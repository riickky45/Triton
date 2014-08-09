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
        private string strConnect;
        private int evento_id;

        public InsertaLecturaE(List<Lectura> aLectura,string strConnect,int evento_id)
        {
            this.aLectura = aLectura;
            this.strConnect=strConnect;
            this.evento_id=evento_id;
        }

        //metodo que inserta el array de lectura en la BD
        protected void insertaLecturaL()
        {

            //string strConnect = "server=127.0.0.1;uid=root;pwd=XXXXXXX;database=YYYYYY";

            MySqlConnection dbConn = new MySqlConnection(strConnect);
            dbConn.Open();

            foreach (Lectura d in aLectura)
            {
                string sql = "INSERT INTO TTTTTT (valor,id_tag) VALUES (" + d.t.ToString() + ",'" + d.t + "')";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();
            }
            dbConn.Close();
            dbConn = null;
        }

    }
}
