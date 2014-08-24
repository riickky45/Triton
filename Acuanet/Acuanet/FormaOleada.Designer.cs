namespace Acuanet
{
    partial class FormaOleada
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cb_categoriaO = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_hora = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hora";
            // 
            // cb_categoriaO
            // 
            this.cb_categoriaO.FormattingEnabled = true;
            this.cb_categoriaO.Location = new System.Drawing.Point(12, 150);
            this.cb_categoriaO.Name = "cb_categoriaO";
            this.cb_categoriaO.Size = new System.Drawing.Size(121, 21);
            this.cb_categoriaO.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Categoria :";
            // 
            // lbl_hora
            // 
            this.lbl_hora.AutoSize = true;
            this.lbl_hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hora.Location = new System.Drawing.Point(61, 25);
            this.lbl_hora.Name = "lbl_hora";
            this.lbl_hora.Size = new System.Drawing.Size(70, 25);
            this.lbl_hora.TabIndex = 3;
            this.lbl_hora.Text = "Hora :";
            // 
            // FormaOleada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 212);
            this.Controls.Add(this.lbl_hora);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_categoriaO);
            this.Controls.Add(this.label1);
            this.Name = "FormaOleada";
            this.Text = "FormaOleada";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_categoriaO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_hora;
    }
}