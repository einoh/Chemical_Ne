using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemical_Ne
{
    public partial class Initiator : Form
    {
        private string data = string.Empty;
        private int counter = 0;

        readonly Dashboard _Dashboard;
        readonly Offline _Offline;

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


        private async void TmrServerConnectionStatus_Tick(object sender, EventArgs e)
        {
            bool isOnline = await IsConnectionAvailableAsync();
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


                    if (isOnline)
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

        public async Task<bool> IsConnectionAvailableAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(3);

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5000/api/voucher/status");
                    return response.IsSuccessStatusCode;
                }
                catch (HttpRequestException)
                {
                    return false;
                }
                catch (TaskCanceledException)
                {
                    // Timeout or canceled request
                    return false;
                }
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

            // Get printable width in pixels
            float pageWidth = e.PageBounds.Width;

            // Helper function to center text horizontally
            void DrawCenteredString(string text, Font font, float y)
            {
                SizeF textSize = e.Graphics.MeasureString(text, font);
                float x = (pageWidth - textSize.Width) / 2;
                e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            }

            // Now print text centered
            DrawCenteredString("Branchette WiFi", repFontNormal, 5);
            DrawCenteredString("9fasFs15sf", repFontVoucher, 25);
            DrawCenteredString("1 Hour Voucher Code", repFontNormal, 50);
            DrawCenteredString(DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"), repFontNormal, 70);
            DrawCenteredString("Branchette Systems", repFontNormal, 90);
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
