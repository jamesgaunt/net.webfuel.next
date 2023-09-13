using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    internal static class QueryTokenType
    {
        // Single Punctuation
        public const string OpenParenToken = "(";
        public const string CloseParenToken = ")";
        public const string MinusToken = "-";
        public const string PlusToken = "+";
        public const string EqualsToken = "=";
        public const string OpenBracketToken = "[";
        public const string CloseBracketToken = "]";
        public const string DoubleQuoteToken = "\"";
        public const string SingleQuoteToken = "\'";
        public const string LessThanToken = "<";
        public const string GreaterThanToken = ">";
        public const string DotToken = ".";
        public const string CommaToken = ",";

        // Double Punctuation
        public const string BarBarToken = "||";
        public const string AmpersandAmpersandToken = "&&";
        public const string ExclamationEqualsToken = "!=";
        public const string LessThanEqualsToken = "<=";
        public const string GreaterThanEqualsToken = ">=";

        // Keywords
        public const string SelectKeyword = "SELECT";
        public const string SearchKeyword = "SEARCH";
        public const string WhereKeyword = "WHERE";
        public const string OrderKeyword = "ORDER";
        public const string SkipKeyword = "SKIP";
        public const string TakeKeyword = "TAKE";
        public const string ByKeyword = "BY";

        public const string TrueKeyword = "true";
        public const string FalseKeyword = "false";
        public const string NullKeyword = "null";

        // Literals
        public const string StringLiteral = "STRING_LITERAL";
        public const string Int32Literal = "INT32_LITERAL";
        public const string Int64Literal = "INT64_LITERAL";
        public const string DoubleLiteral = "DOUBLE_LITERAL";
        public const string SingleLiteral = "SINGLE_LITERAL";
        public const string DecimalLiteral = "DECIMAL_LITERAL";
        public const string BooleanLiteral = "BOOLEAN_LITERAL";
        public const string NullLiteral = "NULL_LITERAL";

        // Others
        public const string IdentifierToken = "IDENTIFIER";
        public const string WhitespaceToken = "WHITESPACE";
        public const string EndOfScriptToken = "END_OF_SCRIPT";
    }
}
