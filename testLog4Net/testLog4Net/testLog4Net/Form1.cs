using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using testLog4Net;
using System.Diagnostics;

namespace testLog4Net
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void test()
        {
            int r = 2+3;
            Program.log.Info("Summ = " + r);
            //Program.log.Fatal("Fatal Error!!!");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            test();
        }
    }
}
