using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Skybound.Gecko.GeckoWebBrowser browser;
        public Form1()
        {
            InitializeComponent();
            string pach = $@"{Environment.CurrentDirectory}\..\..\..\..\doc\xulrunner-1.9.2.en-US.win32.sdk\xulrunner-sdk\bin\";
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
            browser.Navigate("http://google.com");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Skybound.Gecko.GeckoElementCollection el = browser.Document.GetElementsByTagName("button");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(this, null);
        }
    }
}
