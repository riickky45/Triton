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

        //OleadaR2 oleada = new OleadaR2();
        //bool bprendAntenaO = true;

        public FormaPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(30000);
            this.Cursor = Cursors.Default;
            FormaRegistro miForm = new FormaRegistro();
            miForm.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void btn_pantalla_Click(object sender, EventArgs e)
        {
            FormaPLlegadas panLL = new FormaPLlegadas();
            panLL.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormaCSV miForm1 = new FormaCSV();
            miForm1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormaCategoria miForm4 = new FormaCategoria();
            miForm4.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(2500);
            this.Cursor = Cursors.Default;
            EscannerChip miFormEC = new EscannerChip();
            miFormEC.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void btnEAntena_Click(object sender, EventArgs e)
        {
           /* if (this.bprendAntenaO)
            {
                this.bprendAntenaO = false;


                this.btnEAntena.Text = "Encender Antena";
            }
            else
            {
                this.bprendAntenaO=true;
                this.btnEAntena.Text = "Apagar Antena";
            }*/
        }

        /*private void btnOleada_Click(object sender, EventArgs e)
        {

        }
         */
    }
}
