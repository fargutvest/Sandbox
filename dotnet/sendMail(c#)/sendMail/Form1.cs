using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace sendMail
{
    public partial class Form1 : Form
    {
        string file = ""; //путь к файлу вложиния
        
        public Form1()
        {
            InitializeComponent();
            button1.LostFocus+=new EventHandler(button1_LostFocus);
         

        }
  
        public void  send()
    {
            SmtpClient smtp = new SmtpClient ("smtp.gmail.com",25);
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential ("fargutvest@gmail.com", "basmakarulllservak");
            MailMessage message = new MailMessage ();
            message.From = new MailAddress("fargutvest@gmail.com"); //адрес отправителя
            message.To.Add(new MailAddress(textBox3.Text)); //адреса получателя
            message.Subject = textBox2.Text; //тема письма
            message.Body = textBox1.Text; //текст письма
           
            //вложение в письмо
            if (file != "")
            {
                Attachment data = new Attachment(file);
                message.Attachments.Add(data);
            }

            smtp.SendAsync(message,null);
       smtp.SendCompleted+=new SendCompletedEventHandler(smtp_SendCompleted);
    }

        private void smtp_SendCompleted(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightGreen;
            button1.Text = "отправлено";
            button1.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            send();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog  ofd = new OpenFileDialog ();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file = ofd.FileName;
            }

 
        }

        private void button1_LostFocus(object sender, EventArgs e) 
        {
            button1.BackColor = System.Drawing.SystemColors.Control;
            button1.Text = "отправить";
            
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }



  

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
            }

        }

      

      

   
    
    
    }
}
