using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

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
            sbHTML.Append("<table><tbody>");

            

            string sqc = "";
            if (id_categoria > 0)
            {
                sqc = " AND resultado_final.id_categoria=" + id_categoria;
            }

            string sql = "SELECT 1 as posicion, participante.nombre,participante.numero,participante.pais,tiempo,oleadacat.categoria,oleadacat.oleada FROM resultado,participante,salida,oleadacat WHERE participante.id=resultado.id_participante AND salida.oleada=oleadacat.oleada AND oleadacat.categoria=participante.categoria  ORDER BY salida.fecha_hora_ini_local,oleadacat.categoria,tiempo_meta";
            //MessageBox.Show(sql);
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int posicion = 0;

            string stexto_oleada = "";


            while (rdr.Read())
            {

                if (!stexto_oleada.Equals(rdr.GetString(5)))
                {
                    stexto_oleada = rdr.GetString(5);
                    sbHTML.Append("<tr>");
                    sbHTML.Append("<td colspan=\"5\"><h2>"+stexto_oleada+"</h2></td>");
                    sbHTML.Append("</tr>");
                    posicion = 1;

                    sbHTML.Append("<tr>");
                    sbHTML.Append("<th>Posición</th><th>Nombre (número)</th><th>Categoría</th><th>Pais</th><th>Tiempo</th>");
                    sbHTML.Append("</tr>");
                }


                sbHTML.Append("<tr>");
                sbHTML.Append(String.Format("<td align=\"center\">{0}</td><td>{1} ({2})</td><td align=\"center\">{3}</td><td align=\"right\">{4}</td></td><td align=\"right\">{5}</td>", posicion, rdr.GetString(1), rdr.GetString(2), rdr.GetString(5), rdr.GetString(3), rdr.GetString(4)));
                sbHTML.Append("</tr>");
                posicion++;
            }
            rdr.Close();

            sbHTML.Append("</tbody></table>");

            //envio al Clipboard
            ClipboardHelper.CopyToClipboard(sbHTML.ToString(), "Tabla de resultados: ");
            MessageBox.Show("Los Resultados se copiaron correctamente al portapapeles");
        }


        //
        ~GResultadosTablaHTML()
        {
            if (this.dbConn != null) dbConn.Close();
            dbConn = null;
        }



    }
}
