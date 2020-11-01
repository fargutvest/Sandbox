using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CSharpToUmlConverter
{
    public static class Extensions
    {
        public static string TrimWhitespaces(this string text)
        {
            return text?.Replace(" ", string.Empty);
        }

        public static string TrimColons(this string text)
        {
            return text?.Replace(":", string.Empty);
        }

        public static string Trim(this string text, string toTrim)
        {
            return text?.Replace(toTrim, string.Empty);
        }

        public static string TrimCommentedCode(this string text)
        {
            if (text.Contains("//"))
            {
                text = Regex.Replace(text, @"(?=//).*(\r\n|\n)", string.Empty);
            }
            if (text.Contains("/*") && text.Contains("*/"))
            {
                text = Regex.Replace(text, @"/\*(.|\r\n|\n)*?(?<=\*/)", string.Empty);
            }

            return text;
        }

        public static string GetDescription<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes?.Length > 0 ? attributes[0].Description : source.ToString();
        }
    }
}