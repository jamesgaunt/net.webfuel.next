using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class StringExtensions
    {
        // Ensures the first letter of the string is lower case
        public static string? ToCamelCase(this string? input)
        {
            if (input == null || input.Length == 0 || char.IsLower(input[0]))
                return input;

            StringBuilder sb = new StringBuilder();
            bool leading = true;
            foreach (char c in input)
            {
                if (leading && char.IsUpper(c))
                    sb.Append(char.ToLower(c));
                else
                {
                    sb.Append(c);
                    leading = false;
                }
            }
            return sb.ToString();
        }

        // Ensures the first letter of the string is upper case
        public static string? ToPascalCase(this string? input)
        {
            if (input == null || input.Length == 0 || char.IsUpper(input[0]))
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
    }
}
