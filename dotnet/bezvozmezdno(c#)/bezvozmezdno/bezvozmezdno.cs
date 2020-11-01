using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace bezvozmezdno
{
    public partial class Form1 : Form
    {
        string pach, message_id, html, textovka, a2;
        int pos_mess;
        WebClient client = new WebClient();

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void obrabotka()
        {
            DataGridView1.Rows.Clear();
            pos_mess = 1; //вхождние message_ в строку html
            for (int i = 0; i < 20; i++) //обычно на форуме 20 сообщений 
            {
                message_id = html.Substring(html.IndexOf("message_", pos_mess+8)+8,8);
                if (message_id!="PE html ") // коретка поиска еще не вернулась в начало html документа
                {
                    if (Convert.ToInt32(message_id) > Convert.ToInt32(a2))
                        a2 = message_id;
                pos_mess = html.IndexOf("message_",pos_mess+8);
                DataGridView1.Rows.Add();
                DataGridView1.Rows[i].Cells[0].Value = i ; //нумерация строк
                textovka = html.Substring(pos_mess - 1, 500); // (-1) захватим еще и кавычку "
                DataGridView1.Rows[i].Cells[1].Value = textovka;
        
                }
                }
        }
        

        private void gethtml()
        {
           
            pach = TextBox2.Text + "00"; // адрес к ветке форума forum.onliner.by безвозмездно тоесть даром 
            client.Encoding = System.Text.Encoding.UTF8;
            html = client.DownloadString(pach);   
        }

        private void Button3_Click(object sender, EventArgs e) // refresh
        {
            gethtml();
            obrabotka();
        }

        private void timer1_Tick(object sender, EventArgs e) //интервал 5 мин
        {
            gethtml();
            obrabotka();
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.ForeColor = Color.Blue;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start(TextBox2.Text + "00");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.textBox1.Text = html;
            fm2.Show();
        }
    
    
    
    }
}
