using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        const uint WM_KEYDOWN = 0x0100;


        [DllImport("Project1.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void TestPost(int id);

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string pName = tbProcessName.Text;

            //Process[] processes = Process.GetProcessesByName(pName);
            try
            {
                Process process = Process.GetProcessById(Convert.ToInt32(tbProcessName.Text));
                //Main part
                PostMessage(process.MainWindowHandle, WM_KEYDOWN, (int)Keys.W, 0);

            }
            catch (Exception ex) { return; }

        }

        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Process process = Process.GetProcessById(Convert.ToInt32(tbProcessName.Text));
                //Main part
                PostMessage(process.MainWindowHandle, WM_KEYDOWN, (int)e.KeyChar, 0);

            }
            catch (Exception ex) { return; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestPost(Convert.ToInt32(tbProcessName.Text));
        }
    }
}
