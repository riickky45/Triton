using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class LlegadasManuales : Form
    {
        MySqlConnection dbConn = null;
        LecConfigXML cxml = new LecConfigXML();
        
        
        public LlegadasManuales()
        {
            InitializeComponent();

            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
              + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
              + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
              + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            dbConn = new MySqlConnection(strConexion);
            dbConn.Open();
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            DateTime dt_llegada = DateTime.Now;
            string squery = "SELECT UNIX_TIMESTAMP(fecha_hora_ini_local) as tiempo_ini_local,milis_ini_local,participante.id as id_participante FROM salida,participante,oleadacat WHERE participante.categoria=oleadacat.categoria AND oleadacat.oleada=salida.oleada AND participante.numero='" + this.tb_numero.Text + "'";
            //MessageBox.Show(squery);
            MySqlCommand cmd = new MySqlCommand(squery, dbConn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            long tiempo_ini_local = System.Convert.ToInt64(rdr.GetString(0));
            int milis_ini_local = System.Convert.ToInt16(rdr.GetString(1));

            int id_participante=System.Convert.ToInt16(rdr.GetString(2));

            rdr.Close();


            long tiempo_fin_local = ToUnixTimestamp(dt_llegada);

            double d_tiempo_ini = tiempo_ini_local + (double)milis_ini_local / 1000;
            double d_tiempo_fin = tiempo_fin_local + (double)dt_llegada.Millisecond / 1000;


            string stiempo = GResultados.ConvierteUTS2String(d_tiempo_fin - d_tiempo_ini);


            string  sfecha_hora_fin = dt_llegada.ToString("yyyy-MM-dd HH:mm:ss") + "." + Math.Round(dt_llegada.Millisecond / 10.0, 0);

            DateTime dt_inicio = GResultados.UnixTimeStampToDateTime(tiempo_ini_local);
            string sfecha_hora_ini = dt_inicio.ToString("yyyy-MM-dd HH:mm:ss") + "." + Math.Round(dt_inicio.Millisecond / 10.0, 0);

            string squery2 = "INSERT INTO resultado (id_participante,tiempo,fecha_hora_ini,fecha_hora_fin,milis_ini,milis_fin,tiempo_meta,tiempo_ini) VALUES (" + id_participante + ",'" + stiempo + "','" + sfecha_hora_ini + "','" + sfecha_hora_fin + "'," + milis_ini_local + "," + dt_llegada.Millisecond + "," + d_tiempo_fin + ","+d_tiempo_ini+")";
           // MessageBox.Show(squery2);
            cmd = new MySqlCommand(squery2, dbConn);
            cmd.ExecuteNonQuery();

            this.tb_numero.Text = "";
        }


        private long ToUnixTimestamp(DateTime target)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            long unixTimestamp = System.Convert.ToInt64((target - date).TotalSeconds);

            return unixTimestamp;
        }
    }
}
