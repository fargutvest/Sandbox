using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibrary1;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var p = new Class1();
           /* p.Name = "Andrey";
            p.Firstname = "Andreey";
            p.Age = 27;*/
            this.Text = p.Name + " " + p.Firstname + " " + p.Age.ToString();
        }

    }
}
