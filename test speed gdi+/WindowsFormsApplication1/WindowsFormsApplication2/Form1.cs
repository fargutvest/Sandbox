using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Location = new Point(0, 0);
            var content = GetContent();
            if (content == null)
            {
                Close();
                return;
            }
            _cts = new System.Threading.CancellationTokenSource();
            Work(_cts.Token, content);

        }

        private System.Threading.CancellationTokenSource _cts;

        private void Work(System.Threading.CancellationToken token, Bitmap[] content)
        {

            int i = 0;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    //Task.Delay(1).Wait();

                    if (token.IsCancellationRequested)
                        return;

                    pictureBox1.Invoke(new Action(() =>
                    {
                        pictureBox1.Image = content[i];
                    }));
                    i++;
                    if (i >= content.Length)
                        i = 0;
                }
            });
        }

        private Bitmap[] GetContent()
        {
            List<Bitmap> content = new List<Bitmap>();
            int countImagies = 100;
            for (int i = 0; i < countImagies; i++)
            {
                try
                {
                    var image = (Bitmap)Bitmap.FromFile(String.Format("{0}.bmp", i));
                    content.Add(image);
                }
                catch (Exception) { }
            }
            if (content.Count == countImagies)
                return content.ToArray();

            for (int i = 0; i < countImagies; i++)
            {
                try
                {
                    Random rand = new Random();
                    Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            int gray = rand.Next(255);
                            image.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                        }
                    }

                    image.Save(String.Format("{0}.bmp", i));
                }
                catch (Exception) { }
            }

            return null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts.Cancel();
        }
    }
}
