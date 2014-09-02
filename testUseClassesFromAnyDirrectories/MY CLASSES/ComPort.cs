using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;


namespace Adani
{
    class ComPort :  AbstractPort, IDisposable, IPort
    {
        SerialPort _sp;
        List<byte[]> TempBuffer = new List<byte[]>();
        byte[] ReadBuffer;
        public override event EventHandler DataReceived;
        public event EventHandler EvOpened;
        public event EventHandler EvClosed;
        AutoResetEvent AreDalay = new AutoResetEvent(false);
        int delayAfterOpen = 500;

        public ComPort(string _name, int _bautrate)
        {
            _sp = new SerialPort();
            _sp.PortName = _name;
            _sp.BaudRate = _bautrate;
            _sp.Parity = Parity.Even;
            _sp.DataBits = 8;
            _sp.StopBits = StopBits.One;
            _sp.DtrEnable = true;
            _sp.RtsEnable = true;
            

            _sp.DataReceived += sp_DataReceived;
        }

        public override void PortChange(string port)
        {
            if (_sp!= null)
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();
                }
                _sp.PortName = port;
            }
        }
        public override void open()
        {
            try
            {
                if (!_sp.IsOpen)
                {
                    _sp.Open();
                }

                AreDalay.WaitOne(delayAfterOpen);

                if (EvOpened != null)
                {
                    EvOpened(null, null);
                }
            }
            
                
            catch (Exception ex) { ExceptionLog.log(ex); }
        }

       void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                ReadBuffer = new byte[_sp.BytesToRead];
                _sp.Read(ReadBuffer, 0, ReadBuffer.Length);
                if (DataReceived != null)
                {
                    DataReceived(null, null);
                }
            }
            catch (Exception ex) { ExceptionLog.log(ex); return; }
            TempBuffer.Add(ReadBuffer);
        }

        public override void close()
        {
            if (_sp != null)
            {
                _sp.Close();
                _sp.Dispose();
                _sp = null;
            }

            if (EvClosed != null)
            {
                EvClosed(null, null);
            }
        }

        public override bool IsOpen()
        {
            return _sp.IsOpen;
        }

        /// <summary>
        /// Отправляет байты в COM порт
        /// </summary>
        /// <param name="b"></param>
        public override void write(byte[] b)
        {
            try
            {
                _sp.DiscardInBuffer();
                _sp.DiscardOutBuffer();
                _sp.Write(b, 0, b.Length);
            }
            catch (Exception ex) { ExceptionLog.log(ex); }
        }

        /// <summary>
        /// Возвращает буффер чтения COM порта
        /// </summary>
        /// <returns></returns>
        public override byte[] read()
        {
            byte[] bOut = ReadBuffer;
            ReadBuffer = default(byte[]);
            return bOut;
        }

        public override void Dispose()
        {
            if (_sp != null)
            {
                if (_sp.IsOpen)
                {
                    _sp.DataReceived -= sp_DataReceived;
                    _sp.Close();
                }
                _sp.Dispose();
                _sp = null;
            }
        }


    }
}
