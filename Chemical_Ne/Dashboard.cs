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
                _initiator.PrintVoucher();
                Hours3.Enabled = false;
            }
            else
            {
                Hours3.Enabled = false;
            }
        }

        private void Hours8_Click(object sender, EventArgs e)
        {

        }

        private void Hours24_Click(object sender, EventArgs e)
        {

        }
    }
}
