using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public SerialPort _sp;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenPort("COM1");
            SendCommand();
            Thread.Sleep(500);
            ReadResponse();
            
        }
         public void OpenPort(string comPort)
        {
            
                _sp = new SerialPort();
                _sp.PortName = comPort;
                _sp.BaudRate = 19200;
                //_sp.WriteBufferSize = 2048;
                //_sp.WriteBufferSize = 2048;
                //_sp.RtsEnable = false;
                //_sp.DtrEnable = false;
                _sp.DataBits = 8;
                _sp.StopBits = StopBits.One;
                _sp.Parity =  Parity.Even;
                //_sp.BreakState = false;
               // _sp.Handshake = Handshake.None;
               // _sp.ReadTimeout = 100;
                //_sp.WriteTimeout = 100;
                _sp.RtsEnable = false;
                _sp.DtrEnable = false;
                _sp.Open();
       }

         public void SendCommand()
         {
             byte[] full = new byte[1] { 0x3 };
             _sp.Write(full, 0, full.Length);
         }

         public  void ReadResponse()
         {
             byte[] buffer = new byte[_sp.BytesToRead];
             _sp.Read(buffer, 0, buffer.Length);
             string s="";
             for (int i = 0; i < buffer.Length; i++)
             {
                 s += Convert.ToString((int)buffer[i]);
                 this.Text = s;
             }

      
         }

    }
}
