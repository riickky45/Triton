using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaPrincipal : Form
    {

        OleadaR oleada = new OleadaR();

        public FormaPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormaRegistro miForm = new FormaRegistro();
            miForm.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 miForm1 = new Form3();
            miForm1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 miForm4 = new Form4();
            miForm4.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EscannerChip miForm3 = new EscannerChip();
            miForm3.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void btnEAntena_Click(object sender, EventArgs e)
        {

        }

        private void btnOleada_Click(object sender, EventArgs e)
        {

        }
    }
}
