using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication10
{
    delegate void SetTextCallback(string text);
    
    public partial class Form1 : Form
    {
        BackgroundWorker th = new BackgroundWorker();
        private static string FileLog = "richTextBox1.txt";
        private bool Exit = false;

        public Form1()
        {
            InitializeComponent();

            th.WorkerReportsProgress = true;
            th.WorkerSupportsCancellation = true;
            th.DoWork += ThreadProcSafe;
            th.RunWorkerCompleted += th_RunWorkerCompleted;

            Application.Idle += Application_Idle;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            buttonStart.Enabled = !th.IsBusy;
            buttonStop.Enabled = th.IsBusy && th.WorkerSupportsCancellation;
        }

        private void SetText(string text)
        {
            richTextBox1.AppendText(text + "\n");

            try
            {
                File.AppendAllText(FileLog, text + "\n");
            }
            catch
            {

            }
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
            if (th.IsBusy && th.WorkerSupportsCancellation)
            {
                th.CancelAsync();
            }
        }

        public void StartThread()
        {
            StopThread();

            if (th.IsBusy != true)
            {

                try
                {
                    File.Delete(FileLog);
                }
                catch
                {

                }

                SetTextSafe("GO...");

                th.RunWorkerAsync();
            }
        }

        void th_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                SetText("Canceled!");
            }
            else if (e.Error != null)
            {
                SetText("Error: " + e.Error.Message);
            }
            else
            {
                SetText("Done!");
            }

            
            SetText("");

            if (Exit)
                Close();
        }

        private void ThreadProcSafe(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            SetTextSafe("Start...");
            try
            {
                int i = 0;
                while (i < 10)
                {
                    SetTextSafe(i.ToString());

                    i++;

                    Thread.Sleep(100);

                    if (worker.CancellationPending == true)
                    {
                        SetTextSafe("terminate");
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // worker.ReportProgress(i * 10);
                    }


                }
            }
            catch (Exception)
            {
                SetTextSafe("Exception");
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
            /*            
            while (th.IsBusy)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }
            */
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopThread();

            if (th.IsBusy)
            {
               Exit = true;
               e.Cancel = true;
            }
        }

    }
}
