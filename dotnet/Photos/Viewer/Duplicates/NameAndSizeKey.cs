using System.IO;

namespace PhotosViewer
{
    public class NameAndSizeKey
    {
        public FileInfo Info { get; }

        public NameAndSizeKey(FileInfo info)
        {
            Info = info;
        }

        public override int GetHashCode()
        {
            return $"{Path.GetFileName(Info.FullName)}_{Info.Length}".GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var nameAndSizeKey = obj as NameAndSizeKey;

            if (nameAndSizeKey == null)
                return false;

            var fileNamesEquals = Path.GetFileName(nameAndSizeKey.Info.FullName).ToLower() == Path.GetFileName(this.Info.FullName).ToLower();
            var lenghtsEquals = nameAndSizeKey.Info.Length.Equals(this.Info.Length);

            return fileNamesEquals && lenghtsEquals;
        }
    }

}
