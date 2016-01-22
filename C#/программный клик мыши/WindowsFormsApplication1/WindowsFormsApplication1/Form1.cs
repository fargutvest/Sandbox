using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
      [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

      private const UInt32 MOVE = 0x0001;
      private const UInt32 LEFTDOWN = 0x0002;
      private const UInt32 LEFTUP = 0x0004;
      private const UInt32 RIGHTDOWN = 0x0008;
      private const UInt32 RIGHTUP = 0x0010;
      private const UInt32 MIDDLEDOWN = 0x0020;
      private const UInt32 MIDDLEUP = 0x0040;
      private const UInt32 WHEEL = 0x0800;
      private const UInt32 ABSOLUTE = 0x8000;
        
        public Form1()
        {
            InitializeComponent();
            Point p = new Point(100, 100);
            Cursor.Position = p;
            LeftClick();
      }

        public static void LeftClick()
        {
            mouse_event(RIGHTDOWN | RIGHTUP, 0, 0, 0, IntPtr.Zero);
            
        }


       

    }
}
