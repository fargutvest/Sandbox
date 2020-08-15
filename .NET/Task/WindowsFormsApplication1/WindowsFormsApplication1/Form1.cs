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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Task task1;
        bool bStopTask1 = false;

        Task task2;
        AutoResetEvent areStoptask2 = new AutoResetEvent(false);

        Task task3;
        AutoResetEvent areStoptask3 = new AutoResetEvent(false);



        public Form1()
        {
            InitializeComponent();

            //Этот таск завершается булевской переменной
            task1 = Task.Factory.StartNew(() =>
            {
                while (!bStopTask1)
                {
                    WaitHandle.WaitAny(new WaitHandle[] { new AutoResetEvent(false) }, 1);
                }
            });

            //Этот таск завершается сигналом AutoResetEvent
            task2 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int res = WaitHandle.WaitAny(new WaitHandle[] { areStoptask2 }, 1);
                    switch (res)
                    {
                        case 0:
                            return;

                        case 1:
                            break;
                    }
                }
            });

            //Этот таск запускается через асинхронный метод и завершается сигналом AutoResetEvent
            task3 = AsyncStartTask3();
        }


        Task AsyncStartTask3()
        {
            return Task.Factory.StartNew(() =>
           {
               while (true)
               {
                   int res = WaitHandle.WaitAny(new WaitHandle[] { areStoptask3 }, 1);
                   switch (res)
                   {
                       case 0:
                           return;

                       case 1:
                           break;
                   }
               }
           });
        }

        private void btStopTask_Click(object sender, EventArgs e)
        {
            StopTask1();
            StopTask2();
            StopTask3();
        }

        void StopTask1()
        {
            bStopTask1 = true;
            Task.WaitAll(new Task[] { task1 });
        }

        void StopTask2()
        {
            areStoptask2.Set();
            Task.WaitAll(new Task[] { task2 });
        }

        void StopTask3()
        {
            areStoptask3.Set();
            Task.WaitAll(new Task[] { task3 });
        }
    }
}
