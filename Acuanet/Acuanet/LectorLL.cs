using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Acuanet
{
    class LectorLL
    {


        private LecConfigXML cxml = new LecConfigXML();
        MySqlConnection dbConn;
        private int id_oleada;
        private int numero;

        public LectorLL()
        {

            this.numero = 0;
            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();


        }

        public void ponNumero(int numero)
        {
            this.numero = numero;
        }

        

        public void ponOleada(int id)
        {
            this.id_oleada = id;
        }

        //Metodo que obtiene los datos en Formato DataSet
        public DataSet obtenDatos()
        {

            string qc = "participante.numero>0";
            if (this.numero > 0)
            {
                qc = "participante.numero=" + this.numero;
            }

            string sql = "SELECT participante.numero,participante.nombre,categoria.nombre as categoria,participante.id_tag,fecha_hora,milis as ms FROM participante,tags,categoria WHERE categoria.nombre=participante.categoria AND participante.id_tag=tags.id_tag AND "+qc+" ORDER BY  fecha_hora DESC,milis DESC;";
           
            MySqlDataAdapter datad = new MySqlDataAdapter(sql, dbConn);
            DataSet dt = new DataSet();

            datad.Fill(dt);

            return dt;
        }

        ~LectorLL()
        {
            if (dbConn != null)
            {
                dbConn.Close();
            }
            dbConn = null;
        }

    }
}
