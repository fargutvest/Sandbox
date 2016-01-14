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
    public delegate void MyDel_lolo(string data);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
       
        }

        void func(string param)
        {
            this.Text = param;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2(new MyDel_lolo(func));
            fm2.ShowDialog();
        }

    }
}
