using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Viewer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _imageFilePath;
        public string ImageFilePath
        {
            get
            {
                return _imageFilePath;
            }
            set
            {
                _imageFilePath = value;
                Info = $"{ImageFilePath}{Environment.NewLine}{string.Join(Environment.NewLine, _helper.GetTags(ImageFilePath))}";
                TemplateImage = BitmapFrame.Create(new Uri(ImageFilePath), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

        private BitmapFrame _templateImage;
        public BitmapFrame TemplateImage
        {
            get
            {
                return _templateImage;
            }
            set
            {
                _templateImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TemplateImage)));
            }
        }

        private string _info;
        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                _info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));
            }
        }

        private string _photosFolder;
        public string PhotosFolder
        {
            get
            {
                return _photosFolder;
            }
            set
            {
                _photosFolder = value;
                _photos = Directory.GetFiles(PhotosFolder).Where(_ => Path.GetExtension(_) == ".jpg").ToArray();
                _helper = new CompactExifLib.ExifHelper();
                ImageFilePath = _photos[_index];
            }
        }

        private string[] _photos;
        private int _index;
        private CompactExifLib.ExifHelper _helper;
        
        public void On_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                Right();
            }
            if (e.Key == Key.Left)
            {
                Left();
            }
            if (e.Key == Key.Up)
            {
                Mark();
            }

            if (e.Key == Key.Down)
            {
                Mark(false);
            }
        }

        public void Right()
        {
            if (_index < _photos.Length - 1)
            {
                _index += 1;
            }
            ImageFilePath = _photos[_index];
        }

        public void Left()
        {
            if (_index > 0)
            {
                _index -= 1;
            }
            ImageFilePath = _photos[_index];
        }

        public void Mark(bool marked = true)
        {
            var markerTag = ConfigurationManager.AppSettings["MarkerTag"];
            if (marked)
            {
                _helper.SaveTags(ImageFilePath, new string[] { markerTag });
            }
            else
            {
                _helper.SaveTags(ImageFilePath, null, new string[] { markerTag });
            }
            ImageFilePath = _photos[_index];
        }
    }
}
