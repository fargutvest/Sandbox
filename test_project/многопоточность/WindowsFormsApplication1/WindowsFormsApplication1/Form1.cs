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
        
        System.Threading.Thread t1 = new System.Threading.Thread(potok1);
        
        delegate void Del(string text);

        public Form1()
        {
       
            textBox1.Invoke(new Del((s) => textBox1.Text = s), "newText");
            InitializeComponent();
            t1.Start();
           
        }
        
       
      
    
        static void potok1()
        {
  
        }

  
    
    }
}
