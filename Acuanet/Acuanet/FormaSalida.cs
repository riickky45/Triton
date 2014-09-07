using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaSalida : Form
    {

        RegSalida salida;

        LecConfigXML cxml = new LecConfigXML();
        ModOleada modo;
        string strConexion;


        public FormaSalida()
        {
            InitializeComponent();
            salida = new RegSalida();

            strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
           + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
           + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
           + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            modo = new ModOleada(strConexion);
            this.cb_categoriaO.DataSource = modo.obtenDTC();

            this.cb_categoriaO.DisplayMember = "nombre";
            this.cb_categoriaO.ValueMember = "nombre";


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            salida.iniciaCaptura();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            salida.finalizaCaptura();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)this.cb_categoriaO.SelectedItem;
            salida.registraSalida(dr["nombre"].ToString());
        }

        

    }
}
