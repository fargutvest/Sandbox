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

        }

    }

    public class a
    {
    }


    public class b
    {
        public void b (a aa)
        {

        }
    }

    public class c
    {
        a a_ = new a();
        b b_ = new b(a_);



    }
}
