using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class CreaOleada : Form
    {


        LecConfigXML cxml = new LecConfigXML();
        ModCategoria modo;
        string strConexion;

        public CreaOleada()
        {
            InitializeComponent();

              strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            modo = new ModCategoria(strConexion);
            listBox1.DataSource = modo.obtenDTCsinO();

            this.listBox1.DisplayMember = "nombre";
            this.listBox1.ValueMember = "nombre";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> lista_cat = new List<string>();

            foreach (Object selecteditem in listBox1.SelectedItems)
            {
                DataRowView dr = (DataRowView)selecteditem;
                lista_cat.Add(dr["nombre"].ToString());
            }

            ModOleada modol = new ModOleada(strConexion);

            modol.crearOleada(this.textBox1.Text, lista_cat);

        }
    }
}
