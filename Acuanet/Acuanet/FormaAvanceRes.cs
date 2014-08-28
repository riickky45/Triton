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

               this.actualizaPantalla(gres.trabajo_accion);
               backgroundWorker1.ReportProgress(gres.trabajo_rea);

               gres.obtenLecturasPar();
               this.actualizaPantalla(gres.trabajo_accion);
               backgroundWorker1.ReportProgress(gres.trabajo_rea);

               gres.marcaLecturasBOrden();
               this.actualizaPantalla(gres.trabajo_accion);
               backgroundWorker1.ReportProgress(gres.trabajo_rea);

               gres.estimaTCTodos();
               this.actualizaPantalla(gres.trabajo_accion);
               backgroundWorker1.ReportProgress(gres.trabajo_rea);

               gres.escribeRes();
               this.actualizaPantalla(gres.trabajo_accion);
               backgroundWorker1.ReportProgress(gres.trabajo_rea);
            
        }

        //
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.pgbarRes.Value = e.ProgressPercentage;
        }

        //
        private void btn_inicio_Click(object sender, EventArgs e)
        {

            gres.obtenParDistxOleada();

            this.pgbarRes.Maximum = gres.trabajo_tot;
            this.pgbarRes.Step = 1;
            this.pgbarRes.Value = 0;

            backgroundWorker1.RunWorkerAsync();

        }

        private void btn_tabres_Click(object sender, EventArgs e)
        {
            FormaResultado FormaResul = new FormaResultado();
            FormaResul.Show();

        }
    }
}
