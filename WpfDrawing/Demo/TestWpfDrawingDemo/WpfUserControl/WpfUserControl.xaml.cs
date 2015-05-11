using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfDrawingDemo.WpfUserControl
{
    /// <summary>
    /// Логика взаимодействия для WpfUserControl.xaml
    /// </summary>
    public partial class WpfUserControl : UserControl
    {
        //Масштаб
        double scaleMinimum = 1;
        double scaleNormal = 100;
        double scaleMaximum = 10000;
        double scaleCurrent;
        double HeightNormal;
        double WidthNormal;
        double scaleStep = 1;

        public event Action<double> CurrentScaleChanged;

        public WpfUserControl()
        {
            InitializeComponent();
        }


        public void NormalScale()
        {
            scaleCurrent = scaleNormal;

            MyImage.Height = HeightNormal * scaleCurrent / 100;
            MyImage.Width = WidthNormal * scaleCurrent / 100;

            if (CurrentScaleChanged != null)
                CurrentScaleChanged(scaleCurrent);

        }

        public BitmapScalingMode SetBitmapScallingMode
        {
            set
            {
                RenderOptions.SetBitmapScalingMode(MyGrid, value);
            }
        }

        void MyUserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MyGrid.Height = e.NewSize.Height;
            MyGrid.Width = e.NewSize.Width;
        }

        public void SetImageFromBitmap(Bitmap bitmap)
        {
            //http://stackoverflow.com/questions/22499407/how-to-display-a-bitmap-in-a-wpf-image

            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Image img = bitmap as System.Drawing.Image;
                if (img != null)
                {
                    MyImage.Width = img.Width;
                    MyImage.Height = img.Height;

                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    memoryStream.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    MyImage.Source = bitmapImage;
                    scaleCurrent = scaleNormal;
                    HeightNormal = MyImage.Height;
                    WidthNormal = MyImage.Width;

                    if (CurrentScaleChanged != null)
                        CurrentScaleChanged(scaleCurrent);
                }
            }
        }

        public Bitmap GetBitmapFromImage()
        {
            //http://stackoverflow.com/questions/5689674/c-sharp-convert-wpf-image-source-to-a-system-drawing-bitmap
            BitmapSource srs = (BitmapSource)MyImage.Source;
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppRgb, ptr))
                {
                    // Clone the bitmap so that we can dispose it and
                    // release the unmanaged memory at ptr
                    return new System.Drawing.Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }


        private void MyGrid_MouseWheell(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (scaleCurrent < scaleMaximum)
                    scaleCurrent += scaleStep;
            }
            else if (e.Delta < 0)
            {
                if (scaleCurrent > scaleMinimum)
                    scaleCurrent -= scaleStep;
            }

            MyImage.Height = HeightNormal * scaleCurrent / 100;
            MyImage.Width = WidthNormal * scaleCurrent / 100;

            if (CurrentScaleChanged != null)
                CurrentScaleChanged(scaleCurrent);
        }

        public void DrawRect()
        {
            
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();

            Line line = new Line();
            line.X1 = 10;
            line.X2 = 100;
            line.Y1 = 10;
            line.Y2 = 100;
            line.Stroke = System.Windows.Media.Brushes.White;
            line.StrokeThickness = 4;

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 100;
            ellipse.Height = 150;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(255, 255, 255, 0);
            ellipse.Fill = mySolidColorBrush;



            MyGrid.Children.Clear();
            MyGrid.Children.Add(line);
            MyGrid.Children.Add(ellipse);
            MyGrid.Children.Add(rect);

            
        }



    }
}
