using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Task.Factory.StartNew(new Action(() => 
            {
                while (true)
                {
                    chartControl1.Invalidate();
                }
            }));
        }

        private void chartControl1_FpsChanged(object sender, double e)
        {
            BeginInvoke(new Action(() =>
            {
                Text = String.Format("FPS={0}", e.ToString("0.###"));
            }));
        }
    }
}
