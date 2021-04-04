using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

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
                _helper.RemoveTags(ImageFilePath, "ToPrint");
                //_helper.AppendTags(ImageFilePath, "ToPrint");
                Tags = string.Join(Environment.NewLine, _helper.GetTagsOnly(ImageFilePath));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageFilePath)));
            }
        }

        private string _tags;
        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tags)));
            }
        }

        private string _photosFolder;
        private string[] _photos;
        private int _index;
        private CompactExifLib.ExifHelper _helper;

        public MainWindowViewModel()
        {
            _photosFolder = @"C:\Users\henadzi.myslitski\Pictures\Camera Roll\";
            _photos = Directory.GetFiles(_photosFolder).Where(_ => Path.GetExtension(_) == ".jpg").ToArray();
            _helper = new CompactExifLib.ExifHelper();
            ImageFilePath = _photos[_index];
        }

        public void On_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                if (_index < _photos.Length - 1)
                {
                    _index += 1;
                }
            }
            if (e.Key == Key.Left)
            {
                if (_index > 0)
                {
                    _index -= 1;
                }
            }

            ImageFilePath = _photos[_index];
        }
    }
}
