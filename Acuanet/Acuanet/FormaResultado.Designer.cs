namespace Acuanet
{
    partial class FormaResultado
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
            this.dgv_resultados = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnimpresion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_resultados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_resultados
            // 
            this.dgv_resultados.AllowUserToDeleteRows = false;
            this.dgv_resultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_resultados.Location = new System.Drawing.Point(-4, 143);
            this.dgv_resultados.Name = "dgv_resultados";
            this.dgv_resultados.ReadOnly = true;
            this.dgv_resultados.Size = new System.Drawing.Size(833, 461);
            this.dgv_resultados.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(522, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ordenar resultados por :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(495, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(275, 22);
            this.textBox1.TabIndex = 2;
            // 
            // btnimpresion
            // 
            this.btnimpresion.BackColor = System.Drawing.Color.Transparent;
            this.btnimpresion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnimpresion.FlatAppearance.BorderSize = 0;
            this.btnimpresion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnimpresion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnimpresion.Image = global::Acuanet.Properties.Resources.Print_32x32_32;
            this.btnimpresion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnimpresion.Location = new System.Drawing.Point(526, 79);
            this.btnimpresion.Name = "btnimpresion";
            this.btnimpresion.Size = new System.Drawing.Size(186, 43);
            this.btnimpresion.TabIndex = 3;
            this.btnimpresion.Text = "Imprimir Resultados";
            this.btnimpresion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnimpresion.UseVisualStyleBackColor = false;
            this.btnimpresion.Click += new System.EventHandler(this.btnimpresion_Click);
            // 
            // FormaResultado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Acuanet.Properties.Resources.descarga;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(827, 603);
            this.Controls.Add(this.btnimpresion);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_resultados);
            this.Name = "FormaResultado";
            this.Text = "FormaResultados";
            this.Load += new System.EventHandler(this.frmResultado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_resultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_resultados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnimpresion;
    }
}