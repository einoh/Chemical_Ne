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
            Hours3.Enabled = false;
            try
            {
                if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
                {
                    Cursor = Cursors.WaitCursor;
                    var (VoucherCode, Duration) = Initiator.GetVoucherInfo(60);
                    if (!string.IsNullOrEmpty(VoucherCode) && !VoucherCode.StartsWith("Error"))
                    {
                        _initiator.voucherCode = VoucherCode;
                        _initiator.voucherDuration = Duration;
                        _initiator.PrintVoucher();
                        _initiator.SpArduinoConnection.Write("1");
                    }
                    else
                    {
                        lblError.Text = "Failed to get voucher code. Please try again.";
                    }
                }
                else
                {
                    lblError.Text = "Invalid or unavailable credits.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                Hours3.Enabled = true;
                Cursor = Cursors.Default;
                lblError.Text = ""; 
            }
        }


        private void Hours8_Click(object sender, EventArgs e)
        {
            Hours8.Enabled = false;
            try
            {
                if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
                {
                    Cursor = Cursors.WaitCursor;
                    var (VoucherCode, Duration) = Initiator.GetVoucherInfo(480);
                    if (!string.IsNullOrEmpty(VoucherCode) && !VoucherCode.StartsWith("Error"))
                    {
                        _initiator.voucherCode = VoucherCode;
                        _initiator.voucherDuration = Duration;
                        _initiator.PrintVoucher();
                        _initiator.SpArduinoConnection.Write("2");
                    }
                    else
                    {
                        lblError.Text = "Failed to get voucher code. Please try again.";
                    }
                }
                else
                {
                    lblError.Text = "Invalid or unavailable credits.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                Hours8.Enabled = true;
                Cursor = Cursors.Default;
                lblError.Text = "";
            }
        }

        private void Hours24_Click(object sender, EventArgs e)
        {
            Hours24.Enabled = false;
            try
            {
                if (!string.IsNullOrEmpty(lblCredits.Text) && lblCredits.Text != "0" && lblCredits.Text != "Retrieving...")
                {
                    Cursor = Cursors.WaitCursor;
                    var (VoucherCode, Duration) = Initiator.GetVoucherInfo(1440);
                    if (!string.IsNullOrEmpty(VoucherCode) && !VoucherCode.StartsWith("Error"))
                    {
                        _initiator.voucherCode = VoucherCode;
                        _initiator.voucherDuration = Duration;
                        _initiator.PrintVoucher();
                        _initiator.SpArduinoConnection.Write("2");
                        _initiator.SpArduinoConnection.Write("2");
                    }
                    else
                    {
                        lblError.Text = "Failed to get voucher code. Please try again.";
                    }
                }
                else
                {
                    lblError.Text = "Invalid or unavailable credits.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                Hours24.Enabled = true;
                Cursor = Cursors.Default;
                lblError.Text = "";
            }
        }

    }
}
