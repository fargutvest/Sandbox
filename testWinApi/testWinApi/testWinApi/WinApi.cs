using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace testWinApi
{
    /// <summary>
    /// Класс обертка для WinApi
    /// </summary>
    public static class WinApi
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto,
    CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr SecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = true)]
        public static extern bool WriteFile(
             IntPtr hFile,
    System.Text.StringBuilder lpBuffer,
    uint nNumberOfBytesToWrite,
    out uint lpNumberOfBytesWritten,
    [In] ref System.Threading.NativeOverlapped lpOverlapped
        );

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        public static extern int mciSendString(string lpstrCommand,
        string lpstrReturnString, int uReturnLength, int hwndCallback);



    }
}
