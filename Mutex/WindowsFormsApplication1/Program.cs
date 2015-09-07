using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        static Mutex _m;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            if (IsSingleInstance())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("Your application is started!");
            }
        }

        static bool IsSingleInstance()
        {
            try
            {
                // Try to open existing mutex.
                Mutex.OpenExisting("MotoCodeScan");
            }
            catch
            {
                // If exception occurred, there is no such mutex.
                Program._m = new Mutex(true, "MotoCodeScan");

                // Only one instance.
                return true;
            }
            // More than one instance.
            return false;
        }
    }
}
