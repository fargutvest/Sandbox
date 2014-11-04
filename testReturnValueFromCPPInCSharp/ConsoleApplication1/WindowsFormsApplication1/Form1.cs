using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const string pathLib = @"d:\GITREPOSITORIES\test\testReturnValueFromCPPInCSharp\ConsoleApplication1\Debug\Project1.dll";

        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern int ReturnIntModify(int value);

        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr ReturnIntArr();


        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr ReturnCharArr();

        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern int ReturnCharArrPointer(string value);


        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr testChar3();

        [DllImport(pathLib, CallingConvention = CallingConvention.Cdecl)]
        static extern int ReturnByteArrPointer(ref IntPtr ptr);


        public Form1()
        {
            InitializeComponent();
            int f = ReturnIntModify(5);

            string s = "";
            string res = null;
            int len = ReturnCharArrPointer(res);

            IntPtr ipttt = testChar3();
            string sss = Marshal.PtrToStringAnsi(ipttt);




            tbOutput.Text = string.Format("{0} \r\n {1} \r\n {2}", f.ToString(), s, sss);



            IntPtr intptr1 = ReturnIntArr();

            int[] arr = new int[4];
            Marshal.Copy(intptr1, arr, 0, arr.Length);



            IntPtr ptr = new IntPtr();
            int length = ReturnByteArrPointer(ref ptr);


        }
    }
}
