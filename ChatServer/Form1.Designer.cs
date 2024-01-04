
namespace ChatServer
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPorta = new System.Windows.Forms.NumericUpDown();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.listLog = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorta)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtIP.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.ForeColor = System.Drawing.SystemColors.Window;
            this.txtIP.Location = new System.Drawing.Point(41, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(209, 33);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "127.0.0.1";
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPorta
            // 
            this.txtPorta.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtPorta.Location = new System.Drawing.Point(269, 13);
            this.txtPorta.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtPorta.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(67, 33);
            this.txtPorta.TabIndex = 1;
            this.txtPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPorta.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnStartServer
            // 
            this.btnStartServer.Font = new System.Drawing.Font("Rosewood Std Regular", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartServer.Location = new System.Drawing.Point(359, 13);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(135, 32);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Start Server ";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.Location = new System.Drawing.Point(12, 55);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(492, 264);
            this.listLog.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 332);
            this.Controls.Add(this.listLog);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtPorta);
            this.Controls.Add(this.txtIP);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.txtPorta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.NumericUpDown txtPorta;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.ListBox listLog;
    }
}

