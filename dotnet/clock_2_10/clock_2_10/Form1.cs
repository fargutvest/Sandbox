using System;
using System.Windows.Forms;

namespace Clock_2_10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            string[] ss = Get2_10(now.ToString("ss"));
            string[] mm = Get2_10(now.ToString("mm"));
            string[] hh = Get2_10(now.ToString("HH"));

            hourLb.Text = hh[0];
            hour1Lb.Text = hh[1];
            minLb.Text = mm[0];
            lin1Lb.Text = mm[1];
            secLb.Text = ss[0];
            sec1Lb.Text = ss[1];

            timeLb.Text = now.ToString("HH:mm:ss");
        }

        private string[] Get2_10(string twoDigitStr)
        {
            string digit1 = twoDigitStr.Substring(1, 1);
            string digit0 = twoDigitStr.Substring(0, 1);

            string digit2_10_0 = Convert.ToString(Convert.ToInt32(digit0), 2).PadLeft(4, '0');
            string digit2_10_1 = Convert.ToString(Convert.ToInt32(digit1), 2).PadLeft(4, '0');

            return new string[] { digit2_10_0, digit2_10_1 };
        }
    }
}
