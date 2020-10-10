using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = name_1;
            label4.Text = name_2;
            ContextMenu contextMenu1 = new ContextMenu();
            MenuItem menuItem1 = new MenuItem("Выйти", menuItem1_Click);
            contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1 });
            menuItem1.Index = 0;
            this.ContextMenu = contextMenu1;
        }

        void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string fillial_1 = "511/189";
        string name_1 = "Банк";
        string fillial_2 = "511/6";
        string name_2 = "Соседи";
       
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            string pokupka_b="";
            string prodaja_b="";
            string vremja_b = "";
            string pokupka_s = "";
            string prodaja_s = "";
            string vremja_s = "";
            label1.Text = "Покупка:";
            label2.Text = "Продажа:";
            label6.Text = "Покупка:";
            label5.Text = "Продажа:";
            label7.Text = "Время работы:";
            label8.Text = "Время работы:";

            System.Threading.Thread th = new System.Threading.Thread(delegate()
            {
                System.Threading.Thread animth = new System.Threading.Thread(delegate()
                    {
                        string[] anim = new string[] { "/", "--", @"\", @"|" };
                        int i = 0;
                        while (true)
                        {
                            i++;
                            if (i > 3) i = 0;
                            button1.Invoke(new Action<string>((ff) => button1.Text = ff), anim[i]);
                            System.Threading.Thread.Sleep(100);
                        }

                    });
                animth.Start();

                var w = new WebClient();
                w.Proxy = new WebProxy();
                var s = w.DownloadString("http://belarusbank.by/ru/fizicheskim_licam/valuta/kursy-valyut");
                //button1.Invoke(new Action<string>((dd) => button1.Text = dd), "eee");
                try
                {
                    var buff_b = s.Substring(s.IndexOf(fillial_1), 550); 
                    pokupka_b = buff_b.Substring(buff_b.IndexOf("<td align=\"center\">") + 19, 4);
                    prodaja_b = buff_b.Substring(buff_b.IndexOf("<td align=\"center\" nowrap=\"1\">") + 30, 4);
                    vremja_b = buff_b.Substring(buff_b.IndexOf("<span>") + 6, buff_b.IndexOf("</span>") - buff_b.IndexOf("<span>") - 6).Replace("<b>", "").Replace("</b>", "").Replace("<span> ", "\n");
                    label1.Invoke(new Action<string>((ss) => label1.Text = ss), "Покупка: " + pokupka_b);
                    label2.Invoke(new Action<string>((ss) => label2.Text = ss), "Продажа: " + prodaja_b);
                    label7.Invoke(new Action<string>((ss) => label7.Text = ss), "Время работы: \r\n" + vremja_b);
                }
                catch (Exception ex) 
                {
                    label1.Invoke(new Action<string>((ss) => label1.Text = ss), "Покупка: закрыто");
                    label2.Invoke(new Action<string>((ss) => label2.Text = ss), "Продажа: закрыто");
                }
                try
                {
                    var buff_s = s.Substring(s.IndexOf(fillial_2), 550);
                    pokupka_s = buff_s.Substring(buff_s.IndexOf("<td align=\"center\">") + 19, 4);
                    prodaja_s = buff_s.Substring(buff_s.IndexOf("<td align=\"center\" nowrap=\"1\">") + 30, 4);
                    vremja_s = buff_s.Substring(buff_s.IndexOf("<span>") + 6, buff_s.IndexOf("</span>") - buff_s.IndexOf("<span>") - 6).Replace("<b>", "").Replace("</b>", "").Replace("<span> ", "\n");
                    label6.Invoke(new Action<string>((ss) => label6.Text = ss), "Покупка: " + pokupka_s);
                    label5.Invoke(new Action<string>((ss) => label5.Text = ss), "Продажа: " + prodaja_s);
                    label8.Invoke(new Action<string>((ss) => label8.Text = ss), "Время работы: \r\n" + vremja_s);
                }
                catch (Exception ex)
                {
                    label6.Invoke(new Action<string>((ss) => label6.Text = ss), "Покупка: закрыто" );
                    label5.Invoke(new Action<string>((ss) => label5.Text = ss), "Продажа: закрыто" );
                    animth.Abort();
                    button1.Invoke(new Action<string>((hhh) => button1.Enabled = true), "eee");
                    button1.Invoke(new Action<string>((ff) => button1.Text = ff), "Обновить");
                    return;
                }
                animth.Abort();
                 button1.Invoke(new Action<string>((hhh)=>button1.Enabled = true),"eee");
                 button1.Invoke(new Action<string>((ff) => button1.Text = ff),"Обновить");
                 
            });

            th.Start();
            
        }

        bool keyDown = false;
        int x=0;
        int y=0;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!keyDown) return;
            this.Left += e.X - x;
            this.Top += e.Y - y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            keyDown = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            keyDown = true;
            x = e.X;
            y = e.Y;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            comboBox1.Visible = true;
            comboBox1.DroppedDown = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            comboBox2.Visible = true;
            comboBox2.DroppedDown = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = comboBox2.Text;
            comboBox2.Visible = false;
            label4.Visible = true;
            fillial_2 = comboBox2.Text;
            if (comboBox2.Text == "511/6") label4.Text = "Соседи";
            if (comboBox2.Text == "511/189") label4.Text = "Банк";   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = comboBox1.Text;
            comboBox1.Visible = false;
            label3.Visible = true;
            fillial_1 = comboBox1.Text;
            if (comboBox1.Text == "511/6") label3.Text = "Соседи";
            if (comboBox1.Text == "511/189") label3.Text = "Банк";
            
        }

        
        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.ForeColor = Color.Coral;
         
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
         
        }

        
        private void label4_MouseMove(object sender, MouseEventArgs e)
        {

            label4.ForeColor = Color.Coral;
           
        }

       

        private void label4_MouseLeave_1(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://belarusbank.by/ru/fizicheskim_licam/valuta/kursy-valyut");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

      
        

    }
}
