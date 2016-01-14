using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Task taskReceive;
        CancellationTokenSource cts;
        UdpClient uc;

        public Form1()
        {
            InitializeComponent();
            rbSender.Checked = true;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (rbSender.Checked && tbPort.Text != string.Empty)
            {
                UdpClient uc = new UdpClient();
                IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, Convert.ToInt32(tbPort.Text));

                byte[] bsend = Encoding.ASCII.GetBytes(rchtbInput.Text);
                uc.Send(bsend, bsend.Length, iep);
                uc.Close();
            }
        }

        void Receive(object cancell)
        {
            CancellationToken ct = (CancellationToken)cancell;

            while (true)
            {
                try
                {
                    if (ct.IsCancellationRequested)
                        ct.ThrowIfCancellationRequested();
                    if (rbReceiver.Checked)
                    {

                        IPEndPoint iep = null;
                        byte[] breceiv = uc.Receive(ref iep);

                        string str = Encoding.ASCII.GetString(breceiv);
                        if (rchtbInput.InvokeRequired)
                            rchtbInput.Invoke(new Action(() => rchtbInput.Text += str));
                        else
                            rchtbInput.Text += str;
                    }
                }
                catch (OperationCanceledException oce)
                {
                    break;
                }
            }



        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (tbPort.Text != string.Empty)
            {
                if (taskReceive == null )
                {


                   uc = new UdpClient(Convert.ToInt32(tbPort.Text));
                    cts = new CancellationTokenSource();
                    taskReceive = new Task(Receive, cts.Token);
                    taskReceive.Start();
                }
                Text = string.Format("Listening port {0}", tbPort.Text);
            }
        }
    }

}