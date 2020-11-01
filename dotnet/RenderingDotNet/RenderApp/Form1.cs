using System;
using RenderingCommon;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RenderApp
{
    public partial class Form1 : Form
    {
        public delegate void InvokeDelegate();

        private MetricPerSecond metricFps;
        private MetricPerSecond metricLps;
        private ServiceConsumer serviceConsumer;
        private static readonly int width = RenderPreferences.Width;
        private static readonly int height = RenderPreferences.Height;
        private int[] frame;

        private Gdi32Painter gdi32Painter;

        public Form1()
        {
            InitializeComponent();

            Scene.Width = width;
            Scene.Height = height;
            var hDCGraphics = Scene.CreateGraphics();
            var hDCRef = new HandleRef(hDCGraphics, hDCGraphics.GetHdc());
            gdi32Painter = new Gdi32Painter(hDCRef);

            metricFps = new MetricPerSecond();
            metricLps = new MetricPerSecond();
            frame = new int[width * height];
            serviceConsumer = new ServiceConsumer();
        }

        public void ProcessLine(int[] generatedVerticalLine)
        {
            metricLps.Tick();
            frame = ShiftFrameToRight(frame, generatedVerticalLine);
        }


        private int[] ShiftFrameToRight(int[] frame, int[] generatedColumn)
        {
            var length = width - 1;
            var shifted = new int[width];
            for (var row = 0; row < height; row++)
            {
                var index = row * width;
                Array.Copy(frame, index, shifted, 1, length);
                Array.Copy(generatedColumn, row, shifted, 0, 1);
                Array.Copy(shifted, 0, frame, index, width);
            }

            return frame;
        }


        private void Render()
        {
            metricFps.Tick();
            gdi32Painter.Paint(frame, width, height);
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            var rendertimer = new DispatcherTimer();
            rendertimer.Interval = TimeSpan.FromMilliseconds(15);
            rendertimer.Tick += (o, args) => Render();
            rendertimer.Start();


            metricFps.UpdatedFps += (sender1, e1) =>
            {
                Task.Run(() =>
                {
                    label1.Invoke(new InvokeDelegate(() =>
                    {
                        label1.Text = $"Render fps={e1.ToString()}";
                    }));
                });
            };

            metricLps.UpdatedFps += (sender1, e1) =>
            {
                Task.Run(() =>
                {
                    label2.Invoke(new InvokeDelegate(() =>
                    {
                        label2.Text = $"Render lps={e1.ToString()}";
                    }));
                });
            };
        }
    }
}
