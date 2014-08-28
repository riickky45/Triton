using System;
using System.Collections.Generic;
using System.Text;


using MySql.Data;
using MySql.Data.MySqlClient;



namespace Acuanet
{
    //Esta clase genera el HTML y lo manda al clipboard
    class GResultadosTablaHTML
    {
        MySqlConnection dbConn = null;
        LecConfigXML cxml = new LecConfigXML();

        private int id_categoria;

        //
        public GResultadosTablaHTML(int id_categoria)
        {
            this.id_categoria = id_categoria;

            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();

            this.generaHTML();
        }


        //
        private void generaHTML()
        {
            StringBuilder sbHTML = new StringBuilder();
            sbHTML.Append("<table>");

            sbHTML.Append("<tr>");
            sbHTML.Append("<th>Posición</th><th>Nombre (número)</th><th>Tiempo</th>");
            sbHTML.Append("</tr><tbody>");

            string sqc = "";
            if (id_categoria > 0)
            {
                sqc = " AND resultado_final.id_categoria=" + id_categoria;
            }

            string sql = "SELECT posicion,participante.nombre,participante.numero,tiempo FROM resultado,participante WHERE participante.id=resultado.id_participante " + sqc + " ORDER BY tiempo_meta";
           
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                sbHTML.Append("<tr>");
                sbHTML.Append(String.Format("<td align=\"center\">{0}</td><td>{1} ({2})</td><td align=\"right\">{3}</td>", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3)));
                sbHTML.Append("</tr>");
            }
            rdr.Close();

            sbHTML.Append("</tbody></table>");

            //envio al Clipboard
            ClipboardHelper.CopyToClipboard(sbHTML.ToString(), "Tabla de resultados: ");

        }


        //
        ~GResultadosTablaHTML()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }



    }
}
