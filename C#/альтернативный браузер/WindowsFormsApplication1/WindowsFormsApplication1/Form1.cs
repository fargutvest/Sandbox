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
        Skybound.Gecko.GeckoWebBrowser browser;
        public Form1()
        {
            InitializeComponent();
            string pach = "xulrunner";
            Skybound.Gecko.Xpcom.Initialize(pach);
            browser = new Skybound.Gecko.GeckoWebBrowser();
            browser.Parent = this.panel1;
          browser.Dock = DockStyle.Fill;
          browser.Click += new EventHandler(browser_Click);
          browser.MouseClick += new MouseEventHandler(browser_MouseClick);
        }

        void browser_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = browser.PointToClient(Cursor.Position);
        }

        void browser_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Navigate("http://belarusbank.by/ru/fizicheskim_licam/valuta/kursy-valyut?filter");
           
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Skybound.Gecko.GeckoElementCollection el = browser.Document.GetElementsByTagName("button");

        }
    }
}
