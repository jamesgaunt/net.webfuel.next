using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.IO;
using Microsoft.CSharp;
using System.Globalization;

namespace Webfuel.Tools.Typefuel
{
    internal static class StringExtensions
    {
        public static bool IsValidIdentifier(this string identifier)
        {
            for (int i = 0; i < identifier.Length; i++)
            {
                if (char.IsLetter(identifier[i]))
                    continue;
                if (char.IsNumber(identifier[i]) && i > 0)
                    continue;
                if (identifier[i] == '_')
                    continue;
                return false;
            }
            return true;
        }

        public static string ToIdentifier(this string input)
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

        public static bool ContainsWhitespace(this string input)
        {
            foreach (var c in input)
            {
                if (char.IsWhiteSpace(c))
                    return true;
            }
            return false;
        }

        public static string Escape(this string input)
        {
            if (input == null)
                return input;
            return System.Text.RegularExpressions.Regex.Escape(input);
        }

        public static string Unescape(this string input)
        {
            if (input == null)
                return input;
            return System.Text.RegularExpressions.Regex.Unescape(input);
        }

        // Ensures the first letter of the string is lower case
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                char c;
                c = char.ToLower(chars[i], CultureInfo.InvariantCulture);
                chars[i] = c;
            }

            return new string(chars);
        }

        // Ensures the first letter of the string is upper case
        public static string ToPascalCase(this string input)
        {
            if (input == null)
                return input;
            StringBuilder sb = new StringBuilder();
            bool leading = true;
            foreach (char c in input)
            {
                if (leading && char.IsLower(c))
                    sb.Append(char.ToUpper(c));
                else
                    sb.Append(c);
                leading = false;
            }
            return sb.ToString();
        }

        public static string FormatScript(this string script)
        {
            var output = new StringBuilder();
            var lines = script.ToString().Split('\n');
            var indent = 0;
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("}") || line.Trim().StartsWith("]"))
                    indent--;

                for (int i = 0; i < indent * 4; i++)
                    output.Append(" ");
                output.Append(line.Trim());
                output.Append("\n");

                if (line.Trim().StartsWith("}") || line.Trim().StartsWith("]"))
                    indent++;

                // Determine the impact on indent
                indent += line.Count(p => p == '{') - line.Count(p => p == '}') + line.Count(p => p == '[') - line.Count(p => p == ']');
            }
            return output.ToString();
        }
    }
}
