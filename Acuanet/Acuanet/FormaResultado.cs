using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaResultado : Form
    {
        private LecConfigXML cxml = new LecConfigXML();

        Object guiLock = new Object();

        ModResultado modr;
        int id_categoria;


        public FormaResultado()
        {
            id_categoria = 0;

            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");

            modr = new ModResultado(strConexion);

            InitializeComponent();
        }

        private void frmResultado_Load(object sender, EventArgs e)
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
                    this.dgv_resultados.DataSource = modr.obtenDatos(id_categoria).Tables[0].DefaultView;
                }
            }

        }

        private void btnimpresion_Click(object sender, EventArgs e)
        {
            GResultadosTablaHTML grt = new GResultadosTablaHTML(id_categoria);
            
        }
    }
}
