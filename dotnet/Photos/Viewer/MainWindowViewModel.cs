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

        public MainWindowViewModel()
        {
            _photosFolder = @"C:\Users\henadzi.myslitski\Pictures\";
            _photos = Directory.GetFiles(_photosFolder).Where(_ => Path.GetExtension(_) == ".jpg").ToArray();
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
            var tags = GetTags();
            Tags = string.Join(Environment.NewLine, tags);
        }

        private string[] GetTags()
        {
            var shell = new Shell32.Shell();
            var objFolder = shell.NameSpace(_photosFolder);
            var tagsIndex = -1;
            for (int i = 0; i < short.MaxValue; i++)
            {
                string header = objFolder.GetDetailsOf(null, i);
                if (header.ToLower() == "tags")
                {
                    tagsIndex = i;
                    break;
                }
            }

            var tagsStr = "";
            foreach (Shell32.FolderItem2 item in objFolder.Items())
            {
                if (item.Name == Path.GetFileName(ImageFilePath))
                {
                    tagsStr = objFolder.GetDetailsOf(item, tagsIndex);
                    break;
                }
            }

            return tagsStr.Split(';').Select(_ => _.TrimStart(' ')).ToArray();
        }
    }
}
