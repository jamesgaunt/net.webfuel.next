using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public enum ScribblePostfixOp
    {
        Unknown = 0,

        PostIncrement,
        PostDecrement,
    }

    public class ScribbleExprPostfixOp : ScribbleExpr
    {
        private ScribbleExprPostfixOp(ScribbleExpr lhs, ScribblePostfixOp op)
        {
            LHS = lhs;
            Op = op;
        }

        public ScribbleExpr LHS { get; private set; }

        public ScribblePostfixOp Op { get; private set; }
        
        public static ScribbleExprPostfixOp Create(ScribbleExpr lhs, ScribblePostfixOp op)
        {
            return new ScribbleExprPostfixOp(lhs, op);
        }

        public override string ToString()
        {
            return $"({LHS}{FormatOp(Op)})";
        }

        public static string FormatOp(ScribblePostfixOp op)
        {
            return op switch
            {
                ScribblePostfixOp.PostIncrement => ScribbleTokenType.PlusPlusToken,
                ScribblePostfixOp.PostDecrement => ScribbleTokenType.MinusMinusToken,
                _ => "???"
            };
        }

        public static ScribblePostfixOp ParseOp(string op)
        {
            return op switch
            {
                ScribbleTokenType.PlusPlusToken => ScribblePostfixOp.PostIncrement,
                ScribbleTokenType.MinusMinusToken => ScribblePostfixOp.PostDecrement,
                _ => ScribblePostfixOp.Unknown
            };
        }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            return ScribblePostfixOpBinder.ResolvePostfixOp(LHS.Bind(context), Op);
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            yield return LHS;

            // If LHS is a member evaluate its LHS to get the target of the call
            if (LHS is ScribbleExprMember)
                yield return ((ScribbleExprMember)LHS).LHS;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            var originalValue = prerequisites[0]; // Value to increment/decrement
            var adjustedValue = ScribbleBinaryOpBinder.EvaluateBinaryOp(originalValue, Op == ScribblePostfixOp.PostIncrement ? ScribbleBinaryOp.Add : ScribbleBinaryOp.Subtract, 1);

            // Assign & Return
            if (LHS is ScribbleExprMember)
            {
                var name = ((ScribbleExprMember)LHS).Identifier.Name;

                var target = prerequisites[1];
                if (target == null)
                    throw new NullReferenceException();

                ScribblePropertyBinder.SetProperty(target, name, adjustedValue);
                return originalValue; // Postfix operator so evaluates to original value
            }

            if (LHS is ScribbleExprIdentifier)
            {
                var name = ((ScribbleExprIdentifier)LHS).Name;

                context.SetProperty(name, adjustedValue);
                return originalValue; // Postfix operator so evaluates to original value
            }

            throw new InvalidOperationException("Postfix Op can only be applied to members or identifiers");
        }
    }
}
