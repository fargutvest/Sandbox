using Newtonsoft.Json;

namespace ListenCursor
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
