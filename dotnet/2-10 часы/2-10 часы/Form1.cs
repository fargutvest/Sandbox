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
        string sec;
        string min;
        string hour;
        string sec1;
        string min1;
        string hour1;
        string sec2;
        string min2;
        string hour2;



        DateTime DT = new DateTime();
        public Form1()
        {
            InitializeComponent();


            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            sec = DateTimeOffset.Now.Second.ToString();
            min = DateTimeOffset.Now.Minute.ToString();
            hour = DateTimeOffset.Now.Hour.ToString();

            if (sec.Length == 2)
            {
                sec1 = sec.Substring(1, 1);
                sec2 = sec.Substring(0, 1);      
            }
            else
            {
                sec1 = sec;
                sec2 = "0";  
            }

            if (min.Length == 2)
            {
                min1 = min.Substring(1, 1);
                min2 = min.Substring(0, 1);
            }
            else
            {
                min1 = min;
                min2 = "0";
            }

            if (hour.Length == 2)
            {
                hour1 = hour.Substring(1, 1);
                hour2 = hour.Substring(0, 1);
            }
            else
            {
                hour1 = hour;
                hour2 = "0";
            }



            hour2 = Convert.ToString(Convert.ToInt32(hour2), 2).ToString();
            hour1 = Convert.ToString(Convert.ToInt32(hour1), 2).ToString();
            min2 = Convert.ToString(Convert.ToInt32(min2), 2).ToString();
            min1 = Convert.ToString(Convert.ToInt32(min1), 2).ToString();
            sec2 = Convert.ToString(Convert.ToInt32(sec2), 2).ToString();
            sec1 = Convert.ToString(Convert.ToInt32(sec1), 2).ToString();



            if (hour2.Length == 3) hour2 = "0" + hour2;
            if (hour2.Length == 2) hour2 = "00" + hour2 ;
            if (hour2.Length == 1) hour2 = "000" + hour2;


            if (hour1.Length == 3) hour1 = "0" + hour1;
            if (hour1.Length == 2) hour1 = "00" + hour1;
            if (hour1.Length == 1) hour1 = "000" + hour1;


            if (min2.Length == 3) min2 = "0" + min2;
            if (min2.Length == 2) min2 = "00" + min2;
            if (min2.Length == 1) min2 = "000" + min2;


            if (min1.Length == 3) min1 = "0" + min1;
            if (min1.Length == 2) min1 = "00" + min1;
            if (min1.Length == 1) min1 = "000" + min1;


            if (sec2.Length == 3) sec2 = "0" + sec2;
            if (sec2.Length == 2) sec2 = "00" + sec2;
            if (sec2.Length == 1) sec2 = "000" + sec2;


            if (sec1.Length == 3) sec1 = "0" + sec1;
            if (sec1.Length == 2) sec1 = "00" + sec1;
            if (sec1.Length == 1) sec1 = "000" + sec1;
  

                label1.Text = hour2;
                label2.Text = hour1;
                label3.Text = min2;
                label4.Text = min1;
                label5.Text = sec2;
                label6.Text = sec1;
                 
        }



    }
}
