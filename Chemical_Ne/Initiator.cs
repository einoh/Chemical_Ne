using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace Chemical_Ne
{
    public partial class Initiator : Form
    {
        private string data = string.Empty;
        private int counter = 0;

        readonly Dashboard _Dashboard;
        readonly Offline _Offline;
        //readonly Offline _Offline = new Offline();

        public Initiator()
        {
            InitializeComponent();

            this.IsMdiContainer = true;

            _Dashboard = new Dashboard(this)
            {
                MdiParent = this
            };

            _Offline = new Offline(this)
            {
                MdiParent = this
            };


            // Initialize Serial Port
            SpArduinoConnection.DataReceived += SpArduinoConnection_DataReceived;

            //Initialize Printer
            PdPrinter.PrintPage += PdPrinter_PrintPage;  // Add this line

            // Try opening port
            try
            {
                SpArduinoConnection.Open();
            }
            catch (Exception)
            {
                _Offline.Show();
                _Offline.lblStatus.Text = "Hardware Disconnected";
                CenterStatusLabel();
            }

            _Dashboard.Show();

            // Example timer to trigger periodic checks
            Timer timer = new Timer
            {
                Interval = 1000 // 1 second
            };
            timer.Tick += TmrServerConnectionStatus_Tick;
            timer.Start();
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
                        _Dashboard.Hide();
                        _Offline.Show();
                        _Offline.lblStatus.Text = "Database Disconnected";
                        CenterStatusLabel();
                    }
                }
            }
            else
            {
                try
                {
                    SpArduinoConnection.Open();
                    SpArduinoConnection.DataReceived += SpArduinoConnection_DataReceived;
                }
                catch
                {
                    _Dashboard.Hide();
                    _Offline.Show();
                    _Offline.lblStatus.Text = "Hardware Disconnected";
                    CenterStatusLabel();
                }
            }
        }

        private void CheckPrinterStatus()
        {
            try
            {
                bool printerFound = false;
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
                {
                    foreach (ManagementObject printer in searcher.Get().Cast<ManagementObject>())
                    {
                        printerFound = true;
                        int statusValue = Convert.ToInt32(printer["PrinterStatus"]);
                        bool isOffline = Convert.ToBoolean(printer["WorkOffline"]);
                        string status = PrinterStatusToString((PrinterStatus)statusValue);

                        if (status == "offline" || isOffline)
                        {
                            _Dashboard.Hide();
                            _Offline.Show();
                            _Offline.lblStatus.Text = "Printer Disconnected";
                            CenterStatusLabel();
                            return;
                        }
                    }
                }

                if (!printerFound)
                {
                    _Dashboard.Hide();
                    _Offline.Show();
                    _Offline.lblStatus.Text = "No Printer Found";
                    CenterStatusLabel();
                    return;
                }

                // Printer is online — show dashboard
                _Offline.Hide();
                _Dashboard.Show();
            }
            catch (Exception ex)
            {
                _Dashboard.Hide();
                _Offline.Show();
                _Offline.lblStatus.Text = "Error checking printer: " + ex.Message;
                CenterStatusLabel();
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
                    return true;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                return false;
            }
        }

        private void CenterStatusLabel()
        {
            _Offline.lblStatus.Top = (_Offline.ClientSize.Height - _Offline.lblStatus.Height) / 2;
            _Offline.lblStatus.Left = (_Offline.ClientSize.Width - _Offline.lblStatus.Width) / 2;
        }

        private void PdPrinter_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font repFontNormal = new Font("Century Gothic", 10);
            Font repFontVoucher = new Font("Century Gothic", 14, FontStyle.Bold);

            e.Graphics.DrawString("Branchette WiFi", repFontNormal, Brushes.Black, 80, 5);
            e.Graphics.DrawString("9fasFs15sf", repFontVoucher, Brushes.Black, 80, 25);
            e.Graphics.DrawString("1 Hour Voucher Code", repFontNormal, Brushes.Black, 70, 50);
            e.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"), repFontNormal, Brushes.Black, 50, 70);
            e.Graphics.DrawString("Branchette Systems", repFontNormal, Brushes.Black, 80, 90);
        }

        public void PrintVoucher()
        {
            try
            {
                PdPrinter.PrinterSettings.Copies = 1;
                PdPrinter.PrintController = new StandardPrintController(); // hides print dialog
                PdPrinter.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Printing error: {ex.Message}", "Print Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
