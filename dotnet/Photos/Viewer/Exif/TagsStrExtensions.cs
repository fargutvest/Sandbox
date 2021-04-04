namespace CompactExifLib
{
    public static class TagsStrExtensions
    {
        public static string[] SplitTags(this string tagsStr)
        {
            return tagsStr?.TrimEnd(' ').Split(';') ?? new string[] { "" };
        }
    }
}
