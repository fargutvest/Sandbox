using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WebCamCSharp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            BitmapImage bi;

            using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
            {
                bi = bitmap.ToBitmapImage();
            }
            bi.Freeze();
            Dispatcher.BeginInvoke(new ThreadStart(delegate { ImageWebcam.Source = bi; }));
        }
    }
}
