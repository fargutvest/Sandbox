using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        SerialPort _sp;
        public Form1()
        {
            InitializeComponent();
            _sp = new SerialPort("com1", 19200, Parity.None, 8, StopBits.One);
            _sp.ReadTimeout = 500;
            _sp.WriteTimeout = 500;
            _sp.RtsEnable = false;
            _sp.DtrEnable = false;

            //_sp.DataReceived += sp_DataReceived;
            _sp.Open();
            CheckConnected();
        }

        public  Boolean CheckConnected()
        {
            SendCommand(0x3); // Get Firmware Version
            Thread.Sleep(1000);
            byte[] version = ReadResponse();
            if (version == null) return false;
            if (version.Length < 2) return false;
            return true;
        }

        public  void SendCommand(byte b)
        {
            byte[] full = new byte[1] { b };
            //вставить сигнал ошибки если порт закрыт
           // WriteLog(true, full);
            _sp.Write(full, 0, full.Length);
        }

        public  byte[] ReadResponse()
        {
            int msgLen = _sp.BytesToRead;
            if (msgLen == 0)
            {
                return null;
            }
            byte[] buffer = new byte[msgLen];
            _sp.Read(buffer, 0, msgLen);
            //WriteLog(false, buffer);
            return buffer;
        }
    }
}
