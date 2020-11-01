using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;

namespace mailsender
{
    public partial class Form1 : Form
    {
        Attachment att;
        public Form1()
        {
            InitializeComponent();
        }

        void send()
        {
            SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
            Smtp.UseDefaultCredentials = false;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            Smtp.Credentials = new NetworkCredential("fargutvest@gmail.com", "371988343");
      
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("fargutvest@gmail.com");
            Message.To.Add(new MailAddress(textBox1.Text));
            Message.Subject = SystemInformation.UserName + " "+Environment.MachineName; 
            Message.Body = "=)";
            try { Message.Attachments.Add(att); }
            catch (ArgumentNullException) { MessageBox.Show("файл не выбран"); return; }
            Smtp.Send(Message);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            send();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = null;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
             att = new Attachment(label2.Text = openFileDialog1.FileName); 
            
                
        }
    }
}
