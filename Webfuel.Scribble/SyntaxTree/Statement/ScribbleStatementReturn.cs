using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementReturn: ScribbleStatement
    {
        private ScribbleStatementReturn(ScribbleExpr? expr)
        {
            Expr = expr;
        }

        public ScribbleExpr? Expr { get; }

        public static ScribbleStatementReturn Create(ScribbleExpr? expr)
        {
            return new ScribbleStatementReturn(expr);
        }

        public override string ToString()
        {
            if (Expr == null)
                return "return;";
            return $"return {Expr};";
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            if (Expr != null && Expr.Bind(context) == Reflection.UnknownType)
                throw new InvalidOperationException("Unable to resove type of expression: " + Expr);
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            if (Expr == null)
                context.Return();
            else
                context.Return(await context.EvaluateExpressionAsync(Expr));
        }
    }
}
