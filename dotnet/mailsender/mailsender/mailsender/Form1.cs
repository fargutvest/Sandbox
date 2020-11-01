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
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace mailsender
{
    public partial class Form1 : Form
    {
        Attachment att;
        MailMessage Message = new MailMessage();
        SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 25);
        
        System.Threading.Thread th1;
        System.Threading.Thread th2;
        delegate void Mydeltb5(string s5) ;
        Mydeltb5 deltb5;
        delegate void Mydeltb6(string s6);
        Mydeltb6 deltb6;
        long attach_size = 0; //суммарынй размер вложенных файлов
        string patch; // путь к файлу вложения
        public Form1()
        {
            InitializeComponent();
            read_file();
            Smtp.SendCompleted += new SendCompletedEventHandler(Smtp_SendCompleted);
            Smtp.UseDefaultCredentials = false;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            
            deltb5 = new Mydeltb5(refrtb5);
            deltb6 = new Mydeltb6(refrtb6);

            th2 = new System.Threading.Thread(ShowNetworkTraffic);
            th2.Start();

        }

        void send()
        { 
            try
            {
                Smtp.SendAsync(Message,null);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Error); return;}
        }

        void Smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            
            th1.Abort();
            MessageBox.Show("Отправлено!");
            
        }

        private void button1_Click(object sender, EventArgs e) //send
        {
            Smtp.Credentials = new NetworkCredential(textBox2.Text, textBox3.Text); // мой адрес; мой пароль
            Message.From = new MailAddress(textBox2.Text);
            Message.To.Add(new MailAddress(textBox4.Text));
            string attach_spisok="";
            for (int i = 0; i < Message.Attachments.Count; i++)
            {
                attach_spisok += Message.Attachments[i].Name + " ; ";
            }
            Message.Subject = SystemInformation.UserName + " " + Environment.MachineName + " " + attach_spisok;
            Message.Body = textBox1.Text;
            

            th1 = new System.Threading.Thread(send);
            th1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = null;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")  att = new Attachment(openFileDialog1.FileName);
            patch = openFileDialog1.FileName;
            attach_add();
            
                
        }

        void attach_add() // Добавление вложения
        {
            Message.Attachments.Add(att);
            label2.Text += "\r\n" + att.Name;
            FileInfo file = new FileInfo(patch);
            attach_size += file.Length/1024; // kbyte
            progressBar1.Maximum = Convert.ToInt32(attach_size*2);
            textBox1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + 8);
            button1.Location = new Point(button1.Location.X, button1.Location.Y + 6);
            this.Height += 8; 
        }


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
         
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    patch = objects[0];
                    att = new Attachment(objects[0]);
                    attach_add();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "Я папки не ем!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {

            e.Effect = DragDropEffects.Move;
        }

     

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(15, 15);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Size = new Size(20, 20);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Message.Attachments.Clear();
            attach_size = 0;
            label2.Text = "";
            textBox1.Location = new Point(53, 131);
            button1.Location = new Point(170, 322);
            this.Height = 428; 
        }

        void read_file()
        {
            if (File.Exists("memory") == false) File.Create("memory").Dispose() ;
           
            StreamReader sr = new StreamReader("memory");
            textBox2.Text = sr.ReadLine();
            textBox3.Text = sr.ReadLine();
            textBox4.Text = sr.ReadLine();
            sr.Close();
            sr.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            th2.Abort();

            if (File.Exists("memory") == true) File.Delete("memory");
            File.Create("memory").Dispose();

            StreamWriter sw = new StreamWriter("memory");
            sw.WriteLine(textBox2.Text);
            sw.WriteLine(textBox3.Text);
            sw.WriteLine(textBox4.Text);
            sw.Close();
            sw.Dispose();
            

          
        }



        private void ShowNetworkTraffic()
        {
           
           PerformanceCounter performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", "TP-LINK 150Mbps Wireless Lite N Adapter");
           PerformanceCounter performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", "TP-LINK 150Mbps Wireless Lite N Adapter");


          while(true)
            {
              label5.Invoke(deltb5, (performanceCounterSent.NextValue()/1024).ToString()); // kbyte
               label6.Invoke(deltb6, (performanceCounterReceived.NextValue() / 1024).ToString()); //kbyte
                System.Threading.Thread.Sleep(100);
            }
        }


        int count = 0;
        void refrtb5(string s5)
        {
            label5.Text ="upload: "+ s5;
            count+= Convert.ToInt32(Convert.ToDouble(s5))/10;
            if (progressBar1.Maximum > count) progressBar1.Value = count;
            
            label7.Text = "kbyte: "+count.ToString();
        }
        void refrtb6(string s6)
        {
            label6.Text ="download: " +s6;
            
        }

    }
}


/*
 *           
 */