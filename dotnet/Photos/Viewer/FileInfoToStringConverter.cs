using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace PhotosViewer
{
    public class FileInfoToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var info = value as FileInfo;
            return $"{info.FullName} ({info.Length} bytes)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
