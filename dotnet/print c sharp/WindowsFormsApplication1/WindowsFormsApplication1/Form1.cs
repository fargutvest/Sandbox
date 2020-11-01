using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



     
     


        void PRD(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font drawFont = new Font("Arial", 16);
           // Image newimage = Image.FromFile("f.jpg");
            PointF rara = new PointF(200.0F, 150.0F);
            g.DrawString(textBox1.Text, drawFont, new SolidBrush(Color.Red), 100, 100);
           // g.DrawImage(newimage, rara);



        }

      
        private void key_press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
            File.Delete("c:/Users/Геннадий/Documents/document.pdf");
            //  printDialog1.ShowDialog();
            PrintDocument def = new PrintDocument();
            def.PrintPage += new PrintPageEventHandler(PRD);
            //  def.DocumentName = "Document1";
            // def.PrinterSettings = printDialog1.PrinterSettings;
            def.Print();
            }
        }


    }
}
