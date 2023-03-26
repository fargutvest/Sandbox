using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PhotosViewer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string markerTag => ConfigurationManager.AppSettings["MarkerTag"];
        private string _cacheFileName = "cache.photos_viewer";

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
                if (string.IsNullOrEmpty(value) == false)
                {
                    _imageFilePath = value;
                    var tags = _helper.GetTags(ImageFilePath);
                    Marked = tags.Contains(markerTag);
                    Info = $"{ImageFilePath}{Environment.NewLine}{string.Join(Environment.NewLine, tags)}";
                    TemplateImage = BitmapFrame.Create(new Uri(ImageFilePath), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
                else
                {
                    Info = null;
                    TemplateImage = null;
                }
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
        
        private bool _marked;
        public bool Marked
        {
            get
            {
                return _marked;
            }
            set
            {
                _marked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Marked)));
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
                _photos = Directory.GetFiles(value).Where(_ => Path.GetExtension(_) == ".jpg").ToArray();
                _photosFolder = value;
                _helper = new CompactExifLib.ExifHelper();
                if (_photos.Any())
                {
                    _index = 0;
                    ImageFilePath = _photos[_index];
                    SaveCache();
                }
                else
                {
                    ImageFilePath = null;
                }
            }
        }

        private string[] _photos;
        private int _index;
        private CompactExifLib.ExifHelper _helper;

        public MainWindowViewModel()
        {
            LoadCache();
        }

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
            if (_photos?.Any() == true && _index < _photos.Length - 1)
            {
                _index += 1;
                ImageFilePath = _photos[_index];
            }
        }

        public void Left()
        {
            if (_photos?.Any() == true && _index > 0)
            {
                _index -= 1;
                ImageFilePath = _photos[_index];
            }
        }

        public void Mark(bool marked = true)
        {
           
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

        public void Duplicates()
        {
            var modalWindow = new DuplicatesWindow();
            modalWindow.ShowDialog();
        }

        private void LoadCache()
        {
            if (File.Exists(_cacheFileName))
            {
                var cache = File.ReadAllText(_cacheFileName);
                if (string.IsNullOrEmpty(cache) == false && Directory.Exists(cache))
                {
                    PhotosFolder = cache;
                }
            }
        }

        private void SaveCache()
        {
            if (File.Exists(_cacheFileName))
            {
                File.Delete(_cacheFileName);
            }

            File.AppendAllText(_cacheFileName, PhotosFolder);
        }
    }
}
