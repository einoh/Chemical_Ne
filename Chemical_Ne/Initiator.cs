using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemical_Ne
{
    public partial class Initiator : Form
    {
        private string data = string.Empty;
        private int counter = 0;

        Dashboard _Dashboard = new Dashboard();
        Offline _Offline = new Offline();

        public Initiator()
        {
            InitializeComponent();
            this.IsMdiContainer = true;

            // Assign MDI parents
            _Dashboard.MdiParent = this;
            _Offline.MdiParent = this;

            // Initialize Serial Port
            SpArduinoConnection = new SerialPort("COM3", 9600);
            SpArduinoConnection.DataReceived += SpArduinoConnection_DataReceived;

            // Try opening port
            try
            {
                SpArduinoConnection.Open();
            }
            catch (Exception)
            {
                _Offline.Show();
                _Offline.lblStatus.Text = "Hardware Disconnected";
            }

            _Dashboard.Show();

            // Example timer to trigger periodic checks
            using (Timer timer = new Timer())
            {
                timer.Interval = 1000; // 1 second
                timer.Tick += TmrServerConnectionStatus_Tick;
                timer.Start();
            }
        }

        private void SpArduinoConnection_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string receivedData = SpArduinoConnection.ReadLine();
                data = receivedData;

                // Safely update UI from background thread
                this.Invoke((MethodInvoker)delegate
                {
                    _Dashboard.lblCredits.Text = data;
                });
            }
            catch (Exception)
            {
                // Ignore or log serial read errors
            }
        }


        private void TmrServerConnectionStatus_Tick(object sender, EventArgs e)
        {
            if (SpArduinoConnection.IsOpen)
            {
                // Executes every second
                if (_Dashboard.lblCredits.Text == "Retrieving...")
                {
                    SpArduinoConnection.Write("9");
                }

                _Dashboard.lblCredits.Text = data;

                // Executes every 5 seconds
                counter++;
                if (counter >= 5)
                {
                    counter = 0;

                    if (IsConnectionAvailable())
                    {
                        CheckPrinterStatus();
                    }
                    else
                    {
                        _Dashboard.Close();
                        _Offline.Show();
                        _Offline.lblStatus.Text = "Connection Disconnected";
                    }
                }
            }
            else
            {
                try
                {
                    SpArduinoConnection.Open();
                }
                catch
                {
                    _Dashboard.Close();
                    _Offline.Show();
                    _Offline.lblStatus.Text = "Hardware Disconnected";
                }
            }
        }

        private void CheckPrinterStatus()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        int statusValue = Convert.ToInt32(printer["PrinterStatus"]);
                        string status = PrinterStatusToString((PrinterStatus)statusValue);

                        if (status == "offline")
                        {
                            _Dashboard.Close();
                            _Offline.Show();
                            _Offline.lblStatus.Text = "Printer Disconnected";
                            return;
                        }
                    }
                }

                // Printer is online — show dashboard
                _Offline.Close();
                _Dashboard.Show();
            }
            catch (Exception ex)
            {
                _Offline.Show();
                _Offline.lblStatus.Text = "Error checking printer: " + ex.Message;
            }
        }

        public enum PrinterStatus
        {
            PrinterIdle = 3,
            PrinterPrinting = 4,
            PrinterWarmingUp = 5
        }

        private string PrinterStatusToString(PrinterStatus ps)
        {
            switch (ps)
            {
                case PrinterStatus.PrinterIdle:
                    return "waiting (idle)";
                case PrinterStatus.PrinterPrinting:
                    return "printing";
                case PrinterStatus.PrinterWarmingUp:
                    return "warming up";
                default:
                    return "offline";
            }
        }

        public bool IsConnectionAvailable()
        {
            string connectionString = "Server=localhost;Database=chemical_api;Uid=root;Pwd=gscidata;";

            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                {
                    connection.Open();
                    _Dashboard.lblMySQLConStatus.Text = "MySQL Status: Connected";
                    // If we got here, the connection was successful
                    return true;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                return false;
            }
        }

    }
}
