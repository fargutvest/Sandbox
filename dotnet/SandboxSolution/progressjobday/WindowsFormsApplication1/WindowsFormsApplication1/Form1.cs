using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        System.Timers.Timer tmr1 = new System.Timers.Timer();
        public Form1()
        {
            InitializeComponent();
            tmr1.Interval = 1000;
            tmr1.Elapsed += new System.Timers.ElapsedEventHandler(tmr1_Elapsed);
          tmr1.Start();
        }

        void tmr1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int nachalo = 32400; //9ч 00м
            int konez = 63000; //17ч 30м
            int r = DateTime.Now.TimeOfDay.Hours*3600+DateTime.Now.TimeOfDay.Minutes*60+DateTime.Now.TimeOfDay.Seconds;
            if (r < 63000) progressBar1.Invoke(new Action<int>((val) => progressBar1.Value = val), r - nachalo);
            
        }



        bool keyDown = false;
        int x = 0, y = 0;
        
        private void progressBar1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!keyDown) return;
            this.Left += e.X - x;           
            this.Top += e.Y - y;     
        }

        private void progressBar1_MouseUp(object sender, MouseEventArgs e)
        {
            keyDown = false;
        }

        private void progressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            keyDown = true;
            x = e.X;
            y = e.Y;
        }
        
    }
}
