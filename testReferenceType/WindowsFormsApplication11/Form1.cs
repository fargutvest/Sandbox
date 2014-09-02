using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        string[] a = new string[] { "a", "b", "c" };
        string[] b = new string[] { "d", "e", "f" };
        public Form1()
        {
            InitializeComponent();
            a = b;
            b[1] = "x";
            this.Text = a[0] + a[1] + a[2];
        }
    }
}
