using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Begin();
        }

        private Task _task;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        List<Bitmap> _images = new List<Bitmap>();

        private void Begin()
        {
            _task = Task.Factory.StartNew(() =>
            {
                try
                {
                    Random random = new Random();
                    int imagesCount = 10;
                    for (int k = 0; k < imagesCount; k++)
                    {
                        if (File.Exists(String.Format("{0}.bmp", k.ToString())))
                        {
                            continue;
                        }
                        Bitmap temp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        for (int x = 0; x < temp.Width; x++)
                        {
                            for (int y = 0; y < temp.Height; y++)
                            {
                                temp.SetPixel(x, y, Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                            }
                        }
                        temp.Save(String.Format("{0}.bmp", k.ToString()));
                    }

                    int count = 0;

                    for (int t = 0; t < imagesCount; t++)
                    {
                        _images.Add((Bitmap)Bitmap.FromFile(String.Format("{0}.bmp", t)));
                    }



                    while (true)
                    {
                        pictureBox1.Invoke(new Action(() =>
                        {
                            pictureBox1.Image = _images[count];

                            count++;
                            if (count >= _images.Count)
                                count = 0;
                        }));
                        if (_cts.Token.IsCancellationRequested)
                            return;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts.Cancel();
        }
    }
}
