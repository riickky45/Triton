namespace Acuanet
{
    partial class PantallaLlegadas
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_llegadas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_llegadas
            // 
            this.dgv_llegadas.AllowUserToAddRows = false;
            this.dgv_llegadas.AllowUserToDeleteRows = false;
            this.dgv_llegadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_llegadas.Location = new System.Drawing.Point(12, 165);
            this.dgv_llegadas.Name = "dgv_llegadas";
            this.dgv_llegadas.ReadOnly = true;
            this.dgv_llegadas.Size = new System.Drawing.Size(1094, 315);
            this.dgv_llegadas.TabIndex = 0;
            // 
            // PantallaLlegadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 577);
            this.Controls.Add(this.dgv_llegadas);
            this.Name = "PantallaLlegadas";
            this.Text = "PantallaLlegadas";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_llegadas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgv_llegadas;
    }
}