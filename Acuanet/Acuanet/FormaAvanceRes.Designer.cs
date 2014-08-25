namespace Acuanet
{
    partial class FormaAvanceRes
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
            this.pgbarRes = new System.Windows.Forms.ProgressBar();
            this.lbl_desct = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_tabres = new System.Windows.Forms.Button();
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgbarRes
            // 
            this.pgbarRes.Location = new System.Drawing.Point(50, 84);
            this.pgbarRes.Name = "pgbarRes";
            this.pgbarRes.Size = new System.Drawing.Size(377, 23);
            this.pgbarRes.TabIndex = 0;
            // 
            // lbl_desct
            // 
            this.lbl_desct.AutoSize = true;
            this.lbl_desct.BackColor = System.Drawing.Color.Transparent;
            this.lbl_desct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_desct.Location = new System.Drawing.Point(47, 128);
            this.lbl_desct.Name = "lbl_desct";
            this.lbl_desct.Size = new System.Drawing.Size(45, 16);
            this.lbl_desct.TabIndex = 1;
            this.lbl_desct.Text = "label1";
            // 
            // btn_tabres
            // 
            this.btn_tabres.BackColor = System.Drawing.Color.Transparent;
            this.btn_tabres.FlatAppearance.BorderSize = 0;
            this.btn_tabres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tabres.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tabres.Image = global::Acuanet.Properties.Resources.Clipboard_48x48_32;
            this.btn_tabres.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_tabres.Location = new System.Drawing.Point(290, 12);
            this.btn_tabres.Name = "btn_tabres";
            this.btn_tabres.Size = new System.Drawing.Size(137, 55);
            this.btn_tabres.TabIndex = 3;
            this.btn_tabres.Text = "Resultados";
            this.btn_tabres.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_tabres.UseVisualStyleBackColor = false;
            this.btn_tabres.Click += new System.EventHandler(this.btn_tabres_Click);
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.BackColor = System.Drawing.Color.Transparent;
            this.btn_iniciar.FlatAppearance.BorderSize = 0;
            this.btn_iniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_iniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_iniciar.Image = global::Acuanet.Properties.Resources.play_disabled_48x48_32;
            this.btn_iniciar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_iniciar.Location = new System.Drawing.Point(50, 12);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(113, 55);
            this.btn_iniciar.TabIndex = 2;
            this.btn_iniciar.Text = "Iniciar";
            this.btn_iniciar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_iniciar.UseVisualStyleBackColor = false;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // FormaAvanceRes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Acuanet.Properties.Resources.descarga;
            this.ClientSize = new System.Drawing.Size(503, 202);
            this.Controls.Add(this.btn_tabres);
            this.Controls.Add(this.btn_iniciar);
            this.Controls.Add(this.lbl_desct);
            this.Controls.Add(this.pgbarRes);
            this.Name = "FormaAvanceRes";
            this.Text = "Cálculo de Resultados";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbarRes;
        private System.Windows.Forms.Label lbl_desct;
        private System.Windows.Forms.Button btn_iniciar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_tabres;
    }
}