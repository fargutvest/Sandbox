using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tolkit.controls
{
    public partial class SelectEncoding : UserControl
    {
        public event EventHandler ChekedChanged;
        

        public SelectEncoding()
        {
            InitializeComponent();
            rbHex.Checked = true;
        }

        private void rbHex_CheckedChanged(object sender, EventArgs e)
        {
            if (ChekedChanged != null)
                ChekedChanged(sender, e);
        }

        private void rbAscii_CheckedChanged(object sender, EventArgs e)
        {
            if (ChekedChanged != null)
                ChekedChanged(sender, e);
        }

        private void rbUnicode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChekedChanged != null)
                ChekedChanged(sender, e);
        }
    }
}
