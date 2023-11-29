using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    internal class ScribbleLexer : IDisposable
    {
        private readonly string Source;
        private readonly List<ScribbleToken> Tokens = new List<ScribbleToken>();

        public ScribbleLexer(string source)
        {
            Source = source;
        }

        public int Count { get { return Tokens.Count; } }

        public (int, int) GetSourcePosition(int index)
        {
            var row = 0;
            var col = 0;

            for (var i = 0; i < Source.Length; i++)
            {
                if(i >= index)
                    return (col + 1, row + 1);

                if (Source[i] == '\n')
                {
                    row++;
                    col = 0;
                }
                else
                {
                    col++;
                }
            }
            return (0, 0);
        }

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

        bool InTemplateText = false;

        public int Tokenise(bool template)
        {
            Clear();

            if (template)
                InTemplateText = true;

            var index = 0;
            var source = Source.AsSpan();

            var token = Tokenise(source, index);
            while (token != null)
            {
                if (token.Type == ScribbleTokenType.EndOfScriptToken)
                    break;

                if (token.Type == ScribbleTokenType.TemplateText)
                    InTemplateText = false;

                if (token.Type == ScribbleTokenType.CloseTemplateCode || token.Type == ScribbleTokenType.CloseTemplateExpr || token.Type == ScribbleTokenType.CloseTemplateCodeTrim || token.Type == ScribbleTokenType.CloseTemplateExprTrim || token.Type == ScribbleTokenType.CloseTemplateExprHtmlAttribute)
                    InTemplateText = true;

                if (token.Type != ScribbleTokenType.WhitespaceToken)
                    Tokens.Add(token);

                index += token.Length;
                token = Tokenise(source, index);
            }

            if (index != source.Length)
                return index;

            // If this is a template implement trimming on any adjacent template text tokens
            if (template)
            {
                for (var i = 1; i < Tokens.Count - 1; i++)
                {
                    if (Is(i, ScribbleTokenType.OpenTemplateCodeTrim, ScribbleTokenType.OpenTemplateExprTrim) && Is(i - 1, ScribbleTokenType.TemplateText))
                    {
                        var text = Tokens[i - 1];
                        while (text.Length > 0 && char.IsWhiteSpace(source[text.Index + text.Length - 1]))
                        {
                            text.Length--;
                        }
                    }
                    else if (Is(i, ScribbleTokenType.CloseTemplateCodeTrim, ScribbleTokenType.CloseTemplateExprTrim) && Is(i + 1, ScribbleTokenType.TemplateText))
                    {
                        var text = Tokens[i + 1];
                        while (text.Length > 0 && char.IsWhiteSpace(source[text.Index]))
                        {
                            text.Index++;
                            text.Length--;
                        }
                    }
                }
            }

            return 0;
        }

        ScribbleToken? Tokenise(ReadOnlySpan<char> source, int index)
        {
            if (InTemplateText)
                return TokeniseTemplateText(source, index, ScribbleTokenType.TemplateText);

            return
                TokeniseEndOfScript(source, index, ScribbleTokenType.EndOfScriptToken) ??
                TokeniseWhitespace(source, index, ScribbleTokenType.WhitespaceToken) ??
                TokeniseBlockComment(source, index, ScribbleTokenType.WhitespaceToken) ??
                TokeniseLineComment(source, index, ScribbleTokenType.WhitespaceToken) ??
                TokeniseStringLiteral(source, index, ScribbleTokenType.StringLiteral, '\"') ??
                TokeniseCharLiteral(source, index, ScribbleTokenType.CharLiteral, '\'') ??
                TokeniseNumericLiteral(source, index) ??
                TokeniseKeyword(source, index, ScribbleTokenType.BooleanLiteral, ScribbleTokenType.TrueKeyword, ScribbleTokenType.FalseKeyword) ??
                TokeniseKeyword(source, index, ScribbleTokenType.NullLiteral, ScribbleTokenType.NullKeyword) ??
                TokeniseKeyword(source, index, null, Keywords) ??
                TokenisePunctuation(source, index, Punctuation) ??
                TokeniseIdentifier(source, index, ScribbleTokenType.IdentifierToken);
        }

        static string[] Punctuation = new string[]
        {
            // Triple Template Tags
            ScribbleTokenType.OpenTemplateExprTrim,
            ScribbleTokenType.CloseTemplateExprTrim,
            ScribbleTokenType.OpenTemplateCodeTrim,
            ScribbleTokenType.CloseTemplateCodeTrim,

            ScribbleTokenType.OpenTemplateExprHtmlAttribute,
            ScribbleTokenType.CloseTemplateExprHtmlAttribute,

            // Double Template Tags
            ScribbleTokenType.OpenTemplateExpr,
            ScribbleTokenType.CloseTemplateExpr,
            ScribbleTokenType.OpenTemplateCode,
            ScribbleTokenType.CloseTemplateCode,

            // Double Punctuation
            ScribbleTokenType.BarBarToken,
            ScribbleTokenType.AmpersandAmpersandToken,
            ScribbleTokenType.MinusMinusToken,
            ScribbleTokenType.PlusPlusToken,
            ScribbleTokenType.ColonColonToken,
            ScribbleTokenType.QuestionQuestionToken,
            ScribbleTokenType.DollarQuestionToken,
            ScribbleTokenType.ExclamationEqualsToken,
            ScribbleTokenType.EqualsEqualsToken,
            ScribbleTokenType.LessThanEqualsToken,
            ScribbleTokenType.GreaterThanEqualsToken,
            ScribbleTokenType.PlusEqualsToken,
            ScribbleTokenType.MinusEqualsToken,
            ScribbleTokenType.LambdaToken,

            // Single Punctuation
            ScribbleTokenType.TildeToken,
            ScribbleTokenType.ExclamationToken,
            ScribbleTokenType.DollarToken,
            ScribbleTokenType.PercentToken,
            ScribbleTokenType.CaretToken,
            ScribbleTokenType.AmpersandToken,
            ScribbleTokenType.AsteriskToken,
            ScribbleTokenType.OpenParenToken,
            ScribbleTokenType.CloseParenToken,
            ScribbleTokenType.MinusToken,
            ScribbleTokenType.PlusToken,
            ScribbleTokenType.EqualsToken,
            ScribbleTokenType.OpenBraceToken,
            ScribbleTokenType.CloseBraceToken,
            ScribbleTokenType.OpenBracketToken,
            ScribbleTokenType.CloseBracketToken,
            ScribbleTokenType.BarToken,
            ScribbleTokenType.BackslashToken,
            ScribbleTokenType.ColonToken,
            ScribbleTokenType.SemicolonToken,
            ScribbleTokenType.DoubleQuoteToken,
            ScribbleTokenType.SingleQuoteToken,
            ScribbleTokenType.LessThanToken,
            ScribbleTokenType.GreaterThanToken,
            ScribbleTokenType.DotToken,
            ScribbleTokenType.CommaToken,
            ScribbleTokenType.QuestionToken,
            ScribbleTokenType.HashToken,
            ScribbleTokenType.SlashToken,
        };

        static string[] Keywords = new string[]
        {
            ScribbleTokenType.ForEachKeyword,
            ScribbleTokenType.ReturnKeyword,
            ScribbleTokenType.AwaitKeyword,
            ScribbleTokenType.CycleKeyword,
            ScribbleTokenType.ElseKeyword,
            ScribbleTokenType.VarKeyword,
            ScribbleTokenType.ForKeyword,
            ScribbleTokenType.NewKeyword,
            ScribbleTokenType.IfKeyword,
            ScribbleTokenType.InKeyword,
        };

        /// <summary>
        /// Returns the token at the specified index relative to the start of the stream, or null if the index is outside of the stream 
        /// </summary>
        public ScribbleToken? At(int index)
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

        public ReadOnlySpan<char> Extract(int startIndex, int indexCount)
        {
            if (indexCount <= 0)
                return ReadOnlySpan<char>.Empty;

            var length = 0;
            while(indexCount > 0)
            {
                var token = Tokens[startIndex + indexCount - 1];
                length += token.Length;
                indexCount--;
            }
            return Source.AsSpan(Tokens[startIndex].Index, length);
        }

        // Tokenisers

        ScribbleToken? TokeniseTemplateText(ReadOnlySpan<char> source, int index, string type)
        {
            int length = 0;
            while (index + length < source.Length)
            {
                if (CharAt(source, index + length) == '{' || CharAt(source, index + length) == '[')
                {
                    if (CharAt(source, index + length + 1) == '%')
                        break;
                }
                length++;
            }
            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseWhitespace(ReadOnlySpan<char> source, int index, string type)
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
                    return ScribbleToken.Create(index, length, type);
                }
                length++;
            }
            if (length == 0)
                return null;
            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseBlockComment(ReadOnlySpan<char> source, int index, string type)
        {
            if (CharAt(source, index) != '/' || CharAt(source, index + 1) != '*')
                return null; // We do not have the start of a multi-line comment

            int length = 2;
            while (index + length < source.Length)
            {
                if (length > 3 && CharAt(source, index + length - 1) == '*' && CharAt(source, index + length) == '/')
                    break;
                length++;
            }
            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseLineComment(ReadOnlySpan<char> source, int index, string type)
        {
            if (CharAt(source, index) != '/' || CharAt(source, index + 1) != '/')
                return null; // We do not have the start of a single-line comment

            int length = 2;
            bool lineBroken = false;
            while (index + length < source.Length)
            {
                char c = CharAt(source, index + length);

                // Continue until we reach a the first non line-break character after a line-break character
                if (IsLineBreakCharacter(c))
                    lineBroken = true;
                else if (lineBroken && !IsLineBreakCharacter(c))
                    break;

                length++;
            }
            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseEndOfScript(ReadOnlySpan<char> source, int index, string type)
        {
            if (index >= source.Length)
                return ScribbleToken.Create(index, 0, type);
            return null;
        }

        ScribbleToken? TokeniseIdentifier(ReadOnlySpan<char> source, int index, string type)
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

            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseKeyword(ReadOnlySpan<char> source, int index, string? type, params string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                if (!IsKeywordAt(source, index, keyword))
                    continue;

                return ScribbleToken.Create(index, keyword.Length, type ?? keyword);
            }

            return null;
        }

        // Note: Always provide punctuations longest first
        ScribbleToken? TokenisePunctuation(ReadOnlySpan<char> source, int index, params string[] punctuations)
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
                return ScribbleToken.Create(index, punctuation.Length, punctuation);
            }
            return null;
        }

        ScribbleToken? TokeniseStringLiteral(ReadOnlySpan<char> source, int index, string type, params char[] delimiters)
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

            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseCharLiteral(ReadOnlySpan<char> source, int index, string type, params char[] delimiters)
        {
            var d = CharAt(source, index);
            if (!delimiters.Contains(d))
                return null; // We do not have the start of a character literal

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

                // If we have line break then we have a character literal
                if (IsLineBreakCharacter(c))
                    return null;

                length++;
            }

            // Did we run out of source before finding a delimiter?
            if (CharAt(source, index + length - 1) != d)
                return null;

            return ScribbleToken.Create(index, length, type);
        }

        ScribbleToken? TokeniseNumericLiteral(ReadOnlySpan<char> source, int index)
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

            var type = ScribbleTokenType.Int32Literal;

            if (s1 == 'f')
            {
                float f;
                if (!float.TryParse(source.Slice(index, length), out f))
                    return null;
                length++;
                type = ScribbleTokenType.SingleLiteral;
            }
            else if (s1 == 'd')
            {
                double d;
                if (!double.TryParse(source.Slice(index, length), out d))
                    return null;
                length++;
                type = ScribbleTokenType.DoubleLiteral;
            }
            else if (s1 == 'm')
            {
                decimal m;
                if (!decimal.TryParse(source.Slice(index, length), out m))
                    return null;
                length++;
                type = ScribbleTokenType.DecimalLiteral;
            }
            else if (s1 == 'l')
            {
                if (hasDecimalPoint)
                    return null;
                long l;
                if (!long.TryParse(source.Slice(index, length), out l))
                    return null;
                length++;
                type = ScribbleTokenType.Int64Literal;
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
                type = ScribbleTokenType.DoubleLiteral;
            }
            else
            {
                int i;
                if (!Int32.TryParse(source.Slice(index, length), out i))
                    return null;
                type = ScribbleTokenType.Int32Literal;
            }

            return ScribbleToken.Create(index, length, type);
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

