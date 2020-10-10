using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        private static System.Threading.Timer ThreadTimer;
        private static System.Windows.Forms.Timer FormTimer;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
             */
            test1();
        }


        static void test1()
        {
            ThreadTimer = new System.Threading.Timer(ThreadTimerMethod, null, 1000, 1000);
            Console.WriteLine("llalalalala");
            WaitHandle.WaitAny(new WaitHandle[] { new AutoResetEvent(false) });
        }

        static void test2()
        {
            FormTimer = new System.Windows.Forms.Timer();
            FormTimer.Tick += FormTimer_Tick;
            FormTimer.Interval = 100;
            FormTimer.Start();
            Console.WriteLine("llalalalala");
            WaitHandle.WaitAny(new WaitHandle[] { new AutoResetEvent(false) });
        }

        static void FormTimer_Tick(object sender, EventArgs e)
        {
            Console.Write("FormTimer");
        }


        public static void ThreadTimerMethod(object state)
        {
            Console.Write("ThreadTimer");
        }
    }
}
