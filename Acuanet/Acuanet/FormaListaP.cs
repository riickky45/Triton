using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaListaP : Form
    {

        private LecConfigXML cxml = new LecConfigXML();
        ModParticipante modp;
        Object guiLock = new Object();
        LectorLL mll = new LectorLL();

        public FormaListaP()
        {
            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            modp = new ModParticipante(strConexion);
            
            InitializeComponent();
        }

        private void frmListaP_Load(object sender, EventArgs e)
        {
            actualizaGV();
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
                    this.dgv_listap.DataSource =  modp.obtenDatos().Tables[0].DefaultView;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
