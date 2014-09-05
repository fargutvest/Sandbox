using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ns
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            test();
        }

        void test() 
        {
            MySingleton<a>.Instance.method();

            this.Text = MySingleton<a>.Instance.ToString();
            
            
        }
    }

    public class a 
    {
        public void method()
        {

        }
    }

    public class b
    {
   
    }


}
