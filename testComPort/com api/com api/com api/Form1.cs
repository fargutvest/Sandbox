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
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace com_api
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateFile(
    lpFileName: "C:",
    dwDesiredAccess: FileAccess.ReadWrite,
    dwShareMode: FileShare.ReadWrite,
    lpSecurityAttributes: IntPtr.Zero,
    dwCreationDisposition: FileMode.OpenOrCreate,
    dwFlagsAndAttributes: FileAttributes.Normal,
    hTemplateFile: IntPtr.Zero);
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);


    

    }
}
