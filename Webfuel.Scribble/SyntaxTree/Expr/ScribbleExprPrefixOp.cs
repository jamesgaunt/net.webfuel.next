using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public enum ScribblePrefixOp
    {
        Unknown = 0,

        Plus,
        Minus,
        Not,

        Await
    }

    public class ScribbleExprPrefixOp : ScribbleExpr
    {
        private ScribbleExprPrefixOp(ScribblePrefixOp op, ScribbleExpr rhs)
        {
            Op = op;
            RHS = rhs;
        }

        public ScribblePrefixOp Op { get; private set; }

        public ScribbleExpr RHS { get; private set; }

        public static ScribbleExprPrefixOp Create(ScribblePrefixOp op, ScribbleExpr rhs)
        {
            return new ScribbleExprPrefixOp(op, rhs);
        }

        public override string ToString()
        {
            return $"({FormatOp(Op)}{RHS})";
        }

        public static string FormatOp(ScribblePrefixOp op)
        {
            return op switch
            {
                ScribblePrefixOp.Plus => ScribbleTokenType.PlusToken,
                ScribblePrefixOp.Minus => ScribbleTokenType.MinusToken,
                ScribblePrefixOp.Not => ScribbleTokenType.ExclamationToken,
                _ => "???"
            };
        }

        public static ScribblePrefixOp ParseOp(string op)
        {
            return op switch
            {
                ScribbleTokenType.PlusToken => ScribblePrefixOp.Plus,
                ScribbleTokenType.MinusToken => ScribblePrefixOp.Minus,
                ScribbleTokenType.ExclamationToken => ScribblePrefixOp.Not,
                _ => ScribblePrefixOp.Unknown
            };
        }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            return ScribblePrefixOpBinder.ResolvePrefixOp(Op, RHS.Bind(context));
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            return new List<ScribbleExpr> { RHS };
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            return ScribblePrefixOpBinder.EvaluatePrefixOp(Op, prerequisites[0]);
        }
    }
}
