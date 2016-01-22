using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Thread th1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            th1 = new Thread(a);
            th1.Start();
            
            
            
        }

        void a()
        {
            for (int f = 0; f < 3; f++) Thread.Sleep(1000);
            this.Invoke(new Action<string>((s)=>this.Text = s),"dssd"); 
              
          
        }


    }
}
