using System;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CaptureImage.Common.Converters
{
    public static class BitmapConverter
    {
        public static ImageBrush Convert(this Bitmap bitmap)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                                             IntPtr.Zero,
                                                                             Int32Rect.Empty,
                                                                             BitmapSizeOptions.FromEmptyOptions()
             );
            var brush = new ImageBrush(bitmapSource);

            return brush;
        }
    }
}
