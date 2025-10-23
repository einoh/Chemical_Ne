using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemical_Ne
{
    public partial class Offline : Form
    {
        private readonly Initiator _initiator;
        public Offline(Initiator initiator)
        {
            InitializeComponent();
            _initiator = initiator;
        }
    }
}
