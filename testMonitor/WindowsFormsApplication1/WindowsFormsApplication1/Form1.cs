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
        BackgroundWorker bgw = new BackgroundWorker();
        AutoResetEvent are = new AutoResetEvent(false);
        Graphics graphics;
        Task task;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bgw.DoWork += bgw_DoWork;


            //task = new Task(taskExecute);
            //task.Start();

        }

        void taskExecute()
        {
            BeginInvoke(new Action(() =>
            {
                //this.Size = SystemInformation.PrimaryMonitorSize;
                this.Location = new Point(0, 0);

                AutoResetEvent are = new AutoResetEvent(false);
                int delay = 10;

                List<Color> colors = new List<Color>();
                colors.Add(Color.Red);
                colors.Add(Color.Green);
                colors.Add(Color.Blue);
                colors.Add(Color.Black);
                colors.Add(Color.White);


                int count = 0;
                while (true)
                {
                    count++;
                    if (count == colors.Count)
                        count = 0;

                    this.BackColor = colors[count];

                    are.WaitOne(delay);
                    //are.WaitOne();
                }
            }));
        }



        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            /*BeginInvoke(new Action(() =>
            {*/
            //this.Size = SystemInformation.PrimaryMonitorSize;
            //this.Location = new Point(0, 0);

            AutoResetEvent are = new AutoResetEvent(false);
            int delay = 1;

            List<Color> colors = new List<Color>();
            colors.Add(Color.Red);
            colors.Add(Color.Green);
            colors.Add(Color.Blue);
            colors.Add(Color.Black);
            colors.Add(Color.White);


            int count = 0;

            Bitmap _bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Random rnd = new Random();
            graphics = Graphics.FromImage(_bitmap);

            while (true)
            {
                _bitmap.SetPixel(0, 0, Color.FromArgb(0, rnd.Next(0, 255), 0));
                graphics.DrawImage((Image)_bitmap, 0, 0);

                BeginInvoke(new Action(() =>
                {
                    try
                    { 
                        pictureBox1.Image = _bitmap; 
                    }
                    catch (Exception) { }
                }));


                /*
                count++;
                if (count == colors.Count)
                    count = 0;
         
                this.BackColor = colors[count];
                this.Refresh();
                 */
                are.WaitOne(delay);


            }
            /* }));*/

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            are.Set();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            bgw.RunWorkerAsync();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }




    }
}
