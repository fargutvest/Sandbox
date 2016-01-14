using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Thread(_Move).Start();
        }
        int i;
        void _Move()
        {
            this.Move += (sender, e) => { ++i; this.Text = i.ToString(); };
        }
    }
}
