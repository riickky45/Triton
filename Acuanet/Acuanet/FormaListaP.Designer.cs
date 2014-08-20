namespace Acuanet
{
    partial class FormaListaP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_listap = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listap)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_listap
            // 
            this.dgv_listap.AllowUserToAddRows = false;
            this.dgv_listap.AllowUserToDeleteRows = false;
            this.dgv_listap.AllowUserToOrderColumns = true;
            this.dgv_listap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listap.Location = new System.Drawing.Point(100, 91);
            this.dgv_listap.Name = "dgv_listap";
            this.dgv_listap.ReadOnly = true;
            this.dgv_listap.Size = new System.Drawing.Size(873, 313);
            this.dgv_listap.TabIndex = 0;
            // 
            // FormListaP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 432);
            this.Controls.Add(this.dgv_listap);
            this.Name = "FormListaP";
            this.Text = "FormListaP";
            this.Load += new System.EventHandler(this.frmListaP_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgv_listap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_listap;
    }
}