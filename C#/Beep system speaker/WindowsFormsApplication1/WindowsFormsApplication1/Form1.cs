using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Windows.Forms;
	using System.Runtime.InteropServices;
	  
	namespace Sample
	{
	    public partial class Form1 : Form
	    {
	        public Form1()
	        {
	            InitializeComponent();
            }
	  
	        [DllImport("kernel32.dll")]

	        public static extern bool Beep(int BeepFreq, int BeepDuration);
	  
	        private void button1_Click(object sender, EventArgs e)
	        {
	            Beep(5000, 1000);
	        }

	    }
	}