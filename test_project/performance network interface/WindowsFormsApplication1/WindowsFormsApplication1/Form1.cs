using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{ 
    delegate void Mydel(string s);
    
    public partial class Form1 : Form
    {
        PerformanceCounter performanceCounterSent;
        PerformanceCounter performanceCounterReceived;
        PerformanceCounterCategory pcc;
        System.Threading.Thread th1;
        public Form1()
        {
            InitializeComponent();

            pcc = new PerformanceCounterCategory("Network Interface");   
            string[] intance = pcc.GetInstanceNames();
            foreach (string s in intance)
            comboBox1.Items.Add(s);
            comboBox1.SelectedIndex = 0;

         
        }

        void start(string s)
        {
            this.Text = s;   
        }

     

        void performance()
        {
            
            while (true)
            {
                this.Invoke(new Mydel(start),("d:"+performanceCounterReceived.NextValue() / 1024).ToString()+" kbyte/s  "+"u:"+  (performanceCounterSent.NextValue() / 1024).ToString()+" kbyte/s");
                System.Threading.Thread.Sleep(1000);
            }
        }
        bool th1_run=false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (th1_run == true)
            {
                th1.Abort();
                th1_run = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", comboBox1.Text);
            performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", comboBox1.Text);

            if (th1_run == true)
            {
                th1.Abort();
                th1_run = false;
            }
            th1 = new System.Threading.Thread(performance);
            th1.Start();
            th1_run = true;
        }


       

    }
}
