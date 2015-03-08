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
            this.button1 = new System.Windows.Forms.Button();
            this.txt_nombreP = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_listap
            // 
            this.dgv_listap.AllowUserToAddRows = false;
            this.dgv_listap.AllowUserToDeleteRows = false;
            this.dgv_listap.AllowUserToOrderColumns = true;
            this.dgv_listap.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgv_listap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_listap.Location = new System.Drawing.Point(14, 155);
            this.dgv_listap.Name = "dgv_listap";
            this.dgv_listap.ReadOnly = true;
            this.dgv_listap.Size = new System.Drawing.Size(853, 307);
            this.dgv_listap.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(578, -14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 58);
            this.button1.TabIndex = 1;
            this.button1.Text = "Buscar Participante :";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_nombreP
            // 
            this.txt_nombreP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombreP.Location = new System.Drawing.Point(536, 28);
            this.txt_nombreP.Name = "txt_nombreP";
            this.txt_nombreP.Size = new System.Drawing.Size(288, 26);
            this.txt_nombreP.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Acuanet.Properties.Resources.lista;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(507, 144);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::Acuanet.Properties.Resources.magnifier;
            this.button2.Location = new System.Drawing.Point(831, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 42);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormaListaP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Acuanet.Properties.Resources.descarga;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(879, 494);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_nombreP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv_listap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormaListaP";
            this.Text = "Lista de Participantes";
            this.Load += new System.EventHandler(this.frmListaP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_listap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_listap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_nombreP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
    }
}