using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormBienvenida : Form
    {
        public FormBienvenida()
        {
            
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 100;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(2);
            if (progressBar1.Value == 100)this.Close();
        }

      
    }
}

