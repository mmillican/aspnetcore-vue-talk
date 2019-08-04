using System.Text.RegularExpressions;

namespace MVCWithVueSpa
{
    public static class ExtensionMethods
    {
        
        public static string Clean(this string input)
        {
            var regex = new Regex("[^a-z0-9\\-_]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            input = input.Replace(" ", "-");
            var cleaned = regex.Replace(input, "").ToLower();

            while (cleaned.Contains("--"))
            {
                cleaned = cleaned.Replace("--", "-");
            }

            return cleaned;
        }

        public static string Truncate(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (input.Length <= maxLength)
            {
                return input;
            }

            return input.Substring(0, maxLength);
        }
    }
}