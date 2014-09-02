using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;


namespace testWaitTask
{
    public partial class Form1 : Form
    {
        Task task;
        CancellationTokenSource cts;
        AutoResetEvent are = new AutoResetEvent(false);

        public Form1()
        {
            InitializeComponent();
           
        }

        void taskExecuted(object cancell)
        {
            CancellationToken ct = (CancellationToken)cancell;
            int c = 0;
            try
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }
                    Debug.WriteLine((c++).ToString());
                    WaitHandle.WaitAll(new WaitHandle[] { are });

                }
            }
            catch (OperationCanceledException ex) { Debug.WriteLine("OperationCanceledException"); }
        }

        private void btStartTask_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            task = new Task(taskExecuted, cts.Token);
            task.Start();
        }

        private void btDisposeTask_Click(object sender, EventArgs e)
        {
            are.Set();
            cts.Cancel();
            task.Wait();
            task.Dispose();
            task = null;
            Debug.WriteLine("task = null;");
        }

        private void btAreSet_Click(object sender, EventArgs e)
        {
            are.Set();
        }
    }
}
