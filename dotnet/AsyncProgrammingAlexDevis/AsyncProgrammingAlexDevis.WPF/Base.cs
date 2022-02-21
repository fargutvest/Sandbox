using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace AsyncProgrammingAlexDevis.WPF
{
    public class Base
    {
        protected WrapPanel m_WrapPanel;

        public Base(WrapPanel wrapPanel)
        {
            m_WrapPanel = wrapPanel;
        }

        protected Image MakeImageControl(byte[] bytes)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(bytes);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource imgSrc = biImg as ImageSource;
            return new Image() { Source = imgSrc, Width = 50 };
        }
    }
}
