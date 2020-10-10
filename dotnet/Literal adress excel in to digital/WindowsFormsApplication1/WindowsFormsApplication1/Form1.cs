using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void f()  //из числа в буквы
        {
            List<byte> b = new List<byte>();
            int a = Convert.ToInt32(textBox1.Text);
            int r;
            if (a % 26 == 0) r = a - 1;
            else r = a;
            int t;
            while (r > 26)
            {
                t = r / 26;
                if (r % 26 == 0) t -= 1;
                b.Add(Convert.ToByte(r - t * 26));
                r = t;
            }

            if (r < 27) b.Add(Convert.ToByte(r));

            if (a % 26 == 0) b[0] += 1;
            string s = "";
            for (int i = b.Count - 1; i >= 0; i--)
            {
                s += Convert.ToChar(b[i] + 64).ToString();
            }
            textBox2.Text = s;
            textBox2.Refresh();
        }

        void f(string s) //из букв в число
        {
            byte[] bbb = Encoding.Default.GetBytes(s);
            int dd=0;
            for (int i = bbb.Length-1, e=0; i >=0 ; i--, e++)
            {
                bbb[i] -= 64;
                dd += bbb[i] * (int)Math.Pow(26,e);
            }
            textBox1.Text = dd.ToString();
            textBox1.Refresh();

        }






        private void button1_Click(object sender, EventArgs e)
        {
            /*for (int i = 1; i < 100001; i++)
            {
                textBox2.Text = i.ToString();
                textBox2.Refresh();
                f();
                //System.Threading.Thread.Sleep(3);
                if ((textBox1.Text != textBox2.Text) |textBox3.Text.IndexOf("@")!=-1) return;
            }*/
            //f();

            for (int a = 0x41; a < 0x5b; a++)
            {
                for (int b = 0x41; b < 0x5b; b++)
                {
                    for (int c = 0x41; c < 0x5b; c++)
                    {
                        textBox3.Text = ((char)a).ToString() + ((char)b).ToString() + ((char)c).ToString();
                        textBox3.Refresh();
                        f(textBox3.Text);
                        f();
                        System.Threading.Thread.Sleep(3);
                        if ((textBox3.Text != textBox2.Text) | textBox2.Text.IndexOf("@") != -1) return;
                     
                    }
                }
            }



        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
