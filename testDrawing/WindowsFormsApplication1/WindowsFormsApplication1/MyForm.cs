using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class MyForm : Form
    {
        string s="";
        public MyForm()
        {
            InitializeComponent();
            TaskImageGenerate taskImageGenerate = new TaskImageGenerate(this.Height, this.Width);
            taskImageGenerate.ImageLineEvent += taskImageGenerate_ImageLineEvent;
            taskImageGenerate.ImageLineEvent += taskImageGenerate_ImageLineEvent1;
            s = this.Text;
        }

        int inc = 0;
        delegate string del(string s);
        void taskImageGenerate_ImageLineEvent(object sender, Bitmap bitmap)
        {
            inc ++;
            string ss = inc.ToString();
            try
            {
                pictureBox1.Image = bitmap;
               // this.Invoke(new Action<string>(f => this.Text = s + ss ), "");

                del d = par => this.Text = s+ss;
                this.Invoke(d, "");
            }
            catch (Exception) { }
        }
        void taskImageGenerate_ImageLineEvent1(object sender, Bitmap bitmap)
        {
            inc++;
            string ss = inc.ToString();
            try
            {
                pictureBox1.Image = bitmap;
                // this.Invoke(new Action<string>(f => this.Text = s + ss ), "");

                del d = par => this.Text = s + ss;
                this.Invoke(d, "");
            }
            catch (Exception) { }
        }

       
    }
}
