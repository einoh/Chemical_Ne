namespace Chemical_Ne
{
    partial class Dashboard
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
            this.lblCredits = new System.Windows.Forms.Label();
            this.lblMySQLConStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCredits
            // 
            this.lblCredits.AutoSize = true;
            this.lblCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredits.Location = new System.Drawing.Point(150, 181);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(198, 37);
            this.lblCredits.TabIndex = 0;
            this.lblCredits.Text = "Retrieving...";
            // 
            // lblMySQLConStatus
            // 
            this.lblMySQLConStatus.AutoSize = true;
            this.lblMySQLConStatus.Location = new System.Drawing.Point(170, 313);
            this.lblMySQLConStatus.Name = "lblMySQLConStatus";
            this.lblMySQLConStatus.Size = new System.Drawing.Size(75, 13);
            this.lblMySQLConStatus.TabIndex = 1;
            this.lblMySQLConStatus.Text = "MySQL Status";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.lblMySQLConStatus);
            this.Controls.Add(this.lblCredits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblCredits;
        public System.Windows.Forms.Label lblMySQLConStatus;
    }
}