using System.Collections.Generic;
using System.Linq;

namespace PhotosViewer
{
    public class DuplicatesCollection
    {
        public Dictionary<NameAndSizeKey, List<FileMetadata>> Collection { get; set; }

        public void LoadDuplicates(List<FileMetadata> metadatas)
        {
            var nameAndSizeGrouping = new Dictionary<NameAndSizeKey, List<FileMetadata>>();

            foreach (var metadata in metadatas)
            {
                if (nameAndSizeGrouping.ContainsKey(metadata.Key))
                {
                    nameAndSizeGrouping[metadata.Key].Add(metadata);
                }
                else
                {
                    nameAndSizeGrouping.Add(metadata.Key, new List<FileMetadata>() { metadata });
                }
            }

            Collection = nameAndSizeGrouping.Where(_ => _.Value.Count() > 1).ToDictionary(_ => _.Key, _ => _.Value);
        }

    }
}
