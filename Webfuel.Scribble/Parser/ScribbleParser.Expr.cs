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
        ScribbleExpr? ParseExpression()
        {
            int consumed = 0;
            var result = Expr(0, ref consumed);

            if (result == null)
                return null;

            if (consumed != Lexer.Count)
            {
                PushError("Not all tokens consumed", consumed);
                return null;
            }

            return result;
        }

        ScribbleExpr? Expr(int index, ref int consumed)
        {
            return AssignmentExpr(index, ref consumed);
        }

        ScribbleExpr? AssignmentExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = ConditionalExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, ScribbleTokenType.EqualsToken, ScribbleTokenType.PlusEqualsToken, ScribbleTokenType.MinusEqualsToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = AssignmentExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? ConditionalExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var condition = CoalesceExpr(index, ref temp);
            if (condition == null)
                return null;
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.QuestionToken))
            {
                consumed = index - start;
                return condition;
            }
            index++;

            var truePart = CoalesceExpr(index, ref temp);
            if (truePart == null)
                return null;
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.ColonToken))
            {
                PushError("Expected colon", index);
                return null;
            }
            index++;

            var falsePart = CoalesceExpr(index, ref temp);
            if (falsePart == null)
                return null;
            index += temp;

            consumed = index - start;
            return ScribbleExprConditional.Create(condition, truePart, falsePart);
        }

        ScribbleExpr? CoalesceExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = LogicalOrExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, ScribbleTokenType.QuestionQuestionToken) || Lexer.Is(index, ScribbleTokenType.DollarQuestionToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = CoalesceExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? LogicalOrExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = LogicalAndExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, ScribbleTokenType.BarBarToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = LogicalOrExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? LogicalAndExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = EqualityExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, ScribbleTokenType.AmpersandAmpersandToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = LogicalAndExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? EqualityExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = RelationalExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, ScribbleTokenType.EqualsEqualsToken, ScribbleTokenType.ExclamationEqualsToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = EqualityExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? RelationalExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = AdditiveExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            if (Lexer.Is(index, ScribbleTokenType.LessThanToken, ScribbleTokenType.GreaterThanToken, ScribbleTokenType.LessThanEqualsToken, ScribbleTokenType.GreaterThanEqualsToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = AdditiveExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? AdditiveExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = MultiplicativeExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            while (Lexer.Is(index, ScribbleTokenType.PlusToken, ScribbleTokenType.MinusToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = MultiplicativeExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? MultiplicativeExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = PrefixExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            while (Lexer.Is(index, ScribbleTokenType.AsteriskToken, ScribbleTokenType.SlashToken, ScribbleTokenType.PercentToken))
            {
                var op = ScribbleExprBinaryOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = PrefixExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                lhs = ScribbleExprBinaryOp.Create(lhs, op, rhs);
            }

            consumed = index - start;
            return lhs;
        }

        ScribbleExpr? PrefixExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (Lexer.Is(index, ScribbleTokenType.PlusToken, ScribbleTokenType.MinusToken, ScribbleTokenType.ExclamationToken, ScribbleTokenType.AwaitKeyword))
            {
                var op = ScribbleExprPrefixOp.ParseOp(Lexer.At(index)!.Type);
                index++;

                var rhs = PrefixExpr(index, ref temp);
                if (rhs == null)
                    return null;
                index += temp;

                consumed = index - start;
                return ScribbleExprPrefixOp.Create(op, rhs);
            }
            return PostfixExpr(index, ref consumed);
        }

        ScribbleExpr? PostfixExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            var lhs = PrimaryExpr(index, ref temp);
            if (lhs == null)
                return null;
            index += temp;

            while (Lexer.Is(index, ScribbleTokenType.OpenBracketToken, ScribbleTokenType.OpenParenToken, ScribbleTokenType.DotToken, ScribbleTokenType.PlusPlusToken, ScribbleTokenType.MinusMinusToken, ScribbleTokenType.BarToken, ScribbleTokenType.LessThanToken))
            {
                // Index
                if (Lexer.Is(index, ScribbleTokenType.OpenBracketToken))
                {
                    index++;

                    var arguments = ArgumentList(index, ref temp);
                    if (arguments == null)
                        return null;

                    index += temp;

                    if (!Lexer.Is(index, ScribbleTokenType.CloseBracketToken))
                    {
                        PushError("Expected index closing bracket", index);
                        return null;
                    }

                    index++;
                    lhs = ScribbleExprIndex.Create(lhs, arguments);
                    continue;
                }

                // Invoke
                else if (Lexer.Is(index, ScribbleTokenType.OpenParenToken) || Lexer.Is(index, ScribbleTokenType.LessThanToken))
                {
                    List<ScribbleTypeName>? typeArguments = null;

                    // We need to be a bit careful here in case this is just a comparison operator
                    if(Lexer.Is(index, ScribbleTokenType.LessThanToken))
                    {
                        typeArguments = TypeArgumentList(index, ref temp);
                        if (typeArguments != null)
                            index += temp;
                    }

                    if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
                        break; // This wasn't an invoke - we got dummied by the less than!
                    index++;

                    var arguments = ArgumentList(index, ref temp);
                    if (arguments == null)
                        return null;
                    index += temp;

                    if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
                    {
                        PushError("Expected invoke closing parenthesis", index);
                        return null;
                    }

                    index++;
                    lhs = ScribbleExprInvoke.Create(lhs, arguments, typeArguments);
                }

                // Dot / Member
                else if (Lexer.Is(index, ScribbleTokenType.DotToken))
                {
                    index++;

                    if (!Lexer.Is(index, ScribbleTokenType.IdentifierToken))
                    {
                        PushError("Expected member identifier", index);
                        return null;
                    }

                    index++;
                    lhs = ScribbleExprMember.Create(lhs, Lexer.Extract(index - 1));
                }

                // Postfix Unary Op
                else if (Lexer.Is(index, ScribbleTokenType.PlusPlusToken, ScribbleTokenType.MinusMinusToken))
                {
                    index++;

                    var op = ScribbleExprPostfixOp.ParseOp(Lexer.At(index - 1)!.Type);
                    lhs = ScribbleExprPostfixOp.Create(lhs, op);
                }

                // Pipe
                else if (Lexer.Is(index, ScribbleTokenType.BarToken))
                {
                    index++;

                    var identifier = IdentifierExpr(index, ref temp) as ScribbleExprIdentifier;
                    if (identifier == null)
                    {
                        PushError("Expected pipe identifier", index);
                        return null;
                    }
                    index += temp;

                    if (Lexer.Is(index, ScribbleTokenType.ColonToken))
                    {
                        index++;

                        var arguments = ArgumentList(index, ref temp);
                        if (arguments == null)
                            return null;

                        index += temp;

                        if (arguments.Count == 0)
                        {
                            PushError("Expected pipe argument", index);
                            return null;
                        }

                        lhs = ScribbleExprPipe.Create(lhs, identifier, arguments);
                    }
                    else
                    {
                        lhs = ScribbleExprPipe.Create(lhs, identifier, new List<ScribbleExprArgument>());
                    }
                }
            }
            consumed = index - start;
            return lhs;
        }

        List<ScribbleTypeName>? TypeArgumentList(int index, ref int consumed)
        {
            var result = new List<ScribbleTypeName>();
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.LessThanToken))
                return null;
            index++;

            do
            {
                var argument = ParseTypeName(index, ref temp);
                if (argument == null)
                {
                    PushError("Expected type argument", index);
                    return null;
                }

                result.Add(argument);
                index += temp;

                if (!Lexer.Is(index, ScribbleTokenType.CommaToken))
                    break;
                index++;
            }
            while (true);

            if (!Lexer.Is(index, ScribbleTokenType.GreaterThanToken))
            {
                PushError("Expected closure of type argument list", index);
                return null;
            }
            index++;

            if(result.Count == 0)
            {
                PushError("A type argument list cannot be empty", index);
                return null;
            }

            consumed = index - start;
            return result;
        }

        List<ScribbleExprArgument>? ArgumentList(int index, ref int consumed)
        {
            var result = new List<ScribbleExprArgument>();
            int start = index, temp = 0;

            do
            {
                ScribbleExprIdentifier? identifier = null;
                if (Lexer.Is(index, ScribbleTokenType.IdentifierToken) && Lexer.Is(index + 1, ScribbleTokenType.ColonToken))
                {
                    identifier = IdentifierExpr(index, ref temp) as ScribbleExprIdentifier;
                    index += 2;
                }

                var expr = Expr(index, ref temp);
                if (expr != null)
                {
                    result.Add(ScribbleExprArgument.Create(identifier, expr));
                    index += temp;
                }
                else
                {
                    break;
                }

                if (Lexer.Is(index, ScribbleTokenType.CommaToken))
                    index++;
            }
            while (true);

            // Check that any named arguments come after any positional arguments
            var positional = true;
            foreach (var arg in result)
            {
                if (arg.Identifier == null)
                {
                    if (positional == false)
                    {
                        PushError("Cannot have a positional argument after a named argument", index);
                        return null;
                    }
                }
                else
                {
                    positional = false;
                }
            }

            consumed = index - start;
            return result;
        }

        List<ScribbleExprIdentifier> IdentifierList(int index, ref int consumed)
        {
            var result = new List<ScribbleExprIdentifier>();
            int start = index;

            do
            {
                if (!Lexer.Is(index, ScribbleTokenType.IdentifierToken))
                    break;

                result.Add(ScribbleExprIdentifier.Create(Lexer.Extract(index)));
                index++;

                if (!Lexer.Is(index, ScribbleTokenType.CommaToken))
                    break;

                index++;
            }
            while (true);

            consumed = index - start;
            return result;
        }

        ScribbleExpr? PrimaryExpr(int index, ref int consumed)
        {
            var result =
                ConstructorExpr(index, ref consumed) ??
                LambdaExpr(index, ref consumed) ??
                ParenthesisedExpr(index, ref consumed) ??
                IdentifierExpr(index, ref consumed) ??
                LiteralExpr(index, ref consumed);

            if (result == null)
            {
                PushError("Expected primary expression", index);
                return null;
            }

            return result;
        }

        ScribbleExpr? ConstructorExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.NewKeyword))
                return null;
            index++;

            var typeName = ParseTypeName(index, ref temp) as ScribbleTypeName;
            if (typeName == null)
                typeName = ScribbleTypeName.Create("DynamicBag");
            else
                index += temp;

            var initialiser = InitialiserExpr(index, ref temp) as ScribbleExprInitialiser;
            if(initialiser != null)
            {
                // Initialiser with default constructor
                index += temp;

                consumed = index - start;
                return ScribbleExprConstructor.Create(typeName, new List<ScribbleExprArgument>(), initialiser);
            }            

            if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
            {
                PushError("Expected constructor opening parenthesis", index);
                return null;
            }
            index++;

            var arguments = ArgumentList(index, ref temp);
            if (arguments == null)
            {
                PushError("Expected constructor arguments", index);
                return null;
            }
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
            {
                PushError("Expected constructor closing parenthesis", index);
                return null;
            }
            index++;

            // We could have an initialiser after the constructor
            initialiser = InitialiserExpr(index, ref temp) as ScribbleExprInitialiser;
            if (initialiser != null)
                index += temp;

            consumed = index - start;
            return ScribbleExprConstructor.Create(typeName, arguments, initialiser);
        }

        ScribbleNode? InitialiserExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.OpenBraceToken))
                return null;
            index++;

            var initialisers = new List<ScribbleInitialiser>();
            while(!Lexer.Is(index, ScribbleTokenType.CloseBraceToken))
            {
                var identifier = IdentifierExpr(index, ref temp) as ScribbleExprIdentifier;
                if(identifier == null)
                {
                    PushError("Expected initialiser property identifier", index);
                    return null;
                }
                index += temp;

                if(!Lexer.Is(index, ScribbleTokenType.EqualsToken))
                {
                    PushError("Expected initialiser assignment", index);
                    return null;
                }
                index++;

                var expr = Expr(index, ref temp);
                if(expr == null)
                {
                    PushError("Expected initialiser expression", index);
                    return null;
                }
                index += temp;

                if (Lexer.Is(index, ScribbleTokenType.CommaToken))
                    index++;

                initialisers.Add(ScribbleInitialiser.Create(identifier, expr));
            }
            index++;

            consumed = index - start;
            return ScribbleExprInitialiser.Create(initialisers);
        }

        ScribbleExpr? LambdaExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
                return null;
            index++;

            var parameters = IdentifierList(index, ref temp);
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
                return null;
            index++;

            if (!Lexer.Is(index, ScribbleTokenType.LambdaToken))
                return null;
            index++;

            // Expression
            var expr = Expr(index, ref temp);
            if (expr == null)
            {
                PushError("Expected lambda expression value", index);
                return null;
            }
            index += temp;

            consumed = index - start;
            return ScribbleExprLambda.Create(parameters, expr);
        }

        ScribbleExpr? ParenthesisedExpr(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if (!Lexer.Is(index, ScribbleTokenType.OpenParenToken))
                return null;
            index++;

            var expr = Expr(index, ref temp);
            if (expr == null)
                return null;
            index += temp;

            if (!Lexer.Is(index, ScribbleTokenType.CloseParenToken))
            {
                PushError("Expected closing parenthesis", index);
                return null;
            }
            index++;

            consumed = index - start;
            return expr;
        }

        ScribbleExpr? IdentifierExpr(int index, ref int consumed)
        {
            if (!Lexer.Is(index, ScribbleTokenType.IdentifierToken))
                return null;

            consumed = 1;
            return ScribbleExprIdentifier.Create(Lexer.Extract(index));
        }

        ScribbleExpr? LiteralExpr(int index, ref int consumed)
        {
            if (Lexer.Is(index, ScribbleTokenType.StringLiteral))
            {
                var span = Lexer.Extract(index);
                var value = StringUtility.UnescapeString(span.Slice(1, span.Length - 2).ToString());
                consumed = 1;
                return ScribbleExprLiteralString.Create(value);
            }
            else if (Lexer.Is(index, ScribbleTokenType.CharLiteral))
            {
                var span = Lexer.Extract(index);
                var value = StringUtility.UnescapeString(span.Slice(1, span.Length - 2).ToString());
                if (value.Length != 1)
                {
                    PushError("Too many characters in character literal", index);
                    return null;
                }
                consumed = 1;
                return ScribbleExprLiteralChar.Create(value[0]);
            }
            else if (Lexer.Is(index, ScribbleTokenType.BooleanLiteral))
            {
                var span = Lexer.Extract(index);
                if (span.CompareTo(ScribbleTokenType.TrueKeyword, StringComparison.Ordinal) == 0)
                {
                    consumed = 1;
                    return ScribbleExprLiteralBoolean.Create(true);
                }
                if (span.CompareTo(ScribbleTokenType.FalseKeyword, StringComparison.Ordinal) == 0)
                {
                    consumed = 1;
                    return ScribbleExprLiteralBoolean.Create(false);
                }
                return null;
            }
            else if (Lexer.Is(index, ScribbleTokenType.NullLiteral))
            {
                consumed = 1;
                return ScribbleExprLiteralNull.Create();
            }
            else if (Lexer.Is(index, ScribbleTokenType.Int32Literal))
            {
                var span = Lexer.Extract(index);
                var value = Int32.Parse(span.ToString());
                consumed = 1;
                return ScribbleExprLiteralInt32.Create(value);
            }
            else if (Lexer.Is(index, ScribbleTokenType.Int64Literal))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'l' || span[span.Length - 1] == 'L')
                    span = span.Slice(0, span.Length - 1);
                var value = Int64.Parse(span.ToString());
                consumed = 1;
                return ScribbleExprLiteralInt64.Create(value);
            }
            else if (Lexer.Is(index, ScribbleTokenType.DoubleLiteral))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'd' || span[span.Length - 1] == 'D')
                    span = span.Slice(0, span.Length - 1);
                var value = Double.Parse(span.ToString());
                consumed = 1;
                return ScribbleExprLiteralDouble.Create(value);
            }
            else if (Lexer.Is(index, ScribbleTokenType.SingleLiteral))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'f' || span[span.Length - 1] == 'F')
                    span = span.Slice(0, span.Length - 1);
                var value = float.Parse(span.ToString());
                consumed = 1;
                return ScribbleExprLiteralSingle.Create(value);
            }
            else if (Lexer.Is(index, ScribbleTokenType.DecimalLiteral))
            {
                var span = Lexer.Extract(index);
                if (span[span.Length - 1] == 'm' || span[span.Length - 1] == 'M')
                    span = span.Slice(0, span.Length - 1);
                var value = Decimal.Parse(span.ToString());
                consumed = 1;
                return ScribbleExprLiteralDecimal.Create(value);
            }
            return null;
        }
    }
}
