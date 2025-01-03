using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System.Windows;

namespace CaptureImage.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DescktopInfo info = ScreensHelper.GetDesktopInfo();

            // устанавливаем размеры окна по всему экрану
            this.Width = info.ClientSize.Width;
            this.Height = info.ClientSize.Height;

            // устанавливаем форму окна
            this.Clip = info.PolygonGeometry;

            // устанавливаем начальное положение окна в левый верхний угол
            this.Left = info.Location.X;
            this.Top = info.Location.Y;
        }
    }
}
