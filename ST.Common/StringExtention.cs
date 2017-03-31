namespace ST.Common
{
    public class StringExtention
    {
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string NormalizeWhitespace(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", " ").Trim();
        }
    }
}
