namespace ST.Common
{
    public static class StringExtentions
    {
        /// <summary>
        /// Truncate the string to a specific length.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || maxLength <= 0) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// Normalize tabs or multiple whitespace to a single whitespace.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NormalizeWhitespace(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", " ").Trim();
        }
    }
}
