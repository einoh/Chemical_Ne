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
            // Initiator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "Initiator";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TmrServerConnectionStatus;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Drawing.Printing.PrintDocument PdPrinter;
        private System.IO.Ports.SerialPort SpArduinoConnection;
    }
}

