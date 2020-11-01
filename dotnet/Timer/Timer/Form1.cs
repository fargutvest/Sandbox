using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        System.Threading.Timer timer;
        DateTime TimeStart = new DateTime();
        string path = "memory";

        public Form1()
        {
            InitializeComponent();

            Work();
        }

        void Work()
        {
            
            LoadFile();
            timer = new System.Threading.Timer(Output);
            timer.Change(0, 1000);
        }

        void Output(object state)
        {
            if (IsHandleCreated)
            {
                BeginInvoke(new Action(() =>
                {
                    lbOutputDays.Text = String.Format("Days: {0}", (DateTime.Now - TimeStart).ToString(@"dd"));
                    lbOutputTime.Text = (DateTime.Now - TimeStart).ToString(@"hh\:mm\:ss");
                }));
            }
        }



        void LoadFile()
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string s = sr.ReadLine();

                        TimeStart = DateTime.ParseExact(s, "yyyy:MM:dd:HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception) { }
        }

        void SaveFile(DateTime dateTime)
        {
            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter sw = new StreamWriter(File.Create(path)))
            {
                sw.WriteLine(dateTime.ToString("yyyy:MM:dd:HH:mm:ss"));
            }
        }


        private void btStart_Click(object sender, EventArgs e)
        {
         
        }

        private void btReset_Click(object sender, EventArgs e)
        {

        }

        private void btStop_Click(object sender, EventArgs e)
        {

        }

        private void btSetBegin_Click(object sender, EventArgs e)
        {

        }



    }
}
