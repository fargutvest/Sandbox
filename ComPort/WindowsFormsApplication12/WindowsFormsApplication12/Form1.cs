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

namespace WindowsFormsApplication12
{
    public partial class Form1 : Form
    {
        SerialPort sp = new SerialPort("COM1", 19200, Parity.Even, 8, StopBits.One);
        public Form1()
        {
            InitializeComponent();

            sp.DataReceived += sp_DataReceived;
            sp.Open();
        }

        protected void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> ListReadBuff = new List<byte>();
       
            try
            {
                while (sp.BytesToRead != 0)
                {
                    ListReadBuff.Add((byte)sp.ReadByte());
                    if (sp.BytesToRead == 0)
                    {
                        Thread.Sleep(200);
                    }
                }
                ASCIIEncoding encoding = new ASCIIEncoding();
               this.Invoke(new Action<string>((s)=>this.Text = s), encoding.GetString(ListReadBuff.ToArray()) );
            }


                /*
            string hello  = encoding.GetString(bytedata);
                 */
            catch (Exception ex)
            {
                return;
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sp.Write(new byte[] { 0x3 }, 0, 1);
        }


    }
}
