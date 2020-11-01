using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        string patch = "";
        string tt;
     int o=71;
        public Form1()
        {
            InitializeComponent();
        }
             
        private void Form1_Load(object sender, EventArgs e)
        {
           
                
            }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (var file in Directory.EnumerateFiles(patch, "*.*", SearchOption.AllDirectories))//путь
            {
                //if (file.Length<71)
                //tt = (file.Substring(44,4));


                textBox1.Text += file + "\r\n";
                //  File.Move(file, patch + @"\интерны_" + o + "_2011_SATRip-AVC_SVAT.mkv");
                //    o += 1;               



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            { patch = folderBrowserDialog1.SelectedPath;
            label1.Text = patch;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"f:\123.txt"))
            {
                sw.Write(textBox1.Text);
            }
        }
            
    }
}
