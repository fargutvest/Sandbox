using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotosViewer
{
    public class DuplicatesHelper
    {
        public DuplicatesCollection Duplicates;

        public void Init()
        {
            var files = GetDirectoryFiles(@"F:\", "*.*", SearchOption.AllDirectories);
            var metadatas = files.Select(_ => new FileMetadata(_)).Where(_ => Filter.Apply(_.FileName)).ToList();

            Duplicates = new DuplicatesCollection();
            Duplicates.LoadDuplicates(metadatas);
        }

        private static IEnumerable<string> GetDirectoryFiles(string rootPath, string patternMatch, SearchOption searchOption)
        {
            var foundFiles = Enumerable.Empty<string>();

            if (searchOption == SearchOption.AllDirectories)
            {
                try
                {
                    IEnumerable<string> subDirs = Directory.EnumerateDirectories(rootPath);
                    foreach (string dir in subDirs)
                    {
                        foundFiles = foundFiles.Concat(GetDirectoryFiles(dir, patternMatch, searchOption));
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (PathTooLongException) { }
            }

            try
            {
                foundFiles = foundFiles.Concat(Directory.EnumerateFiles(rootPath, patternMatch));
            }
            catch (UnauthorizedAccessException) { }

            return foundFiles;
        }

    }
}
