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
        int dolo = 0;
        public Form1()
        {
            InitializeComponent();
            
        }


        void func()
           
        {
            for (int i = 0; i < 10000; i++) { }
            Form2 fm2 = new Form2();
            fm2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(func);
            th.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.ShowDialog();
            dolo +=1;
            label1.Text = " " + dolo;
        }

    }
}
