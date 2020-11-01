using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;


namespace WPF_ScreenSaver
{
    /// <summary>
    /// provides extension methods for 
    /// IEnumerable&lt;FileInfo&gt; Types
    /// </summary>
    public static class FileInfoExtensions
    {
        #region IEnumerable<FileInfo> extension methods
        public static IEnumerable<FileInfo> IsImageFile(this IEnumerable<FileInfo> files,
                                            Predicate<FileInfo> isMatch)
        {
            foreach (FileInfo file in files)
            {
                if (isMatch(file))
                    yield return file;
            }
        }

        public static IEnumerable<FileInfo> IsImageFile(this IEnumerable<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                if (file.Name.EndsWith(".jpg") || 
                    file.Name.EndsWith(".png") || 
                    file.Name.EndsWith(".bmp"))
                    yield return file;
            }
        }
        #endregion
    }
}
