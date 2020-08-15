using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlwaysOnTopAnyWindow
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);


        private Task _task;
        private IntPtr _handle;
        public Form1()
        {
            InitializeComponent();
            _task = Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    BringWindowToTop(_handle);
                    Task.Delay(1).Wait();
                }
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _handle = Process.GetProcessById(Int32.Parse(textBox1.Text)).Handle;
        }


    }
}
