using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Webfuel.Scribble.Extensions
{
    public static class ScribbbleStringExtensions
    {
        public static string? Substring(this string? input, int skip, int? take = null)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            if (skip >= input.Length || take == 0)
                return String.Empty;

            if (take == null || skip + take >= input.Length)
                take = input.Length - skip;

            return input.Substring(startIndex: skip, length: take.Value);
        }

        public static string? WrapWithTag(this string? input, string tag)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return $"<{tag}>{input}</{tag}>";
        }

        public static bool IsNullOrEmpty(this string? input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool IsNotNullOrEmpty(this string? input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool IsNullOrWhiteSpace(this string? input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsNotNullOrWhiteSpace(this string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static string? HtmlAttributeEncode(this string? input)
        {
            return string.IsNullOrWhiteSpace(input)
                   ? input
                   : System.Web.HttpUtility.HtmlAttributeEncode(input);
        }

        public static string? ToIdentifier(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            StringBuilder sb = new StringBuilder();
            foreach (var c in input)
            {
                if (char.IsLetterOrDigit(c))
                    sb.Append(c);
            }
            var id = sb.ToString();
            if (!char.IsLetter(id[0]))
                id = "_" + id;
            return id;
        }

        public static string? FirstParagraph(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return String.Empty;

            var sa = input.IndexOf("<p>");
            var sb = input.IndexOf("<p ");

            if (sa < 0 && sb < 0)
                return String.Empty;

            // Pick the smallest one that isn't < 0
            var s = -1;
            if (sa < 0)
                s = sb;
            else if (sb < 0)
                s = sa;
            else
                s = Math.Min(sa, sb);

            var e = input.IndexOf("/p>", s);

            if (s < 0 || e <= s)
                return String.Empty;

            return input.Substring(s, e - s + 3);
        }

        public static string? ToClass(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            input = input.Trim().Replace(' ', '-').ToLower();

            while (input.Contains("--"))
                input = input.Replace("--", "-");

            return input;
        }

        public static string? NewLinesToParagraphs(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            var parts = input.Split('\n').Select(p => p.Trim()).Where(p => !String.IsNullOrEmpty(p)).Select(p => "<p>" + p + "</p>");
            return String.Join(String.Empty, parts);
        }

        public static string? NewLinesToBreaks(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            var parts = input.Split('\n').Select(p => p.Trim()).Where(p => !String.IsNullOrEmpty(p)).Select(p => p + "<br/>");
            return String.Join(String.Empty, parts);
        }

        public static string? StripHtml(this string? input)
        {
            return string.IsNullOrWhiteSpace(input)
                   ? input
                   : Regex.Replace(input, @"<.*?>", string.Empty);
        }

        public static string? MD5Hash(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashAlgorithm = MD5.Create();
            var hashedBytes = hashAlgorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", string.Empty).ToLower();
        }

        // Conversions 

        public static Int32? ToInt32(this string? input, Int32? fallback = null)
        {
            if (String.IsNullOrEmpty(input))
                return fallback;

            if (!Int32.TryParse(input, out int result))
                return fallback;

            return result;
        }

        // Path

        public static string? DirectoryName(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return System.IO.Path.GetDirectoryName(input);
        }

        public static string? FileName(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return System.IO.Path.GetFileName(input);
        }

        public static string? FileNameWithoutExtension(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return System.IO.Path.GetFileNameWithoutExtension(input);
        }

        public static string? FileNameExtension(this string? input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return System.IO.Path.GetExtension(input);
        }

        // Markup

        public static string? Markup(this string? input)
        {
            if (String.IsNullOrEmpty(input) || !input.Contains('['))
                return input;

            return input
                .Replace("[BR]", "<br/>");
        }
    }
}
