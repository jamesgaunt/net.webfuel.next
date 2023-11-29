using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Webfuel.Scribble
{
    public static class StringUtility
    {
        public static string UnescapeString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            return input.Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\r", "\r").Replace("\\\'", "\'").Replace("\\\"", "\"").Replace("\\\\", "\\");
        }

        public static string EscapeString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            return input.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r").Replace("\"", "\\").Replace("\'", "\\\'");
        }
        
        public static Int32 Hash(string input)
        {
            return Hash(input.AsSpan());
        }

        public static Int32 Hash(ReadOnlySpan<char> input)
        {
            unchecked
            {
                int hash = 17;
                for (var i = 0; i < input.Length; i++)
                    hash = hash * 31 + input[i].GetHashCode();
                return hash;
            }
        }
    }
}
