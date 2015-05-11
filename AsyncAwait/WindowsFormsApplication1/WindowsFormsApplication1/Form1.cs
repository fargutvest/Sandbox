using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a();
        }

        async void a()
        {
            textBox1.AppendText("перед Method1\r\n");
            textBox1.AppendText(await Method1());
            textBox1.AppendText("после Method1\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(".");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("перед Method2\r\n");
            textBox1.AppendText(await Method2());
            textBox1.AppendText("после Method2\r\n");
        }

        async Task<string> Method1()
        {
            textBox1.AppendText("перед await\r\n");
            await Task.Delay(5000);
            textBox1.AppendText("после await\r\n");
            return "результат Method1\r\n";
        }

        Task<string> Method2()
        {
            return Task<string>.Factory.StartNew(new Func<string>(() =>
            {
                Task.Delay(5000).Wait();
                return "результат Method2\r\n";
            }));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }




    }
}
