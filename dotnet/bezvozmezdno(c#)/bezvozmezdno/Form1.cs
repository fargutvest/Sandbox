using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace bezvozmezdno
{
    public partial class Form1 : Form
    {
        string pach, message_id, html, textovka, a2;
        int pos_mess, a1, i, sec;
        WebClient client = new WebClient();

        public Form1()
        {
            InitializeComponent();
            
        }
        private void obrabotka()
        {
            DataGridView1.Rows.Clear();
            pos_mess = 1; //вхождние message_ в строку html
            a1 = 1;
            for (int i = 0; i < 20; i++) //обычно на форуме 20 сообщений 
            {
                a2 = html.Substring(html.IndexOf("message_", a1+8)+7,8);
                if (Convert.ToInt32(a2) > Convert.ToInt32(message_id))
             message_id = a2;
                i+=1;
                a1 = html.IndexOf("message_",a1+8);
                DataGridView1.Rows.Add();
                DataGridView1.Rows[i].Cells[0].Value = i + 1; //нумерация строк

                    pos_mess = html.IndexOf("message_", pos_mess);
                    if (pos_mess == 0) break; // сообщений нет
                    a1 = pos_mess;
                    
                    


                    textovka = html.Substring(html.IndexOf("message_", pos_mess) - 1, 500);

                    DataGridView1.Rows[i].Cells[1].Value = textovka;
                    // message_id = html.Substring(html.IndexOf("message_", pos_mess) + 7, 8);
                    pos_mess += 8;
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
    
    
    
    }
}
