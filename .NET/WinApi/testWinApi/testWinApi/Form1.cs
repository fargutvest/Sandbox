using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace testWinApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void CreateFile()
        {
            SafeFileHandle handle = WinApi.CreateFile(
                 @"\\.\D:\fffffffffff.txt", (uint)FileAccess.Read, (uint)FileShare.ReadWrite,
    IntPtr.Zero, (uint)FileMode.OpenOrCreate, (uint)FileAttributes.Normal,
     IntPtr.Zero);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            WinApi.mciSendString("set CDAudio door open", null, 127, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WinApi.mciSendString("set CDAudio door closed", null, 127, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateFile();
        }
    }
}
