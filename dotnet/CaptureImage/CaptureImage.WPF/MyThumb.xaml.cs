using System.Windows.Controls;

namespace CaptureImage.WPF
{
    /// <summary>
    /// Interaction logic for Thumb.xaml
    /// </summary>
    public partial class MyThumb : UserControl
    {
        private bool lmb_down;
        private double start_mouse_x;
        private double start_mouse_y;

        private bool mouseIsOver;

        public MyThumb()
        {
            InitializeComponent();
        }
    }
}
