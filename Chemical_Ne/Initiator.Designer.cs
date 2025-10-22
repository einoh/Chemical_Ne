namespace Chemical_Ne
{
    partial class Initiator
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
            this.TmrServerConnectionStatus = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.PdPrinter = new System.Drawing.Printing.PrintDocument();
            this.SpArduinoConnection = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TmrServerConnectionStatus
            // 
            this.TmrServerConnectionStatus.Enabled = true;
            this.TmrServerConnectionStatus.Interval = 1000;
            this.TmrServerConnectionStatus.Tick += new System.EventHandler(this.TmrServerConnectionStatus_Tick);
            // 
            // SpArduinoConnection
            // 
            this.SpArduinoConnection.PortName = "COM3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(434, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Initiator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.IsMdiContainer = true;
            this.Name = "Initiator";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer TmrServerConnectionStatus;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Drawing.Printing.PrintDocument PdPrinter;
        private System.IO.Ports.SerialPort SpArduinoConnection;
        private System.Windows.Forms.Label label1;
    }
}

