using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBHIDDRIVER;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            test();
        }
        void test()
        {
            USBInterface usb = new USBInterface("vid_046D","pid_C05B");
            usb.Connect();
            usb.write(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }
}
