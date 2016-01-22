using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebClient wbcl = new WebClient();
        string mem1 = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                Thread animth = new Thread(delegate()
                {
                    string[] anim = new string[] { "--", @"\", @"|", "/" };
                    int j = 0;
                    while (true)
                    {
                        j++;
                        if (j == 4) j = 0;
                        label1.Invoke(new Action<string>((lll) => label1.Text = lll), anim[j]);
                        Thread.Sleep(100);
                    }

                });
                animth.Start();

                wbcl.Encoding = System.Text.Encoding.UTF8;
                string uri = "http://forum.onliner.by/viewtopic.php?t=1051943&start=138800000000000000000000";
                wbcl.Proxy = new WebProxy();
                string htm ="";
                
                try
                {
                    if (textBox2.Text == "") textBox2.Invoke(new Action<string>((s) => textBox2.Text = s), uri);
                        htm = wbcl.DownloadString(textBox2.Text);
                     
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return; }

                string last_post = htm.Substring(htm.LastIndexOf("id=\"message_"), (htm.IndexOf("</p>", htm.LastIndexOf("id=\"message_")) - htm.LastIndexOf("id=\"message_") + 4));
                string id_message = last_post.Substring(12, last_post.IndexOf("\">") - 12);
                last_post = last_post.Substring(last_post.IndexOf("<p>") + 3, last_post.IndexOf("</p>") - last_post.IndexOf("<p>") - 3).Replace("<strong>", "").Replace("</strong>", "");
                textBox1.Invoke(new Action<string>((ss) => textBox1.Text = ss), last_post);
                if (id_message != mem1)
                {
                    mem1 = id_message;
                    textBox1.Invoke(new Action<string>((qqq) => textBox1.BackColor = Color.LightGreen), "");
                }
                else textBox1.Invoke(new Action<string>((qqq) => textBox1.BackColor = Color.White), "");
                animth.Abort();
            });

            

            th.Start();
            
            
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.ForeColor = Color.Blue;
          
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox2.Text);   
        }

       
    }
}
