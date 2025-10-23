using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemical_Ne
{
    public partial class Dashboard : Form
    {

        public Dashboard()
        {
            InitializeComponent();
            PdPrinter.PrintPage += PdPrinter_PrintPage;  // Add this line
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check if lblCredits value is not zero or empty
            if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
            {
                PrintVoucher();
            }
            else
            {
                MessageBox.Show("No credits available.");
            }
        }

        private void PdPrinter_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font repFontNormal = new Font("Century Gothic", 10);
            Font repFontVoucher = new Font("Century Gothic", 14, FontStyle.Bold);

            e.Graphics.DrawString("Branchette WiFi", repFontNormal, Brushes.Black, 80, 5);
            e.Graphics.DrawString("9fasFs15sf", repFontVoucher, Brushes.Black, 80, 25);
            e.Graphics.DrawString("1 Hour Voucher Code", repFontNormal, Brushes.Black, 70, 50);
            e.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"), repFontNormal, Brushes.Black, 80, 70);
            e.Graphics.DrawString("Golden Success College", repFontNormal, Brushes.Black, 80, 90);
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
