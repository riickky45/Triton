using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaPLlegadas : Form
    {
        LectorLL mll = new LectorLL();
        public FormaPLlegadas()
        {
            InitializeComponent();
        }


        private void actualizaGV()
        {
            lock (this)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
