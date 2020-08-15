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
        Form2 f2;
        public Form1()
        {
            InitializeComponent();
             f2 = new Form2(this);
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f2.Text = textBox1.Text;
        }
    }
}
