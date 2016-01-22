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
            this.MouseMove+=new MouseEventHandler(Form1_MouseMove);

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = string.Format("X={0}, Y={0}", e.X, e.Y);
        }
    }
}
