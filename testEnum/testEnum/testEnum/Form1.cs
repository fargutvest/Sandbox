using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testEnum
{
    public partial class Form1 : Form
    {

    
        enum MyEnum 
        {
            Avtobus = 1,
            Avtomobil = 0,
            Samosval = 2,
            Trolleibus = 1
        }
        public Form1()
        {
            InitializeComponent();
            test();
        }

        void test()
        {
            MyEnum myEnum1 = (MyEnum)1;
            MyEnum myEnum2 = (MyEnum)1;

            string s1 = myEnum1.ToString();
            string s2 = myEnum2.ToString();
            if (myEnum1 == myEnum2)
            {
                 //Enum.Parse(typeof(eTPCMD), xmlReader.Value)
                richTextBox1.AppendText(Enum.GetName(typeof(MyEnum), myEnum1)+"\r\n");
                richTextBox1.AppendText(Enum.GetName(typeof(MyEnum), myEnum2)+"\r\n");
             
            }

        }
    }
}
