using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace selectAudioDrive
{
    public partial class Form1 : Form
    {
        string path = @"AudioEndPointController\Release\EndPointController.exe";
        public Form1()
        {
            InitializeComponent();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                Process.Start(path,rb.Tag.ToString());
            }
            
        }
    }
}
