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
            this.Hours3 = new System.Windows.Forms.Button();
            this.Hours8 = new System.Windows.Forms.Button();
            this.Hours24 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCredits
            // 
            this.lblCredits.AutoSize = true;
            this.lblCredits.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredits.Location = new System.Drawing.Point(47, 33);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(294, 56);
            this.lblCredits.TabIndex = 0;
            this.lblCredits.Text = "Retrieving...";
            // 
            // Hours3
            // 
            this.Hours3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hours3.Location = new System.Drawing.Point(498, 171);
            this.Hours3.Name = "Hours3";
            this.Hours3.Size = new System.Drawing.Size(151, 47);
            this.Hours3.TabIndex = 2;
            this.Hours3.Text = "3 Hours (P 5.00)";
            this.Hours3.UseVisualStyleBackColor = true;
            this.Hours3.Click += new System.EventHandler(this.Hours3_Click);
            // 
            // Hours8
            // 
            this.Hours8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hours8.Location = new System.Drawing.Point(498, 247);
            this.Hours8.Name = "Hours8";
            this.Hours8.Size = new System.Drawing.Size(151, 47);
            this.Hours8.TabIndex = 3;
            this.Hours8.Text = "8 Hours (P 10.00)";
            this.Hours8.UseVisualStyleBackColor = true;
            this.Hours8.Click += new System.EventHandler(this.Hours8_Click);
            // 
            // Hours24
            // 
            this.Hours24.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hours24.Location = new System.Drawing.Point(498, 318);
            this.Hours24.Name = "Hours24";
            this.Hours24.Size = new System.Drawing.Size(151, 47);
            this.Hours24.TabIndex = 4;
            this.Hours24.Text = "24 Hours (P 25.00)";
            this.Hours24.UseVisualStyleBackColor = true;
            this.Hours24.Click += new System.EventHandler(this.Hours24_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.Hours24);
            this.Controls.Add(this.Hours8);
            this.Controls.Add(this.Hours3);
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
        private System.Windows.Forms.Button Hours3;
        private System.Windows.Forms.Button Hours8;
        private System.Windows.Forms.Button Hours24;
    }
}