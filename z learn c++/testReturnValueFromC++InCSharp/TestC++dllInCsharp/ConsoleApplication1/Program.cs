using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Mime;

namespace ConsoleApplication1
{
    class Program
    {
        string path = @"d:\GITREPOSITORIES\test\testReturnValueFromCPPInCSharp\ConsoleApplication1\Debug\Project1.dll"


            [DllImport(path, CallingConvention = CallingConvention.Cdecl)]
            static extern int test(int value);

 /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
