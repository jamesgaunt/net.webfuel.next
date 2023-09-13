using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel
{
    internal interface IQueryLexer
    {
        int Count { get; }

        QueryToken? At(int index);

        bool Is(int index, params string[] types);

        ReadOnlySpan<char> Extract(int index);
    }

    internal class QueryLexer : IQueryLexer, IDisposable
    {
        private readonly string Source;
        private readonly List<QueryToken> Tokens = new List<QueryToken>();

        public QueryLexer(string source)
        {
            Source = source;
        }

        public int Count { get { return Tokens.Count; } }

        public void Clear()
        {
            foreach (var token in Tokens)
                token.Release();
            Tokens.Clear();
        }

        public void Dispose()
        {
            this.Clear();
        }

        // Tokenise

        public int Tokenise()
        {
            Clear();

            var index = 0;
            var source = Source.AsSpan();

            var token = Tokenise(source, index);
            while (token != null)
            {
                if (token.Type == QueryTokenType.EndOfScriptToken)
                    break;

                if (token.Type != QueryTokenType.WhitespaceToken)
                    Tokens.Add(token);

                index += token.Length;
                token = Tokenise(source, index);
            }

            if (index != source.Length)
                return index;

            return 0;
        }

        QueryToken? Tokenise(ReadOnlySpan<char> source, int index)
        {
            return
                TokeniseEndOfScript(source, index, QueryTokenType.EndOfScriptToken) ??
                TokeniseWhitespace(source, index, QueryTokenType.WhitespaceToken) ??
                TokeniseStringLiteral(source, index, QueryTokenType.StringLiteral, '\"', '\'') ??
                TokeniseNumericLiteral(source, index) ??
                TokeniseKeyword(source, index, QueryTokenType.BooleanLiteral, QueryTokenType.TrueKeyword, QueryTokenType.FalseKeyword) ??
                TokeniseKeyword(source, index, QueryTokenType.NullLiteral, QueryTokenType.NullKeyword) ??
                TokeniseKeyword(source, index, null, Keywords) ??
                TokenisePunctuation(source, index, Punctuation) ??
                TokeniseIdentifier(source, index, QueryTokenType.IdentifierToken);
        }

        static string[] Punctuation = new string[]
        {
            // Double Punctuation
            QueryTokenType.BarBarToken,
            QueryTokenType.AmpersandAmpersandToken,
            QueryTokenType.ExclamationEqualsToken,
            QueryTokenType.LessThanEqualsToken,
            QueryTokenType.GreaterThanEqualsToken,

            // Single Punctuation
            QueryTokenType.OpenParenToken,
            QueryTokenType.CloseParenToken,
            QueryTokenType.MinusToken,
            QueryTokenType.PlusToken,
            QueryTokenType.EqualsToken,
            QueryTokenType.OpenBracketToken,
            QueryTokenType.CloseBracketToken,
            QueryTokenType.DoubleQuoteToken,
            QueryTokenType.SingleQuoteToken,
            QueryTokenType.LessThanToken,
            QueryTokenType.GreaterThanToken,
            QueryTokenType.DotToken,
            QueryTokenType.CommaToken,
        };

        static string[] Keywords = new string[]
        {
            QueryTokenType.SelectKeyword,
            QueryTokenType.SearchKeyword,
            QueryTokenType.WhereKeyword,
            QueryTokenType.OrderKeyword,
            QueryTokenType.SkipKeyword,
            QueryTokenType.TakeKeyword,
            QueryTokenType.ByKeyword,
        };

        /// <summary>
        /// Returns the token at the specified index relative to the start of the stream, or null if the index is outside of the stream 
        /// </summary>
        public QueryToken? At(int index)
        {
            if (index < 0 || index >= Tokens.Count)
                return null;
            return Tokens[index];
        }

        /// <summary>
        /// If the token at the specified index is of one of the specified types then return true, otherwise false
        /// </summary>
        public bool Is(int index, params string[] types)
        {
            if (At(index) != null && types.Contains(At(index)!.Type))
                return true;
            return false;
        }

        public ReadOnlySpan<char> Extract(int index)
        {
            var token = Tokens[index];
            return Source.AsSpan(token.Index, token.Length);
        }

        // Tokenisers

        QueryToken? TokeniseWhitespace(ReadOnlySpan<char> source, int index, string type)
        {
            if (!IsWhitespaceCharacter(CharAt(source, index)))
                return null; // We do not have the start of some whitespace

            int length = 0;
            while (index + length < source.Length)
            {
                if (!char.IsWhiteSpace(source[index + length]))
                {
                    if (length == 0)
                        return null;
                    return QueryToken.Create(index, length, type);
                }
                length++;
            }
            if (length == 0)
                return null;
            return QueryToken.Create(index, length, type);
        }

        QueryToken? TokeniseEndOfScript(ReadOnlySpan<char> source, int index, string type)
        {
            if (index >= source.Length)
                return QueryToken.Create(index, 0, type);
            return null;
        }

        QueryToken? TokeniseIdentifier(ReadOnlySpan<char> source, int index, string type)
        {
            if (!IsIdentifierStartCharacter(CharAt(source, index)))
                return null; // We do not have the start of an identifier

            int length = 1;
            while (index + length < source.Length)
            {
                char c = CharAt(source, index + length);
                if (!IsIdentifierCharacter(c))
                    break;
                length++;
            }

            return QueryToken.Create(index, length, type);
        }

        QueryToken? TokeniseKeyword(ReadOnlySpan<char> source, int index, string? type, params string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                if (!IsKeywordAt(source, index, keyword))
                    continue;

                return QueryToken.Create(index, keyword.Length, type ?? keyword);
            }

            return null;
        }

        // Note: Always provide punctuations longest first
        QueryToken? TokenisePunctuation(ReadOnlySpan<char> source, int index, params string[] punctuations)
        {
            foreach (var punctuation in punctuations)
            {
                if (index + punctuation.Length > source.Length)
                    continue;

                int i;
                for (i = 0; i < punctuation.Length; i++)
                {
                    if (punctuation[i] != source[index + i])
                        break;
                }
                if (i < punctuation.Length)
                    continue;
                return QueryToken.Create(index, punctuation.Length, punctuation);
            }
            return null;
        }

        QueryToken? TokeniseStringLiteral(ReadOnlySpan<char> source, int index, string type, params char[] delimiters)
        {
            var d = CharAt(source, index);
            if (!delimiters.Contains(d))
                return null; // We do not have the start of a string literal

            int length = 1;
            while (index + length < source.Length)
            {
                char c = CharAt(source, index + length);

                // If we have an unescaped delimiter then we are done
                if (c == d && CharAt(source, index + length - 1) != '\\')
                {
                    length++;
                    break;
                }

                // If we have a line break then we don't have a string literal
                if (IsLineBreakCharacter(c))
                    return null;

                length++;
            }

            // Did we run out of source before finding a delimiter?
            if (CharAt(source, index + length - 1) != d)
                return null;

            return QueryToken.Create(index, length, type);
        }

        QueryToken? TokeniseNumericLiteral(ReadOnlySpan<char> source, int index)
        {
            if (!IsNumericCharacter(CharAt(source, index)))
                return null; // All numeric literals must start with a number

            // First scan until we find the end of the number itself
            int length = 1;
            bool hasDecimalPoint = false;
            while (index + length < source.Length)
            {
                char c = CharAt(source, index + length);

                if (!IsNumericCharacter(c))
                {
                    if (c == '.')
                    {
                        if (hasDecimalPoint)
                            break; // We already had a decimal point in this number, so this is the end of the token
                        else
                            hasDecimalPoint = true;
                    }
                    else
                    {
                        break; // No more numbers signifies the end of the number
                    }
                }
                length++;
            }

            // Now, is the next character a literal suffix, and let's parse...
            var s1 = char.ToLower(CharAt(source, index + length));
            var s2 = char.ToLower(CharAt(source, index + length + 1));

            var type = QueryTokenType.Int32Literal;

            if (s1 == 'f')
            {
                float f;
                if (!float.TryParse(source.Slice(index, length), out f))
                    return null;
                length++;
                type = QueryTokenType.SingleLiteral;
            }
            else if (s1 == 'd')
            {
                double d;
                if (!double.TryParse(source.Slice(index, length), out d))
                    return null;
                length++;
                type = QueryTokenType.DoubleLiteral;
            }
            else if (s1 == 'm')
            {
                decimal m;
                if (!decimal.TryParse(source.Slice(index, length), out m))
                    return null;
                length++;
                type = QueryTokenType.DecimalLiteral;
            }
            else if (s1 == 'l')
            {
                if (hasDecimalPoint)
                    return null;
                long l;
                if (!long.TryParse(source.Slice(index, length), out l))
                    return null;
                length++;
                type = QueryTokenType.Int64Literal;
            }
            /*
            else if (s1 == 'u' && s2 == 'l')
            {
                if (hasDecimalPoint)
                    return null;
                ulong ul;
                if (!ulong.TryParse(source.Slice(index, length), out ul))
                    return null;
                length += 2;
            }
            else if (s1 == 'u')
            {
                if (hasDecimalPoint)
                    return null;
                uint u;
                if (!uint.TryParse(source.Slice(index, length), out u))
                    return null;
                length++;
            }
            */
            else if (hasDecimalPoint)
            {
                double d;
                if (!double.TryParse(source.Slice(index, length), out d))
                    return null;
                type = QueryTokenType.DoubleLiteral;
            }
            else
            {
                int i;
                if (!Int32.TryParse(source.Slice(index, length), out i))
                    return null;
                type = QueryTokenType.Int32Literal;
            }

            return QueryToken.Create(index, length, type);
        }

        // Helpers

        char CharAt(ReadOnlySpan<char> source, int index)
        {
            if (source == null || index < 0 || index >= source.Length)
                return '\0';
            return source[index];
        }

        bool IsKeywordAt(ReadOnlySpan<char> source, int index, string keyword)
        {
            if (!IsMatchAt(source, index, keyword))
                return false;

            if (IsIdentifierCharacter(CharAt(source, index + keyword.Length)))
                return false;

            return true;
        }

        bool IsMatchAt(ReadOnlySpan<char> source, int index, string match)
        {
            for (var i = 0; i < match.Length; i++)
            {
                if (CharAt(source, index + i) != match[i])
                    return false;
            }
            return true;
        }

        bool IsWhitespaceCharacter(char c)
        {
            return char.IsWhiteSpace(c);
        }

        bool IsLineBreakCharacter(char c)
        {
            return c == '\n' || c == '\r';
        }

        bool IsIdentifierStartCharacter(char c)
        {
            return char.IsLetter(c) || c == '_';
        }

        bool IsIdentifierCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        bool IsNumericCharacter(char c)
        {
            return char.IsNumber(c);
        }
    }
}

