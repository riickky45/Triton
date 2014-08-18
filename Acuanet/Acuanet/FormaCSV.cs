using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Acuanet
{
    public partial class FormaCSV : Form
    {
        DataTable UserInfoTable = new DataTable();
        public FormaCSV()
        {
            InitializeComponent();
        }
        private void loadUserInfo()
        {
            //Create data table
            UserInfoTable.Columns.Add("ID", typeof(String));
            UserInfoTable.Columns.Add("Nombre", typeof(String));
            UserInfoTable.Columns.Add("Numero", typeof(String));
            UserInfoTable.Columns.Add("Categoria", typeof(String));
            UserInfoTable.Columns.Add("Prueba", typeof(String));
            UserInfoTable.Columns.Add("Club", typeof(String));
            UserInfoTable.Columns.Add("Direcciòn", typeof(String));

            UserInfoTable.PrimaryKey = new DataColumn[] { UserInfoTable.Columns["ID"] };

            String line = null;
            string[] fields = null;
            try
            {
                StreamReader sr = new StreamReader(System.Environment.CurrentDirectory + "\\db.csv");
                sr.ReadLine();
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    fields = line.Split(',');

                    object[] o = new object[7];
                    for (int i = 0; i < 7; i++)
                    {
                        o[i] = fields[i].Trim();
                    }
                    UserInfoTable.LoadDataRow(o, LoadOption.OverwriteChanges);
                }
            }
            catch
            {
            }
        }
    }
}
