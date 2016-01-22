using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            
         
        }
        delegate int y(int ar);
        void test()
        {
            int t = 0;

            label1.Invoke(new Action<int>(i => t = i), 15);

            int iko = 0;
            y myY = parampampam => iko = parampampam;
            int iiio = myY(158);

            y ggg = fuk => 38 * 39;
            int res = ggg(10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }
    }
}


/*
 лямбда выражение:
 delegate тип имя_делегата(тип имя параметра)
 имя делегата = параметр => что сделать;
 */