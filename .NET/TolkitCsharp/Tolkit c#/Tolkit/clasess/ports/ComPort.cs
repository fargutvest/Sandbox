using System;
using System.Collections.Generic;
using System.IO.Ports;


namespace Tolkit
{
    public class ComPort : IPortGeneral
    {
        SerialPort sp;
        List<byte[]> TempBuffer = new List<byte[]>();
        
        public event Action DataReceived;
        public event EventHandler EvOpened;
        public event EventHandler EvClosed;
        byte[] ReadBuffer;

        public bool DTR
        {
            set
            {
                if (sp != null)
                    sp.DtrEnable = value;
            }
        }


        public bool RTS
        {
            set
            {
                if (sp != null)
                    sp.RtsEnable = value;
            }
        }

        public ComPort(string portName, int BaudRate, Parity paity)
        {
            
            sp = new SerialPort(portName, BaudRate);
            sp.Parity = Parity.Even;
            sp.StopBits = StopBits.One;
            sp.DataBits = 8;
            sp.Handshake = Handshake.None;
            sp.DataReceived += sp_DataReceived;
        }

        public ComPort(string portName, int BaudRate)
        {
            sp = new SerialPort(portName, BaudRate);
            sp.Parity = Parity.Even;
            sp.StopBits = StopBits.One;
            sp.DataBits = 8;
            sp.Handshake = Handshake.None;

            sp.DataReceived += sp_DataReceived;
        }



        public void Open()
        {

            if (!sp.IsOpen)
            {
                sp.Open();
            }

            if (EvOpened != null)
            {
                EvOpened(null, null);
            }

        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] bytes = default(byte[]);
            bytes = new byte[sp.BytesToRead];
            sp.Read(bytes, 0, bytes.Length);
            TempBuffer.Add(bytes);

            if (bytes.Length != 0)
            {
                if (TempBuffer.Count == 1) //проверка на признак конца строки
                {
                    //подсчет длинны ReadBuffer
                    int len = 0;
                    for (int i = 0; i < TempBuffer.Count; i++)
                    {
                        len += TempBuffer[i].Length;
                    }
                    ReadBuffer = new byte[len];
                    //Наполнение ReadBuffer
                    int offset = 0;
                    for (int i = 0; i < TempBuffer.Count; i++)
                    {
                        Array.Copy(TempBuffer[i], 0, ReadBuffer, offset, TempBuffer[i].Length);
                        offset += TempBuffer[i].Length;
                    }
                    TempBuffer.Clear();

                    if (DataReceived != null)
                        DataReceived();
                    
                }
            }
        }

        public void Close()
        {
            if (sp != null)
            {
                sp.Close();
            }

            if (EvClosed != null)
            {
                EvClosed(null, null);
            }
        }


        public bool IsOpen
        {
            get
            {
                return sp.IsOpen;
            }
        }

        /// <summary>
        /// Отправляет байты в COM порт
        /// </summary>
        /// <param name="b"></param>
        public void Write(byte[] b)
        {
            sp.Write(b, 0, b.Length);
        }


        public byte[] Read()
        {
            return ReadBuffer;
        }
        
        public void Dispose()
        {
            if (sp != null)
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
                sp.Dispose();
                sp = null;
            }
        }

    }
}


