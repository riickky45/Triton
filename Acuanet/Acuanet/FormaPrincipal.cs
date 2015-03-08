using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Acuanet
{
    public partial class FormaPrincipal : Form
    {

        
         

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
            FormaAvanceRes formaRes = new FormaAvanceRes();
            formaRes.Show();

        }

        private void btnEAntena_Click(object sender, EventArgs e)
        {
            CreaOleada btncreaOleda = new CreaOleada();
            btncreaOleda.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormaListaP flp = new FormaListaP();
            flp.Show();
        }

        private void btnOleada_Click(object sender, EventArgs e)
        {
           // salida.iniciaCaptura();
           
            FormaSalida formaO = new FormaSalida();
            formaO.Show();
        }

        private void llegadasm_Click(object sender, EventArgs e)
        {
            LlegadasManuales formaM = new LlegadasManuales();
             formaM.Show();
        }

        



       
    }
}
