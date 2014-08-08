using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CSL;

namespace Acuanet
{
    public partial class FormaRegistro : Form
    {

        CS461_HL_API reader = new CS461_HL_API();
        

        public FormaRegistro()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //este metodo recupera los datos y los manda a insertar a la BD
        private void button3_Click(object sender, EventArgs e)
        {

            //se crea el particiapnete "en memoria"
            Participante par = new Participante();

            //se asignana los valores 
            par.nombre = text_nombre.Text;

            //se prepara la conexion a la BD
            ModParticipante modp = new ModParticipante();

            //se crea el particiapante
            modp.crearP(par);

        }
    }
}
