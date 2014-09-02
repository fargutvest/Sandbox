using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    delegate void SetTextCallback(string text);

    public partial class Form1 : Form
    {
        Thread th;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetText(string text)
        {
            richTextBox1.AppendText(text + "\n");
        }

        public void SetTextSafe(string value)
        {
         //   if (richTextBox1 != null)
            {
                if (richTextBox1.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    Invoke(d, new object[] { value + " (Invoke)" });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    SetText(value + " (No Invoke)");
                }
            }
        }
        public void StopThread()
        {

            if (th != null)
            {
                th.Interrupt();

                while (th.Join(10) == false)
                {
                    Application.DoEvents();
                }

                SetTextSafe("OK...");
                SetText("");
                th = null;
            }
        }

        public void StartThread()
        {
            StopThread();

            th = new Thread(new ThreadStart(this.ThreadProcSafe));
            SetTextSafe("GO...");
            th.Start();
        }

        private void ThreadProcSafe()
        {
            SetTextSafe("Start...");
            try
            {
                int i = 0;
                while (i < 1000)
                {
                    SetTextSafe(i.ToString());

                    i++;
                    Thread.Sleep(100);
 
                }
            }
            catch (ThreadInterruptedException)
            {
                SetTextSafe("terminate");
            }
            
            SetTextSafe("stop");
          
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartThread();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopThread();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           StopThread();
          /*  if (th != null)
            {
                th.Abort();
                th.Join();                            
            }*/
        }

    }
}
