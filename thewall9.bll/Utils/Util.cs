using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace thewall9.bll.Utils
{
    public static class Util
    {
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        public static readonly Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

        public static string CleanUrl(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // replace hyphens to spaces, remove all leading and trailing whitespace
            value = value.Replace("-", " ").Trim().ToLower();

            // replace multiple whitespace to one hyphen
            value = Regex.Replace(value, @"[\s]+", "-");

            // replace umlauts and eszett with their equivalent
            value = value.Replace("ß", "ss");
            value = value.Replace("ä", "ae");
            value = value.Replace("ö", "oe");
            value = value.Replace("ü", "ue");

            // removes diacritic marks (often called accent marks) from characters
            value = RemoveDiacritics(value);

            // remove all left unwanted chars (white list)
            value = Regex.Replace(value, @"[^a-z0-9\s-]", String.Empty);

            return value;
        }
        public static string CleanUrl(this string value, int lenght)
        {
            var _C = value.CleanUrl();
            if (_C.Length > lenght)
                _C = _C.Substring(0, lenght);
            return _C;
        }
        public static string RemoveDiacriticsEF(this string s)
        {
            return s.ToLower().Replace("á", "a");
        }

        public static string RemoveDiacritics(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string normalized = value.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            Encoding nonunicode = Encoding.GetEncoding(850);
            Encoding unicode = Encoding.Unicode;

            byte[] nonunicodeBytes = Encoding.Convert(unicode, nonunicode, unicode.GetBytes(sb.ToString()));
            char[] nonunicodeChars = new char[nonunicode.GetCharCount(nonunicodeBytes, 0, nonunicodeBytes.Length)];
            nonunicode.GetChars(nonunicodeBytes, 0, nonunicodeBytes.Length, nonunicodeChars, 0);

            return new string(nonunicodeChars);
        }

        public static string GetSrc(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Match youtubeMatch = YoutubeVideoRegex.Match(value);
                Match vimeoMatch = VimeoVideoRegex.Match(value);

                string id = string.Empty;

                if (youtubeMatch.Success)
                    return "//www.youtube.com/embed/" + youtubeMatch.Groups[4].Value;
                if (vimeoMatch.Success)
                    return "//player.vimeo.com/video/" + vimeoMatch.Groups[1].Value;
            }
            return null;
        }
    }
}
