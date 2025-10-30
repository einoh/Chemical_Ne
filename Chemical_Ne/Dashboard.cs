using System;
using System.Windows.Forms;

namespace Chemical_Ne
{
    public partial class Dashboard : Form
    {
        private readonly Initiator _initiator;
        public Dashboard(Initiator initiator)
        {
            InitializeComponent();
            _initiator = initiator;
        }

        private void Hours3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
            {
                var (VoucherCode, Duration) = Initiator.GetVoucherInfo(60);
                if (VoucherCode != null && Duration != null)
                {
                    if (VoucherCode != "Error: NotFound")
                    {
                        _initiator.voucherCode = VoucherCode;
                        _initiator.voucherDuration = Duration;
                        _initiator.PrintVoucher();
                        _initiator.SpArduinoConnection.Write("1");
                    }
                    else
                    {
                        Hours3.Enabled = false;
                    }
                }
                else
                {
                    Hours3.Enabled = false;
                }
            }
            else
            {
                Hours3.Enabled = false;
            }
        }

        private void Hours8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
            {
                var (VoucherCode, Duration) = Initiator.GetVoucherInfo(480);
                if (VoucherCode != null && Duration != null)
                {
                    if (VoucherCode != "Error: NotFound")
                    {
                        _initiator.voucherCode = VoucherCode;
                        _initiator.voucherDuration = Duration;
                        _initiator.PrintVoucher();
                        _initiator.SpArduinoConnection.Write("2");
                    }
                    else
                    {
                        Hours8.Enabled = false;
                    }
                }
                else
                {
                    Hours8.Enabled = false;
                }
            }
            else
            {
                Hours8.Enabled = false;
            }
        }

        private void Hours24_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
            {
                var (VoucherCode, Duration) = Initiator.GetVoucherInfo(1440);
                if (VoucherCode != null && Duration != null)
                { 
                    if (VoucherCode != "Error: NotFound")
                    {
                        _initiator.voucherCode = VoucherCode;
                        _initiator.voucherDuration = Duration;
                        _initiator.PrintVoucher();
                        _initiator.SpArduinoConnection.Write("3");
                    }
                    else
                    {
                        Hours24.Enabled = false;
                    }
                }
                else
                {
                    Hours24.Enabled = false;
                }
            }
            else
            {
                Hours24.Enabled = false;
            }
        }

    }
}
