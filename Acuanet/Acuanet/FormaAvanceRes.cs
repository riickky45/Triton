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
    public partial class FormaAvanceRes : Form
    {

        GResultados gres = new GResultados();

        Object guiLock = new Object();

        public FormaAvanceRes()
        {
            InitializeComponent();

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;

        }


        delegate void actualizaPantalla_Delegate(string s);

        // metodo para actualizar la pantalla 
        private void actualizaPantalla(string s)
        {

            if (InvokeRequired)
            {
                actualizaPantalla_Delegate task = new actualizaPantalla_Delegate(actualizaPantalla);
                BeginInvoke(task, new object[] { s });
            }
            else
            {
                lock (guiLock)
                {
                    this.lbl_desct.Text = s;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
                backgroundWorker1.ReportProgress(i);
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.pgbarRes.Value = e.ProgressPercentage;
        }


        private void btn_inicio_Click(object sender, EventArgs e)
        {

            gres.obtenParDistxOleada();

            this.pgbarRes.Maximum = gres.tot_trabajo;
            this.pgbarRes.Step = 1;
            this.pgbarRes.Value = gres.rea_trabajo;

            backgroundWorker1.RunWorkerAsync();

        }
    }
}
