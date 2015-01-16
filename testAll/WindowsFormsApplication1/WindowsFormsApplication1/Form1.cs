using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Generator generator;
        Form2 form2;

        bool flag = false;

        public Form1()
        {
            InitializeComponent();
            generator = new Generator();
            generator.evAutoSearch += generator_evAutoSearch;
            form2 = new Form2(generator);

            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    WaitHandle.WaitAll(new WaitHandle[] { new AutoResetEvent(false) }, 10);
                    if (flag)
                    {
                        PlayStop.Set();
                    }
                    else
                    {
                        PlayStop.Reset();
                    }
                }
            });
        }

        void generator_evAutoSearch()
        {
            form2.Location = new Point(this.Location.X, this.Location.Y + 30);
            if (form2.ShowDialog() == DialogResult.OK)
            {

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            test();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag = false;
        }

        AutoResetEvent WaitCancell = new AutoResetEvent(false);
        ManualResetEvent PlayStop = new ManualResetEvent(false);
        void test()
        {
            int i = 0;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    //Управление потоком
                    WaitHandle.WaitAny(new WaitHandle[] { WaitCancell, PlayStop });

                    i++;
                    this.BeginInvoke(new Action(() =>
                    {
                        this.Text = i.ToString();
                    }));
                    WaitHandle.WaitAll(new WaitHandle[] { new AutoResetEvent(false) }, 10);
                }
            });
        }



    }
}
