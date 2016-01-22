using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Thread th1;
        System.Timers.Timer tm1;
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            //pictureBox1.Image = WindowsFormsApplication1.Properties.Resources.Desert;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "";
            //th1 = new Thread(delegate() { play(3000); });
            th1 = new Thread(delegate() { play("port"); });
            th1.Start();
            th1.Join();
            
            this.Text = "complete "+i;
        }

        //поток вызывает таймер
        void play(int i)
        {
            //Thread.Sleep(3000);
            tm1 = new System.Timers.Timer(i);
            tm1.Elapsed += new System.Timers.ElapsedEventHandler(tm1_Elapsed);
            tm1.Start();
            while (tm1.Enabled)
            { }
            
        }

        void tm1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            i = 5;
            tm1.Stop();
        }
        SerialPort port = new SerialPort();
        //поток запускает обмен с ком портом
        void play(string s)
        {
            
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.PortName = "COM25";
            port.BaudRate = 300;
            port.Parity = Parity.Even;
            port.StopBits = StopBits.One;
            port.DataBits = 7;
            port.Open();

            string[] splitarray = "2F 3F 21 0D 0A".Split(new char[] { ' ' }); // разбиваем слово по буквам в массив
            byte[] bufferwrite = new byte[splitarray.Length];  // обьявляем массив буфера байт записи 
            for (int i = 0; i < splitarray.Length; i++)
            {
                byte.TryParse(splitarray[i], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out bufferwrite[i]); // кладем массив букв в массив байт записи
            }
            port.Write(bufferwrite, 0, bufferwrite.Length);
            while (port.IsOpen)
            {
            }
            //Thread.Sleep(3000);    
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int a = 0;
            int b = 0;
            int c = a + b;
            Thread.Sleep(3000);
            i = 3;
            port.Close();
        }

        void play()
        {
            Thread th2 = new Thread(() => 
            {
                Thread.Sleep(3000);
                int r = 0;
            });
            th2.Start();
        }


    }
}
