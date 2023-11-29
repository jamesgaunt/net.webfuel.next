using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    internal partial class ScribbleParser
    {
        List<ScribbleStatement>? ParseStatements()
        {
            int consumed = 0;
            var statements = new List<ScribbleStatement>();

            var index = 0;
            while (index != Lexer.Count)
            {
                var statement = Statement(index, ref consumed);
                if (statement == null)
                    return null;

                statements.Add(statement);
                index += consumed;
            }

            return statements;
        }

        ScribbleStatement? Statement(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var statement =
                StatementBlock(index, ref temp) ??
                StatementIf(index, ref temp) ??
                StatementForEach(index, ref temp) ??
                StatementFor(index, ref temp) ??
                StatementReturn(index, ref temp) ??
                StatementWriteExpr(index, ref temp) ??
                StatementWriteText(index, ref temp) ??
                StatementVar(index, ref temp) ??
                StatementExpr(index, ref temp);

            if (statement == null)
                return null;
            index += temp;

            consumed = index - start;
            return statement;
        }

        ScribbleStatement? StatementBlock(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.OpenBraceToken))
                return null;
            index++;

            var statements = new List<ScribbleStatement>();
            while (!Lexer.Is(index, ScribbleTokenType.CloseBraceToken))
            {
                var statement = Statement(index, ref temp);
                if (statement == null)
                {
                    PushError("Expected block statement", index);
                    return null;
                }
                index += temp;
                statements.Add(statement);
            }
            index++;

            consumed = index - start;
            return ScribbleStatementBlock.Create(statements);
        }

        ScribbleStatement? StatementVar(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.VarKeyword))
                return null;
            index++;

            var variables = new List<ScribbleExprArgument>();

            while (true)
            {
                var identifier = IdentifierExpr(index, ref temp) as ScribbleExprIdentifier;
                if (identifier == null)
                {
                    PushError("Expected var identifier", index);
                    return null;
                }
                index += temp;

                if (!Lexer.Is(index, ScribbleTokenType.EqualsToken))
                {
                    PushError("Expected var equals", index);
                    return null;
                }
                index++;

                var expr = Expr(index, ref temp);
                if (expr == null)
                {
                    PushError("Expected var expression", index);
                    return null;
                }
                index += temp;

                variables.Add(ScribbleExprArgument.Create(identifier, expr));

                if (Lexer.Is(index, ScribbleTokenType.CommaToken))
                {
                    index++;
                    continue;
                }

                if (!Lexer.Is(index, ScribbleTokenType.SemicolonToken))
                {
                    PushError("Expected var semicolon", index);
                    return null;
                }
                index++;

                break;
            }

            consumed = index - start;
            return ScribbleStatementVar.Create(variables);
        }

        ScribbleStatement? StatementIf(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.IfKeyword))
                return null;
            index++;

            var parts = new List<ScribbleIfPart>();
            ScribbleStatement? @default = null;

            while (true)
            {
                // If
                if (parts.Count == 0)
                {
                    var part = StatementIfPart(index, ref temp);
                    if (part == null)
                        return null;
                    parts.Add(part);
                    index += temp;
                    continue;
                }

                if (!Lexer.Is(index, ScribbleTokenType.ElseKeyword))
                    break; // We are done
                index++;

                // Else If
                if (Lexer.Is(index, ScribbleTokenType.IfKeyword))
                {
                    index++;
                    var part = StatementIfPart(index, ref temp);
                    if (part == null)
                        return null;
                    parts.Add(part);
                    index += temp;
                    continue;
                }

                // Else 
                @default = Statement(index, ref temp);
                if (@default == null)
                {
                    PushError("Expected else statement", index);
                    return null;
                }
                index += temp;
                break;
            }

            consumed = index - start;
            return ScribbleStatementIf.Create(parts, @default);
        }

        ScribbleIfPart? StatementIfPart(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
            {
                PushError("Expected if part opening parenthesis", index);
                return null;
            }
            index++;

            var condition = Expr(index, ref temp);
            if (condition == null)
            {
                PushError("Expected if part conditional expression", index);
                return null;
            }
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
            {
                PushError("Expected if part closing parenthesis", index);
                return null;
            }
            index++;

            var statement = Statement(index, ref temp);
            if (statement == null)
            {
                PushError("Expected if part statement", index);
                return null;
            }
            index += temp;

            consumed = index - start;
            return new ScribbleIfPart(condition, statement);
        }

        ScribbleStatement? StatementExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var expr = Expr(index, ref temp);
            if (expr == null)
                return null;
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.SemicolonToken))
            {
                PushError("Expected expression statement semicolon", index);
                return null;
            }
            index++;

            consumed = index - start;
            return ScribbleStatementExpr.Create(expr);
        }

        ScribbleStatement? StatementWriteExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.OpenTemplateExpr, ScribbleTokenType.OpenTemplateExprTrim, ScribbleTokenType.OpenTemplateExprHtmlAttribute))
                return null;
            index++;

            var expr = Expr(index, ref temp);
            if (expr == null)
            {
                PushError("Expected template expression", index);
                return null;
            }
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseTemplateExpr, ScribbleTokenType.CloseTemplateExprTrim, ScribbleTokenType.CloseTemplateExprHtmlAttribute))
            {
                PushError("Expected template expression closing tag", index);
                return null;
            }
            var htmlAttributeEncode = Lexer.Is(index, ScribbleTokenType.CloseTemplateExprHtmlAttribute);

            index++;

            consumed = index - start;
            return ScribbleStatementWriteExpr.Create(expr, htmlAttributeEncode);
        }

        ScribbleStatement? StatementWriteText(int index, ref int consumed)
        {
            int start = index;

            // Consume CloseTemplateCode if exists
            if (Lexer.Is(index, ScribbleTokenType.CloseTemplateCode, ScribbleTokenType.CloseTemplateCodeTrim))
                index++;

            if (!Lexer.Is(index, ScribbleTokenType.TemplateText))
                return null;
            var text = Lexer.Extract(index);
            index++;

            // Consume OpenTemplateCode if exists
            if (Lexer.Is(index, ScribbleTokenType.OpenTemplateCode, ScribbleTokenType.OpenTemplateCodeTrim))
                index++;

            consumed = index - start;
            return ScribbleStatementWriteText.Create(text);
        }

        ScribbleStatement? StatementReturn(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.ReturnKeyword))
                return null;
            index++;

            ScribbleExpr? expr = null;
            if (!Lexer.Is(index, ScribbleTokenType.SemicolonToken))
            {
                expr = Expr(index, ref temp);
                if (expr == null)
                {
                    PushError("Expected return expression", index);
                    return null;
                }
                index += temp;
            }

            if (!Lexer.Is(index, ScribbleTokenType.SemicolonToken))
            {
                PushError("Expected return semicolon", index);
                return null;
            }
            index++;

            consumed = index - start;
            return ScribbleStatementReturn.Create(expr);
        }

        ScribbleStatement? StatementForEach(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.ForEachKeyword))
                return null;
            index++;

            if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
            {
                PushError("Expected foreach loop opening parenthesis", index);
                return null;
            }
            index++;

            if (!Lexer.Is(index, ScribbleTokenType.VarKeyword))
            {
                PushError("Expected foreach loop var keyword", index);
                return null;
            }
            index++;

            var identifier = IdentifierExpr(index, ref temp) as ScribbleExprIdentifier;
            if (identifier == null)
            {
                PushError("Expected foreach loop identifier", index);
                return null;
            }
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.InKeyword))
            {
                PushError("Expected foreach loop in keyword", index);
                return null;
            }
            index++;

            var collection = Expr(index, ref temp);
            if (collection == null)
            {
                PushError("Expected foreach loop collection expression", index);
                return null;
            }
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
            {
                PushError("Expected foreach loop closing parenthesis", index);
                return null;
            }
            index++;

            var body = Statement(index, ref temp);
            if (body == null)
            {
                PushError("Expected foreach loop body statement", index);
                return null;
            }
            index += temp;

            consumed = index - start;
            return ScribbleStatementForEach.Create(identifier, collection, body);
        }

        ScribbleStatement? StatementFor(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.ForKeyword))
                return null;
            index++;

            if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
            {
                PushError("Expected for loop opening parenthesis", index);
                return null;
            }
            index++;

            var initialiser = StatementVar(index, ref temp) ?? StatementExpr(index, ref temp);
            if(initialiser == null)
            {
                PushError("For loop initialiser must be a variable or expression statement", index);
                return null;
            }
            index += temp;

            var condition = StatementExpr(index, ref temp) as ScribbleStatementExpr;
            if(condition == null)
            {
                PushError("For loop condition must be an expression statement", index);
                return null;
            }
            index += temp;

            var iterator = Expr(index, ref temp);
            if(iterator == null)
            {
                PushError("For loop iterator must be an expression", index);
                return null;
            }
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
            {
                PushError("Expected for loop closing parenthesis", index);
                return null;
            }
            index++;

            var body = Statement(index, ref temp);
            if (body == null)
            {
                PushError("Expected for loop body statement", index);
                return null;
            }
            index += temp;

            consumed = index - start;
            return ScribbleStatementFor.Create(initialiser, condition.Expr, iterator, body);
        }
    }
}
