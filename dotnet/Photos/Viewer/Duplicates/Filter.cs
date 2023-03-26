using System.Linq;

namespace PhotosViewer
{
    public class Filter
    {
        private static string[] _filtered = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        public static bool Apply(string value) => _filtered.Any(_ => value.EndsWith(_));
    }
}
