using System.Collections.Generic;
using System.IO;

namespace Photos
{
    public class MyDirectory
    {
        public string Path { get; set; }
        public DirectoryInfo Info { get; set; }
        public List<FileInfo> Files { get; set; }
        public List<MyDirectory> SubDirectories { get; set; }
        public override string ToString()
        {
            return Path;
        }
    }
    
}
