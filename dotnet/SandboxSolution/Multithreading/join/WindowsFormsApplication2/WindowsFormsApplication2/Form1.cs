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
        Thread t1;
      //  Thread t2;
       
        public Form1()
        {
            InitializeComponent();
            
        }



        void v(int v)
        {
            Thread.Sleep(v);
          // textBox1.Invoke(new Action<string>((str)=> textBox1.Text=str),"t1 is ending."); 
        }


    void a()
    {
        t1 = new Thread(delegate() { v(5000); });
        t1.Start();
        t1.Join();
        textBox1.Text += "t1.Join() returned.\r\n";

        t1 = new Thread(() =>
        {
            Thread.Sleep(4000);
            //textBox1.Invoke(new Action<string>((str) => textBox1.Text += str), "t2 is ending.\r\n");     
        });

        t1.Start();
        t1.Join();
        textBox1.Text += "new t1.Join() returned.\r\n";

    }

    private void button1_Click(object sender, EventArgs e)
    {
        a();
    }
   
  }
   
}











