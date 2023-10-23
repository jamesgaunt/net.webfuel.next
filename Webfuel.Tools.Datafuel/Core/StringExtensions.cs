using System;
using System.Collections.Generic;
using System.Globalization;
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
            var prev = ' ';
            foreach (var c in input)
            {
                if (char.IsLetterOrDigit(c)) {
                    if (char.IsWhiteSpace(prev))
                        sb.Append(char.ToUpper(c));
                    else
                        sb.Append(c);
                }
                prev = c;
            }
            var id = sb.ToString();
            if (!char.IsLetter(id[0]))
                id = "_" + id;
            return id;
        }

        public static string ToSnakeCase(this string name, char divider = '_')
        {
            if (string.IsNullOrEmpty(name))
                return name;

            var builder = new StringBuilder(name.Length + Math.Min(2, name.Length / 5));
            var previousCategory = default(UnicodeCategory?);

            for (var currentIndex = 0; currentIndex < name.Length; currentIndex++)
            {
                var currentChar = name[currentIndex];
                if (currentChar == divider)
                {
                    builder.Append(divider);
                    previousCategory = null;
                    continue;
                }

                var currentCategory = char.GetUnicodeCategory(currentChar);
                switch (currentCategory)
                {
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                        if (previousCategory == UnicodeCategory.SpaceSeparator ||
                            previousCategory == UnicodeCategory.LowercaseLetter ||
                            previousCategory != UnicodeCategory.DecimalDigitNumber &&
                            previousCategory != null &&
                            currentIndex > 0 &&
                            currentIndex + 1 < name.Length &&
                            char.IsLower(name[currentIndex + 1]))
                        {
                            builder.Append(divider);
                        }

                        currentChar = char.ToLower(currentChar);
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (previousCategory == UnicodeCategory.SpaceSeparator)
                            builder.Append(divider);
                        break;

                    default:
                        if (previousCategory != null)
                            previousCategory = UnicodeCategory.SpaceSeparator;
                        continue;
                }

                builder.Append(currentChar);
                previousCategory = currentCategory;
            }

            return builder.ToString();
        }
    }
}
