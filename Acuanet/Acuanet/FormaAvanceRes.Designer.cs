﻿namespace Acuanet
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
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_tabres = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgbarRes
            // 
            this.pgbarRes.Location = new System.Drawing.Point(50, 52);
            this.pgbarRes.Name = "pgbarRes";
            this.pgbarRes.Size = new System.Drawing.Size(377, 23);
            this.pgbarRes.TabIndex = 0;
            // 
            // lbl_desct
            // 
            this.lbl_desct.AutoSize = true;
            this.lbl_desct.Location = new System.Drawing.Point(50, 82);
            this.lbl_desct.Name = "lbl_desct";
            this.lbl_desct.Size = new System.Drawing.Size(35, 13);
            this.lbl_desct.TabIndex = 1;
            this.lbl_desct.Text = "label1";
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.Location = new System.Drawing.Point(50, 23);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(75, 23);
            this.btn_iniciar.TabIndex = 2;
            this.btn_iniciar.Text = "Iniciar";
            this.btn_iniciar.UseVisualStyleBackColor = true;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // btn_tabres
            // 
            this.btn_tabres.Location = new System.Drawing.Point(132, 23);
            this.btn_tabres.Name = "btn_tabres";
            this.btn_tabres.Size = new System.Drawing.Size(75, 23);
            this.btn_tabres.TabIndex = 3;
            this.btn_tabres.Text = "Resultados";
            this.btn_tabres.UseVisualStyleBackColor = true;
            // 
            // FormaAvanceRes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 128);
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