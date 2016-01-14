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
    delegate void Mydel_kuku(string t);   
    
    public partial class Form1 : Form
    {
        Mydel_kuku kuku;
        Thread th;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            th.Abort();
        }

        void metod(string param)
        {
            textBox1.Text += param +";";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kuku = new Mydel_kuku(metod);
            th = new Thread(potok_metod);
            th.Start();
        }

        void potok_metod()
        {
            int i = 0;
     
            while (true)
            {
                Thread.Sleep(1000);
                i += 1;
                textBox1.Invoke(kuku,i.ToString());
             
            }

        }
    }
}
