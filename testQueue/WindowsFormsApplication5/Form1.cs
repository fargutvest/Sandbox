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

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        BackgroundWorker bgw = new BackgroundWorker();
        Thread thr1;
        Thread thr2;
        Queue<string>qu;
        UInt64 i = 0;
        public Form1()
        {
            InitializeComponent();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerAsync();
            

        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            qu = new Queue<string>();
            thr1 = new Thread(method1);
            thr2 = new Thread(method2);
            thr1.Start();
            thr2.Start();
        }

        void method1()
        {
         
            while (true)
            {
                i++;
                lock (qu)
                {
                    qu.Enqueue(i.ToString());
                }
               
            }
        }

        void method2()
        {
            while (true)
            {
                if (qu.Count != 0)
                {
                    lock (qu)
                    {
                        qu.Dequeue();
                    }
                }
                label1.Invoke(new Action<string>((s) => label1.Text = s), String.Format("{0}  {1}", qu.Count.ToString(), i.ToString()));
            }
        }
    }
}
