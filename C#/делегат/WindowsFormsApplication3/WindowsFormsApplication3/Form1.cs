using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
     delegate void МойДелегат(string a);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            МойДелегат D = new МойДелегат(Функция);
            D("ПРИВЕТ");
        }

          void Функция(string параметр)
    {
        this.Text = параметр;
    }

    }//class
}//namespace




   
