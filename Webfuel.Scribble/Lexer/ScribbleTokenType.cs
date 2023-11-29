using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    internal static class ScribbleTokenType
    {
        // Single Punctuation
        public const string TildeToken = "~";
        public const string ExclamationToken = "!";
        public const string DollarToken = "$";
        public const string PercentToken = "%";
        public const string CaretToken = "^";
        public const string AmpersandToken = "&";
        public const string AsteriskToken = "*";
        public const string OpenParenToken = "(";
        public const string CloseParenToken = ")";
        public const string MinusToken = "-";
        public const string PlusToken = "+";
        public const string EqualsToken = "=";
        public const string OpenBraceToken = "{";
        public const string CloseBraceToken = "}";
        public const string OpenBracketToken = "[";
        public const string CloseBracketToken = "]";
        public const string BarToken = "|";
        public const string BackslashToken = "\\";
        public const string ColonToken = ":";
        public const string SemicolonToken = ";";
        public const string DoubleQuoteToken = "\"";
        public const string SingleQuoteToken = "\'";
        public const string LessThanToken = "<";
        public const string GreaterThanToken = ">";
        public const string DotToken = ".";
        public const string CommaToken = ",";
        public const string QuestionToken = "?";
        public const string HashToken = "#";
        public const string SlashToken = "/";

        // Double Punctuation
        public const string BarBarToken = "||";
        public const string AmpersandAmpersandToken = "&&";
        public const string MinusMinusToken = "--";
        public const string PlusPlusToken = "++";
        public const string ColonColonToken = "::";
        public const string QuestionQuestionToken = "??";
        public const string DollarQuestionToken = "$?";
        public const string ExclamationEqualsToken = "!=";
        public const string EqualsEqualsToken = "==";
        public const string LessThanEqualsToken = "<=";
        public const string GreaterThanEqualsToken = ">=";
        public const string PlusEqualsToken = "+=";
        public const string MinusEqualsToken = "-=";
        public const string LambdaToken = "=>";

        // Template
        public const string TemplateText = "TEMPLATE_TEXT";

        public const string OpenTemplateExpr = "[%";
        public const string CloseTemplateExpr = "%]";
        public const string OpenTemplateCode = "{%";
        public const string CloseTemplateCode = "%}";

        public const string OpenTemplateExprTrim = "[%-";
        public const string CloseTemplateExprTrim = "-%]";
        public const string OpenTemplateCodeTrim = "{%-";
        public const string CloseTemplateCodeTrim = "-%}";

        public const string OpenTemplateExprHtmlAttribute = "[%<";
        public const string CloseTemplateExprHtmlAttribute = ">%]";

        // Keywords
        public const string ForEachKeyword = "foreach";
        public const string ReturnKeyword = "return";
        public const string AwaitKeyword = "await";
        public const string CycleKeyword = "cycle";
        public const string ElseKeyword = "else";
        public const string VarKeyword = "var";
        public const string ForKeyword = "for";
        public const string NewKeyword = "new";
        public const string IfKeyword = "if";
        public const string InKeyword = "in";

        public const string TrueKeyword = "true";
        public const string FalseKeyword = "false";
        public const string NullKeyword = "null";

        // Literals
        public const string StringLiteral = "STRING_LITERAL";
        public const string CharLiteral = "CHAR_LITERAL";
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
