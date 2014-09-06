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


        public FormaSalida()
        {
            InitializeComponent();
            salida = new RegSalida();
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
            salida.registraSalida(this.cb_categoriaO.SelectedIndex);
        }

        

    }
}
