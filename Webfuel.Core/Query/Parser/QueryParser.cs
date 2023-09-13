using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    public partial class QueryParser
    {
        QueryFilter? ParseFilter()
        {
            int consumed = 0;
            var filter = FilterExpr(0, ref consumed);

            if (filter == null)
                return null;

            if(consumed != Lexer.Count)
            {
                PushError("Not all tokens consumed", consumed);
                return null;
            }

            return filter;
        }

        QueryFilter? FilterExpr(int index, ref int consumed)
        {
            return LogicalOrExpr(index, ref consumed);
        }

        QueryFilter? LogicalOrExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = LogicalAndExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, QueryTokenType.BarBarToken))
            {
                index++;

                var rhs = LogicalOrExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = new QueryFilter { Op = QueryOp.Or, Filters = new List<QueryFilter> { lhs, rhs } };
            }

            consumed = index - start;
            return lhs;
        }

        QueryFilter? LogicalAndExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = PrimaryExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, QueryTokenType.AmpersandAmpersandToken))
            {
                index++;

                var rhs = LogicalAndExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = new QueryFilter { Op = QueryOp.And, Filters = new List<QueryFilter> { lhs, rhs } };
            }

            consumed = index - start;
            return lhs;
        }

        QueryFilter? PrimaryExpr(int index, ref int consumed)
        {
            var result =
                ParenthesisedExpr(index, ref consumed) ??
                BinaryExpr(index, ref consumed);

            if (result == null)
            {
                PushError("Expected primary expression", index);
                return null;
            }

            return result;
        }

        QueryFilter? BinaryExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var field = FieldExpr(index, ref temp);
            if (field == null)
                return null;
            index += temp;

            if (!Lexer.Is(index, 
                QueryTokenType.LessThanToken, 
                QueryTokenType.GreaterThanToken, 
                QueryTokenType.LessThanEqualsToken, 
                QueryTokenType.GreaterThanEqualsToken,
                QueryTokenType.EqualsToken,
                QueryTokenType.ExclamationEqualsToken))
                return null;

            var op = QueryOp.ParseOp(Lexer.At(index)!.Type);
            if (op == null)
                return null;

            index++;

            var value = ValueExpr(index, ref temp);
            if (temp == 0)
                return null;
            index += temp;

            // TODO: Replace = NULL or != NULL with IS NULL / IS NOT NULL

            consumed = index - start;
            return new QueryFilter(field, op, value);
        }

        QueryFilter? ParenthesisedExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, QueryTokenType.OpenParenToken))
                return null;
            index++;

            var filter = FilterExpr(index, ref temp);
            if (filter == null)
                return null;
            index += temp;

            if (!Lexer.Is(index, QueryTokenType.CloseParenToken))
            {
                PushError("Expected closing parenthesis", index);
                return null;
            }
            index++;

            consumed = index - start;
            return filter;
        }


        string? FieldExpr(int index, ref int consumed)
        {
            int start = index;
            var sb = new StringBuilder();

            if (!Lexer.Is(index, QueryTokenType.IdentifierToken))
                return null;
            sb.Append(Lexer.Extract(index).ToString());
            index++;

            while(Lexer.Is(index, QueryTokenType.DotToken))
            {
                sb.Append(".");
                index++;

                if (!Lexer.Is(index, QueryTokenType.IdentifierToken))
                    return null;
                sb.Append(Lexer.Extract(index).ToString());
                index++;
            }

            consumed = index - start;
            return sb.ToString();
        }

        object? ValueExpr(int index, ref int consumed)
        {
            if (Lexer.Is(index, QueryTokenType.StringLiteral))
            {
                var span = Lexer.Extract(index);
                var value = UnescapeString(span.Slice(1, span.Length - 2).ToString());
                consumed = 1;
                return value;
            }
            else if (Lexer.Is(index, QueryTokenType.BooleanLiteral))
            {
                var span = Lexer.Extract(index);
                if (span.CompareTo(QueryTokenType.TrueKeyword, StringComparison.Ordinal) == 0)
                {
                    consumed = 1;
                    return true;
                }
                if (span.CompareTo(QueryTokenType.FalseKeyword, StringComparison.Ordinal) == 0)
                {
                    consumed = 1;
                    return false;
                }
                return null;
            }
            else if (Lexer.Is(index, QueryTokenType.NullLiteral))
            {
                consumed = 1;
                return null;
            }
            else if (Lexer.Is(index, QueryTokenType.Int32Literal))
            {
                var span = Lexer.Extract(index);
                var value = Int32.Parse(span.ToString());
                consumed = 1;
                return value;
            }
            else if (Lexer.Is(index, QueryTokenType.Int64Literal))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'l' || span[span.Length - 1] == 'L')
                    span = span.Slice(0, span.Length - 1);
                var value = Int64.Parse(span.ToString());
                consumed = 1;
                return value;
            }
            else if (Lexer.Is(index, QueryTokenType.DoubleLiteral))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'd' || span[span.Length - 1] == 'D')
                    span = span.Slice(0, span.Length - 1);
                var value = Double.Parse(span.ToString());
                consumed = 1;
                return value;
            }
            else if (Lexer.Is(index, QueryTokenType.SingleLiteral))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'f' || span[span.Length - 1] == 'F')
                    span = span.Slice(0, span.Length - 1);
                var value = float.Parse(span.ToString());
                consumed = 1;
                return value;
            }
            else if (Lexer.Is(index, QueryTokenType.DecimalLiteral))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'm' || span[span.Length - 1] == 'M')
                    span = span.Slice(0, span.Length - 1);
                var value = Decimal.Parse(span.ToString());
                consumed = 1;
                return value;
            }

            consumed = 0;
            return null;
        }

        static string UnescapeString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            return input.Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\r", "\r").Replace("\\\'", "\'").Replace("\\\"", "\"").Replace("\\\\", "\\");
        }
    }
}
