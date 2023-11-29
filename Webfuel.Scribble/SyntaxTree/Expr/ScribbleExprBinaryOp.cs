using System;
using System.Linq;
using System.Collections.Generic;

namespace Webfuel.Scribble
{
    public enum ScribbleBinaryOp
    {
        Unknown = 0,

        Add,
        Subtract,
        Multiply,
        Divide,
        Mod,

        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        Equal,
        NotEqual,

        LogicalOr,
        LogicalAnd,

        Coalesce,
        StringCoalesce,

        Assignment,
        AdditionAssignment,
        SubtractionAssignment,
    }

    public class ScribbleExprBinaryOp : ScribbleExpr
    {
        private ScribbleExprBinaryOp(ScribbleExpr lhs, ScribbleBinaryOp op, ScribbleExpr rhs)
        {
            LHS = lhs;
            Op = op;
            RHS = rhs;
        }

        public ScribbleExpr LHS { get; private set; }

        public ScribbleBinaryOp Op { get; private set; }

        public ScribbleExpr RHS { get; private set; }

        public static ScribbleExprBinaryOp Create(ScribbleExpr lhs, ScribbleBinaryOp op, ScribbleExpr rhs)
        {
            return new ScribbleExprBinaryOp(lhs, op, rhs);
        }

        public override string ToString()
        {
            return $"({LHS} {FormatOp(Op)} {RHS})";
        }

        public static string FormatOp(ScribbleBinaryOp op)
        {
            return op switch
            {
                ScribbleBinaryOp.Add => ScribbleTokenType.PlusToken,
                ScribbleBinaryOp.Subtract => ScribbleTokenType.MinusToken,
                ScribbleBinaryOp.Multiply => ScribbleTokenType.AsteriskToken,
                ScribbleBinaryOp.Divide => ScribbleTokenType.SlashToken,
                ScribbleBinaryOp.Mod => ScribbleTokenType.PercentToken,
                ScribbleBinaryOp.LessThan => ScribbleTokenType.LessThanToken,
                ScribbleBinaryOp.GreaterThan => ScribbleTokenType.GreaterThanToken,
                ScribbleBinaryOp.LessThanOrEqual => ScribbleTokenType.LessThanEqualsToken,
                ScribbleBinaryOp.GreaterThanOrEqual => ScribbleTokenType.GreaterThanEqualsToken,
                ScribbleBinaryOp.LogicalOr => ScribbleTokenType.BarBarToken,
                ScribbleBinaryOp.LogicalAnd => ScribbleTokenType.AmpersandAmpersandToken,
                ScribbleBinaryOp.Equal => ScribbleTokenType.EqualsEqualsToken,
                ScribbleBinaryOp.NotEqual => ScribbleTokenType.ExclamationEqualsToken,
                ScribbleBinaryOp.Coalesce => ScribbleTokenType.QuestionQuestionToken,
                ScribbleBinaryOp.StringCoalesce => ScribbleTokenType.DollarQuestionToken,

                ScribbleBinaryOp.Assignment => ScribbleTokenType.EqualsToken,
                ScribbleBinaryOp.AdditionAssignment => ScribbleTokenType.PlusEqualsToken,
                ScribbleBinaryOp.SubtractionAssignment => ScribbleTokenType.MinusEqualsToken,

                _ => "???"
            };
        }

        public static ScribbleBinaryOp ParseOp(string op)
        {
            return op switch
            {
                ScribbleTokenType.PlusToken => ScribbleBinaryOp.Add,
                ScribbleTokenType.MinusToken => ScribbleBinaryOp.Subtract,
                ScribbleTokenType.AsteriskToken => ScribbleBinaryOp.Multiply,
                ScribbleTokenType.SlashToken => ScribbleBinaryOp.Divide,
                ScribbleTokenType.PercentToken => ScribbleBinaryOp.Mod,
                ScribbleTokenType.LessThanToken => ScribbleBinaryOp.LessThan,
                ScribbleTokenType.GreaterThanToken => ScribbleBinaryOp.GreaterThan,
                ScribbleTokenType.LessThanEqualsToken => ScribbleBinaryOp.LessThanOrEqual,
                ScribbleTokenType.GreaterThanEqualsToken => ScribbleBinaryOp.GreaterThanOrEqual,
                ScribbleTokenType.BarBarToken => ScribbleBinaryOp.LogicalOr,
                ScribbleTokenType.AmpersandAmpersandToken => ScribbleBinaryOp.LogicalAnd,
                ScribbleTokenType.EqualsEqualsToken => ScribbleBinaryOp.Equal,
                ScribbleTokenType.ExclamationEqualsToken => ScribbleBinaryOp.NotEqual,
                ScribbleTokenType.QuestionQuestionToken => ScribbleBinaryOp.Coalesce,
                ScribbleTokenType.DollarQuestionToken => ScribbleBinaryOp.StringCoalesce,

                ScribbleTokenType.EqualsToken => ScribbleBinaryOp.Assignment,
                ScribbleTokenType.PlusEqualsToken => ScribbleBinaryOp.AdditionAssignment,
                ScribbleTokenType.MinusEqualsToken => ScribbleBinaryOp.SubtractionAssignment,
                _ => ScribbleBinaryOp.Unknown
            };
        }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            if ((int)Op < (int)ScribbleBinaryOp.Assignment)
            {
                return ScribbleBinaryOpBinder.ResolveBinaryOp(LHS.Bind(context), Op, RHS.Bind(context));
            }
            else
            {
                if (LHS is ScribbleExprMember)
                {
                    var member = (ScribbleExprMember)LHS;
                    var name = member.Identifier.Name;
                    var targetType = member.LHS.Bind(context);

                    var lhs = ScribblePropertyBinder.PropertyType(targetType, name, set: true, get: false);
                    var rhs = RHS.Bind(context);

                    if (!Reflection.CanCast(from: rhs, to: lhs))
                        throw new InvalidOperationException($"Cannot assign value of type '{rhs.Name}' to '{LHS}'");

                    return lhs;
                }

                if (LHS is ScribbleExprIdentifier)
                {
                    var identifier = (ScribbleExprIdentifier)LHS;
                    var name = identifier.Name;

                    var lhs = context.IdentifierType(name, set: true, get: false);
                    var rhs = RHS.Bind(context);

                    if (!Reflection.CanCast(from: rhs, to: lhs))
                        throw new InvalidOperationException($"Cannot assign value of type '{rhs.Name}' to '{LHS}'");

                    return lhs;
                }

                if (LHS is ScribbleExprIndex)
                {
                    var index = (ScribbleExprIndex)LHS;

                    var lhs = index.Bind(context);
                    var rhs = RHS.Bind(context);

                    if (index.IndexBinding?.CanSet != true)
                        throw new InvalidOperationException($"Cannot assign to readonly index");

                    if (!Reflection.CanCast(from: rhs, to: lhs))
                        throw new InvalidOperationException($"Cannot assign value of type '{rhs.Name}' to '{LHS}'");

                    return lhs;
                }

                throw new InvalidOperationException($"Expression '{LHS}' cannot be assigned to");
            }
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            if ((int)Op < (int)ScribbleBinaryOp.Assignment)
            {
                // Simple Op
                yield return LHS;
                yield return RHS;
            }
            else
            {
                // Assignment Op
                yield return RHS;

                // If this is a compound assignment push the current LHS value
                if (Op != ScribbleBinaryOp.Assignment)
                    yield return LHS;

                // If LHS is a member or index evaluate its LHS as the target
                if (LHS is ScribbleExprMember)
                    yield return ((ScribbleExprMember)LHS).LHS;
                if (LHS is ScribbleExprIndex)
                    yield return ((ScribbleExprIndex)LHS).LHS;

                // If LHS is an index evaluate its arguments (note could result in duplicate evaluation of these arguments)
                if (LHS is ScribbleExprIndex)
                {
                    var index = (ScribbleExprIndex)LHS;
                    foreach (var argument in index.Arguments)
                        yield return argument.Expr;
                }
            }
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            if (Op == ScribbleBinaryOp.LogicalAnd || Op == ScribbleBinaryOp.LogicalOr)
            {
                // Note there is an edge case hack in the runtime to enable short circuiting of the conditional expressions
                return prerequisites[0];
            }

            if ((int)Op < (int)ScribbleBinaryOp.Assignment)
            {
                // Simple Op
                return ScribbleBinaryOpBinder.EvaluateBinaryOp(prerequisites[0], Op, prerequisites[1]);
            }

            var targetIndex = 1;

            // Assignment Op

            var value = prerequisites[0]; // The value to assign

            // Adjust assignment value based on current value if this is a compound assignment
            if (Op == ScribbleBinaryOp.AdditionAssignment)
            {
                value = ScribbleBinaryOpBinder.EvaluateBinaryOp(prerequisites[1], ScribbleBinaryOp.Add, value);
                targetIndex++;
            }
            else if (Op == ScribbleBinaryOp.SubtractionAssignment)
            {
                value = ScribbleBinaryOpBinder.EvaluateBinaryOp(prerequisites[1], ScribbleBinaryOp.Subtract, value);
                targetIndex++;
            }

            // Ready to assign
            if (LHS is ScribbleExprMember)
            {
                var name = ((ScribbleExprMember)LHS).Identifier.Name;

                var target = prerequisites[targetIndex];
                if (target == null)
                    throw new NullReferenceException();

                return ScribblePropertyBinder.SetProperty(target, name, value);
            }

            if (LHS is ScribbleExprIdentifier)
            {
                var name = ((ScribbleExprIdentifier)LHS).Name;

                return context.SetProperty(name, value);
            }

            if (LHS is ScribbleExprIndex)
            {
                var index = (ScribbleExprIndex)LHS;

                var target = prerequisites[targetIndex];
                if (target == null)
                    throw new NullReferenceException();

                index.IndexBinding!.Set(target, value, prerequisites.Skip(targetIndex + 1).ToArray());
                return value;
            }

            throw new InvalidOperationException("Assignment can only be applied to members, identifiers or indexes");
        }
    }
}
