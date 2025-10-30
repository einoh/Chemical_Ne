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
            int credits = Convert.ToInt32(lblCredits.Text);

            Hours3.Enabled = false;
            Hours8.Enabled = false;
            Hours24.Enabled = false;

            try
            {
                if (!string.IsNullOrEmpty(lblCredits.Text) && credits != 0 && lblCredits.Text != "Retrieving...")
                {
                    if (credits < 5)
                    {
                        MessageBox.Show("Insufficient credit balance.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
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
                            MessageBox.Show("Failed to get voucher code. Please try again.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid or unavailable credits.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Hours3.Enabled = true;
                Hours8.Enabled = true;
                Hours24.Enabled = true;
                Cursor = Cursors.Default; 
            }
        }


        private void Hours8_Click(object sender, EventArgs e)
        {
            int credits = Convert.ToInt32(lblCredits.Text);

            Hours3.Enabled = false;
            Hours8.Enabled = false;
            Hours24.Enabled = false;

            try
            {
                if (!string.IsNullOrEmpty(lblCredits.Text) && credits != 0 && lblCredits.Text != "Retrieving...")
                {
                    if (credits < 10)
                    {
                        MessageBox.Show("Insufficient credit balance.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
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
                            MessageBox.Show("Failed to get voucher code. Please try again.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }  
                }
                else
                {
                    MessageBox.Show("Invalid or unavailable credits.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Hours3.Enabled = true;
                Hours8.Enabled = true;
                Hours24.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void Hours24_Click(object sender, EventArgs e)
        {
            int credits = Convert.ToInt32(lblCredits.Text);

            Hours3.Enabled = false;
            Hours8.Enabled = false;
            Hours24.Enabled = false;

            try
            {
                if (!string.IsNullOrEmpty(lblCredits.Text) && credits != 0 && lblCredits.Text != "Retrieving...")
                {
                    if (credits < 25)
                    {
                        MessageBox.Show("Insufficient credit balance.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Cursor = Cursors.WaitCursor;
                        var (VoucherCode, Duration) = Initiator.GetVoucherInfo(1440);
                        if (!string.IsNullOrEmpty(VoucherCode) && !VoucherCode.StartsWith("Error"))
                        {
                            _initiator.voucherCode = VoucherCode;
                            _initiator.voucherDuration = Duration;
                            _initiator.PrintVoucher();
                            _initiator.SpArduinoConnection.Write("4");
                            _initiator.SpArduinoConnection.Write("1");
                        }
                        else
                        {
                            MessageBox.Show("Failed to get voucher code. Please try again.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid or unavailable credits.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Hours3.Enabled = true;
                Hours8.Enabled = true;
                Hours24.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _initiator.SpArduinoConnection.Write("8");
            MessageBox.Show("Session has been reset.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
