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
        //LectorLL gres_pre = new LectorLL();
        GResultados gres_pre = new GResultados();

        Object guiLock = new Object();

        public FormaPLlegadas()
        {
            InitializeComponent();
        }

        private void frmPL_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        delegate void actualizaGV_Delegate();

        private void actualizaGV()
        {
            if (InvokeRequired)
            {
                actualizaGV_Delegate task = new actualizaGV_Delegate(actualizaGV);
                BeginInvoke(task, new object[] { });
            }
            else
            {
                lock (guiLock)
                {
                    this.dgv_llegadas.DataSource = gres_pre.obtenResPreliminares().Tables[0].DefaultView;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            actualizaGV();
        }

        private void frmPL_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Enabled = false;
        }

        private void btn_revisar_Click(object sender, EventArgs e)
        {
            if (txt_numero.Text.Length == 0) return;
            gres_pre.ponNumero(System.Convert.ToInt32(this.txt_numero.Text));
            txt_numero.Text = "";
        }
    }
}
