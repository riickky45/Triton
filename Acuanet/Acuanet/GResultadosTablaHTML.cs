using System;
using System.Collections.Generic;
using System.Text;


using MySql.Data;
using MySql.Data.MySqlClient;



namespace Acuanet
{
    class GResultadosTablaHTML
    {
        MySqlConnection dbConn = null;
        LecConfigXML cxml = new LecConfigXML("config_oleada.xml");

        private int id_categoria;


        public GResultadosTablaHTML(int id_categoria)
        {

           string  strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
               + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
               + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
               + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

        }


        private void GeneraHTML()
        {
            StringBuilder sbHTML = new StringBuilder();






            ClipboardHelper.CopyToClipboard(sbHTML.ToString(), "Tabla de resultados: "); 

        }




          ~GResultadosTablaHTML()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }



    }
}
