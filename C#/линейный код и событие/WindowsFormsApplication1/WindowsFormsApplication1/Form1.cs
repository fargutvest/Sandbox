using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Form2 fm2 = new Form2();
        public Form1()
        {
            InitializeComponent();

            Thread th1 = new Thread(b);
            th1.Start();
                
        }
        void a()
        {
            textBox1.Text = "1";
            Thread.Sleep(1000);
            textBox1.Refresh();
            textBox1.Text = "2";
            Thread.Sleep(1000);
            textBox1.Refresh();
            textBox1.Text = "3";
            Thread.Sleep(1000);
            textBox1.Refresh();
            textBox1.Text = "4";
            while (fm2.fl != true)
            {
            }

            textBox1.Refresh();
            textBox1.Text = "5";
            Thread.Sleep(1000);
            textBox1.Refresh();
            textBox1.Text = "6";
            Thread.Sleep(1000);
            textBox1.Refresh();
            textBox1.Text = "7";
            Thread.Sleep(1000);
            textBox1.Refresh();
            textBox1.Text = "8";
            Thread.Sleep(1000);
            textBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a();
        }

        void b()
        {
            fm2.ShowDialog();
        }

        
    }
}
