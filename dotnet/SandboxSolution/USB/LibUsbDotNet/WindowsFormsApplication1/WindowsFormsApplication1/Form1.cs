using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using System.Management;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static UsbDevice MyUsbDevice;
       
        static int vid = 0x0647;
        static int pid = 0x1002;
        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(vid,pid);
        
        public Form1()
        {
            InitializeComponent();
            test();
        }

        void test()
        {
            UsbRegDeviceList urdl = UsbDevice.AllDevices;
            MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

            IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
            wholeUsbDevice.SetConfiguration(1);
            wholeUsbDevice.ClaimInterface(0);
            MyUsbDevice.Open();
            

            UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);
            UsbEndpointWriter writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep01);
            reader.DataReceived += reader_DataReceived;
            reader.DataReceivedEnabled = true;

            string s = "[RI]";
            byte[] b = Encoding.ASCII.GetBytes(s);
            int bytesWritten;
            ErrorCode ec;
            ec = writer.Write(b,100,out bytesWritten);
            byte[] readBuffer = new byte[1024];
            int bytesRead;
            ec = reader.Read(readBuffer, 100, out bytesRead);
        }

        void reader_DataReceived(object sender, EndpointDataEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "";
            if (textBox1.Text.Length ==0) textBox1.Text = "0";
            if (textBox2.Text.Length == 0) textBox2.Text = "0";
            vid = Convert.ToInt32(textBox1.Text, 16);
            pid = Convert.ToInt32(textBox2.Text, 16);
            MyUsbFinder = new UsbDeviceFinder(vid, pid);
            MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);
            if (MyUsbDevice == null)
            {
                this.Text = "null";
            }
            else
            {
                this.Text = "not null";
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > '9' | e.KeyChar < '0' ) & (e.KeyChar != (char)Keys.Back) & e.KeyChar != 'A' & e.KeyChar != 'B' & e.KeyChar != 'C' & e.KeyChar != 'D' & e.KeyChar != 'E' & e.KeyChar != 'F')
            {
                e.Handled = true;
            }
        }

      
    }
}



#region testcode
 /**/
#endregion