using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace TestWpfDrawingDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            wpfUserControl1.CurrentScaleChanged += wpfUserControl1_CurrentScaleChanged;
        }

        void wpfUserControl1_CurrentScaleChanged(double currentScale)
        {
            tsslCurrentScale.Text = String.Format("Current scale: {0}%", currentScale.ToString());
        }

        #region Выбор интераоляции
       

        private void tsmiBicubic_Click(object sender, EventArgs e)
        {
            wpfUserControl1.SetBitmapScallingMode = BitmapScalingMode.HighQuality;
        }

        private void tsmiBilinear_Click(object sender, EventArgs e)
        {
            wpfUserControl1.SetBitmapScallingMode = BitmapScalingMode.LowQuality;
        }

        private void tsmiNearestNeighbor_Click(object sender, EventArgs e)
        {
            wpfUserControl1.SetBitmapScallingMode = BitmapScalingMode.NearestNeighbor;
        }

       

        #endregion

        private void normalScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wpfUserControl1.NormalScale();
        }


        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            string pathBitmap;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.bmp)|*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pathBitmap = ofd.FileName;
                Bitmap bmp = (Bitmap)Bitmap.FromFile(pathBitmap);
                if (bmp != null)
                {

                    wpfUserControl1.SetImageFromBitmap(bmp);
                }
            }
        }

        private void tsmiGenerate_Click(object sender, EventArgs e)
        {
            System.Drawing.Color chessWhite = System.Drawing.Color.White;
            System.Drawing.Color chessBlack = System.Drawing.Color.Black;
            List<System.Drawing.Color> chessColors = new List<System.Drawing.Color>();
            chessColors.Add(chessWhite);
            chessColors.Add(chessBlack);

            Rectangle rectScreen = Screen.AllScreens[0].WorkingArea;
            Bitmap testBitmap = new Bitmap(rectScreen.Width, rectScreen.Height);
            int maximum = testBitmap.Width * testBitmap.Height;
            

            int value = 0;
            bool taskRun = true;
            Task.Factory.StartNew(new Action(() => 
            {
                Form form = new Form();
                form.Size = new Size(200, 10);
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                form.Controls.Add(new ProgressBar());
                form.Controls[0].Size = form.Size;
                ProgressBar pBar = (ProgressBar)form.Controls[0];
                pBar.Maximum = maximum;
                pBar.Value = 0;

                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(this.Location.X + this.Width / 2 - pBar.Width/2, this.Location.Y + this.Height / 2 - pBar.Height/2);
                form.Show();

                while (taskRun)
                {
                    pBar.Value = value;
                }

                form.Close();
            }));
            


        

            for (int j = 0; j < testBitmap.Height - 1; j++)
            {
                for (int i = 0; i < testBitmap.Width - 1; i++)
                {
                    int ti = i % 2;
                    int tj = j % 2;

                    testBitmap.SetPixel(i, j, chessColors[ti ^ tj]);
                    value++;
                }
            }
            taskRun = false;
            wpfUserControl1.SetImageFromBitmap(testBitmap);
            
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = wpfUserControl1.GetBitmapFromImage();
            bmp.Save("sample.bmp");
        }

        private void drawRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wpfUserControl1.DrawRect();
        }




    }
}
