using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CSharpUtilityExtensions.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// If the value is higher than 1, returns a new string with the suffix.
        /// </summary>
        public static string Plural(this string str, int value, string suffix = "s")
        {
            if (value > 1)
                return str += suffix;

            return str;
        }

        public static int GetDamerauLevenshteinDistance(string s, string t)
        {
            int n = s?.Length ?? 0; // length of s
            int m = t?.Length ?? 0; // length of t

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            int[] p = new int[n + 1]; //'previous' cost array, horizontally
            int[] d = new int[n + 1]; // cost array, horizontally

            // indexes into strings s and t
            int i; // iterates through s
            int j; // iterates through t

            for (i = 0; i <= n; i++)
            {
                p[i] = i;
            }

            for (j = 1; j <= m; j++)
            {
                char tJ = t[j - 1]; // jth character of t
                d[0] = j;

                for (i = 1; i <= n; i++)
                {
                    int cost = s[i - 1] == tJ ? 0 : 1; // cost
                                                       // minimum of cell to the left+1, to the
                                                       // top+1, diagonally left and up +cost
                    d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + cost);
                }

                // copy current distance counts to 'previous row' distance counts
                int[] dPlaceholder = p; //placeholder to assist in swapping p and d
                p = d;
                d = dPlaceholder;
            }

            // our last action in the above loop was to switch d and p, so p now actually has the
            // most recent cost counts
            return p[n];
        }

        /// <summary>
        /// Checks if two strings are similar
        /// </summary>
        /// <param name="s"> </param>
        /// <param name="other"> </param>
        /// <param name="tolerance"> </param>
        /// <returns> </returns>
        public static bool Similar(this string s, string other, int tolerance = 5)
        {
            return GetDamerauLevenshteinDistance(s, other) <= Math.Abs(tolerance);
        }

        /// <summary>
        /// Returns a new String with its characters shuflled.
        /// </summary>
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

        /// <summary>
        /// If the string is Empty or Null, returns the replacement string
        /// </summary>
        /// <param name="s"> </param>
        /// <param name="replacement"> </param>
        /// <returns> </returns>
        public static string IfEmpty(this string s, string replacement)
        {
            return string.IsNullOrEmpty(s) ? replacement : s;
        }

        public static string RemoveLineEndings(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return str.Replace("\r\n", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace(lineSeparator, string.Empty)
                        .Replace(paragraphSeparator, string.Empty)
                        .RemoveZeroWidthSpace();
        }

        public static bool ContainsIgnoreCase(this List<string> l, string check)
        {
            if (l == null)
                return false;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].ToLower().Trim().RemoveZeroWidthSpace() == check.ToLower().Trim().RemoveZeroWidthSpace())
                    return true;
            }

            return false;
        }

        public static string RemoveZeroWidthSpace(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Replace("\u200B", "");
        }

        /// <summary>
        /// Try to safely convert this string to a DateTime.
        /// </summary>
        /// <param name="@default"> The default value. </param>

        public static DateTime ToDateTime(this string str, DateTime @default = default, IFormatProvider format = null, DateTimeStyles style = DateTimeStyles.None)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            DateTime temp;
            if (DateTime.TryParse(str, format, style, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a TimeSpan.
        /// </summary>
        /// <param name="@default"> The default value. </param>

        public static TimeSpan ToTimeSpan(this string str, TimeSpan @default = default, IFormatProvider format = null)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            TimeSpan temp;
            if (TimeSpan.TryParse(str, format, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a int.
        /// </summary>
        /// <param name="@default"> The default value. </param>
        public static int ToInt(this string str, int @default = 0, IFormatProvider format = null, NumberStyles style = NumberStyles.Any)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            int temp;
            if (int.TryParse(str, style, format, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a long.
        /// </summary>
        /// <param name="@default"> The default value. </param>
        public static long ToLong(this string str, long @default = 0, IFormatProvider format = null, NumberStyles style = NumberStyles.Any)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            long temp;
            if (long.TryParse(str, style, format, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a double.
        /// </summary>
        /// <param name="@default"> The default value. </param>

        public static double ToDouble(this string str, double @default = 0, IFormatProvider format = null, NumberStyles style = NumberStyles.Any)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            double temp;
            if (double.TryParse(str, style, format, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a float.
        /// </summary>
        /// <param name="@default"> The default value. </param>

        public static float ToFloat(this string str, float @default = 0, IFormatProvider format = null, NumberStyles style = NumberStyles.Any)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            float temp;
            if (float.TryParse(str, style, format, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a decimal.
        /// </summary>
        /// <param name="@default"> The default value. </param>

        public static decimal ToDecimal(this string str, decimal @default = 0, IFormatProvider format = null, NumberStyles style = NumberStyles.Any)
        {
            if (format == null)
                format = CultureInfo.InvariantCulture;
            decimal temp;
            if (decimal.TryParse(str, style, format, out temp))
                return temp;
            else
                return @default;
        }

        /// <summary>
        /// Try to safely convert this string to a bool.
        /// </summary>
        /// <param name="@default"> The default value. </param>
        public static bool ToBool(this string str, bool @default = false)
        {
            return str.Trim().ToLower().In("true", "1");
        }

        public static string Hash_MD5(this string str)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                // Create a new Stringbuilder to collect the bytes and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}