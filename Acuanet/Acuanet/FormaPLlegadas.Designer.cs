namespace Acuanet
{
    partial class FormaPLlegadas
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
            this.dgv_llegadas = new System.Windows.Forms.DataGridView();
            this.gbox_estatus = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_revisar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_llegadas)).BeginInit();
            this.gbox_estatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgv_llegadas
            // 
            this.dgv_llegadas.AllowUserToAddRows = false;
            this.dgv_llegadas.AllowUserToDeleteRows = false;
            this.dgv_llegadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_llegadas.Location = new System.Drawing.Point(12, 173);
            this.dgv_llegadas.Name = "dgv_llegadas";
            this.dgv_llegadas.ReadOnly = true;
            this.dgv_llegadas.Size = new System.Drawing.Size(1094, 315);
            this.dgv_llegadas.TabIndex = 0;
            // 
            // gbox_estatus
            // 
            this.gbox_estatus.BackColor = System.Drawing.Color.Transparent;
            this.gbox_estatus.Controls.Add(this.textBox1);
            this.gbox_estatus.Controls.Add(this.btn_revisar);
            this.gbox_estatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbox_estatus.Location = new System.Drawing.Point(987, 22);
            this.gbox_estatus.Name = "gbox_estatus";
            this.gbox_estatus.Size = new System.Drawing.Size(205, 111);
            this.gbox_estatus.TabIndex = 1;
            this.gbox_estatus.TabStop = false;
            this.gbox_estatus.Text = "Estatus";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(40, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 29);
            this.textBox1.TabIndex = 3;
            // 
            // btn_revisar
            // 
            this.btn_revisar.Location = new System.Drawing.Point(114, 73);
            this.btn_revisar.Name = "btn_revisar";
            this.btn_revisar.Size = new System.Drawing.Size(75, 23);
            this.btn_revisar.TabIndex = 2;
            this.btn_revisar.Text = "Revisar";
            this.btn_revisar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(233, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(644, 111);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resultados";
            // 
            // FormaPLlegadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Acuanet.Properties.Resources.descarga1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1204, 577);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbox_estatus);
            this.Controls.Add(this.dgv_llegadas);
            this.Name = "FormaPLlegadas";
            this.Text = "PantallaLlegadas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPL_FormClosing);
            this.Load += new System.EventHandler(this.frmPL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_llegadas)).EndInit();
            this.gbox_estatus.ResumeLayout(false);
            this.gbox_estatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgv_llegadas;
        private System.Windows.Forms.GroupBox gbox_estatus;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_revisar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}