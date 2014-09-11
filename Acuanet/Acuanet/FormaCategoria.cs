using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    
    public partial class FormaCategoria : Form
    {
        LecConfigXML cxml = new LecConfigXML();
        ModCategoria modC;


        public FormaCategoria()
        {
            InitializeComponent();

            string strConexion = "server=" + cxml.Text("ACUANET/BD/SBD_ip", "127.0.0.1")
                + ";uid=" + cxml.Text("ACUANET/BD/SBD_usuario", "root")
                + ";pwd=" + cxml.Text("ACUANET/BD/SBD_passwd", "")
                + ";database=" + cxml.Text("ACUANET/BD/SBD_bdn", "ntritondb");
                modC = new ModCategoria(strConexion);
                this.dgv_categoria.DataSource = modC.obtenCategoriadgv().Tables[0].DefaultView;
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            Categoria Cat = new Categoria();

            Cat.nombre = txtb_nombre.Text;
            Cat.desc = text_descrip.Text;
            

            //se crea la categoria
            if (modC.creaCat(Cat))
            {
                MessageBox.Show("Se creo correctamente la Categoria", "Notificacioon", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se creo la Categoria", "Notificacioon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }
    }
}
