using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaOleada : Form
    {

        OleadaR oleada;


        public FormaOleada()
        {
            InitializeComponent();
            oleada = new OleadaR();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            oleada.iniciaCaptura();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            oleada.finalizaCaptura();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oleada.registraOleada(this.cb_categoriaO.SelectedIndex);
        }

        

    }
}
