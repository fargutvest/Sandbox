using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(str.ToUpper());//верхний регистр
            textBox1.Text = textBox1.Text.ToUpper();
            string sd, ss;
            sd = new string(textBox1.Text.Where(ch => !char.IsLetter(ch)).ToArray());
            ss = new string(textBox1.Text.Where(ch => !char.IsDigit(ch)).ToArray());
            
            int dstr = 0;
            int ch1 = 0;
            int i,y;
            for (i = 0, y = ss.Length - 1; i < ss.Length; i++, y--)
            {
                
                //ch1 = (int)sd[i];
                dstr += ((int)(char)ss[i]-64)*(int)Math.Pow(26,y);
            }
            textBox2.Text = sd + "/" + dstr.ToString();
            string r3, r2, r1="";
            y = dstr / 676;
            dstr = dstr - y * 676;
            if (dstr == 0 & y > 0)
            {
                r1 = Convert.ToString((char)(y + 63)) + "ZZ";
                textBox3.Text = r1;
                return;
            }
            else
            {
                if (y > 0) r1 = Convert.ToString((char)(y + 64));
                y = dstr / 26;
                dstr = dstr - y * 26;
                if (dstr == 0 & y > 0)
                {
                    r1 = Convert.ToString((char)(y + 63)) + "Z";
                    textBox3.Text = r1;
                    return;
                }
                else
                {

                    if (y > 0) r1 += (char)(y + 64);
                    y = dstr - y * 26;
                    if (y > 0) r1 += (char)(y + 64);
                }
            }

            textBox3.Text = r1;
            
        }
    }
}
/*
            if (y == 2)
            {
                r1 = Convert.ToString((char)(y + 63));
                y--;
            }
            else
            {
                if (y > 0) r1 = Convert.ToString((char)(y + 64));
            }
            dstr = dstr- y*676;
            y = dstr  / 26;
            if (y == 2)
            {
                r1 = Convert.ToString((char)(y + 63));
                y--;
            }
            else
            {
                if (y > 0) r1 = Convert.ToString((char)(y + 64));
            }*/