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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int WM_KEYDOWN = 0x0100;
        public Form1()
        {
            InitializeComponent();
            int CurrentProcessId = Process.GetCurrentProcess().Id;
            this.Text = string.Format("id:{0} (0x{1})",
                CurrentProcessId.ToString(), CurrentProcessId.ToString("X4"));

        }


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    string text = m.ToString() + "*";
                    Invoke(new Action(() => { rchtbOutput.Text += text; }));
                    break;
            }

            Debug.WriteLine(m.ToString());
            base.WndProc(ref m);
        }

    }
}
