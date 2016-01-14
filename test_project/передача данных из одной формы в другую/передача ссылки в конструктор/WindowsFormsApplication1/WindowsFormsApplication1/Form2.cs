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
    public partial class Form2 : Form
    {
        Form f;
        public Form2(Form1 f1)
        {
            InitializeComponent();
            f = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.Text = textBox1.Text;
        }
    }
}
