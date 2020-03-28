using System;
using System.Collections.Generic;

namespace UtilityExtensions.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// If the value is higher than 1, returns a new string with the suffix.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="condition"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string Plural(this string s, int value, string suffix = "s")
        {
            if (value > 1)
                return s += suffix;

            return s;
        }

        /// <summary>
        /// Returns a new String with its characters shuflled.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Shuffle(this string str)
        {

            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        public static string RemoveLineEndings(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace(lineSeparator, string.Empty)
                        .Replace(paragraphSeparator, string.Empty)
                        .RemoveZeroWidthSpace();
        }

        public static bool ContainsIgnoreCase(this List<string> l, string s)
        {
            if (l == null)
                return false;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].ToLower().Trim().RemoveZeroWidthSpace() == s.ToLower().Trim().RemoveZeroWidthSpace())
                    return true;
            }

            return false;
        }

        public static string RemoveZeroWidthSpace(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Replace("\u200B", "");
        }
    }
}