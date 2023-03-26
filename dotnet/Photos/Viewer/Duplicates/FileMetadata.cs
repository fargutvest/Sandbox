using System.IO;

namespace PhotosViewer
{
    public class FileMetadata
    {
        public FileInfo Info { get; set; }
        public string FileName => Path.GetFileName(Info.FullName);

        public FileMetadata(string filePath)
        {
            Info = new FileInfo(filePath);
        }

        public override string ToString()
        {
            return $"{Info.FullName} {Info.Length} bytes";
        }

        public NameAndSizeKey Key => new NameAndSizeKey(Info);

    }
}
