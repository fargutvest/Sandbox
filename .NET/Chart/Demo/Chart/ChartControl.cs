using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Chart
{
    public partial class ChartControl : UserControl
    {
        public ChartControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public event EventHandler<double> FpsChanged;
        private Stopwatch _stopwatch = new Stopwatch();

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawGrid(e);
            DrawRandomRect(e);

            base.OnPaint(e);

            if (FpsChanged != null)
                FpsChanged(this, (1 / ((double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency)));

            _stopwatch.Reset();
            _stopwatch.Start();
        }


        private void DrawRandomRect(PaintEventArgs e)
        {
            Random rand = new Random();
            int x = rand.Next(0, 2000);
            int y = rand.Next(0, 2000);
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(x, y, 40, 40));
        }


        private void DrawGrid(PaintEventArgs e)
        {

            int step = 4;
            Pen pen = new Pen(Color.Green);
            //vertical lines
            for (int i = 0; i < 1000; i++)
            {
                e.Graphics.DrawLine(pen, new Point(i * step, 0), new Point(i * step, Height));
            }

            //horizontal lines
            for (int j = 0; j < 1000; j++)
            {
                e.Graphics.DrawLine(pen, new Point(0, j * step), new Point(Width, j * step));
            }

        }
    }
}
