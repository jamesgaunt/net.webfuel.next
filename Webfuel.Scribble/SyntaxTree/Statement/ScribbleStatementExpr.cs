using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementExpr: ScribbleStatement
    {
        private ScribbleStatementExpr(ScribbleExpr expr)
        {
            Expr = expr;
        }

        public ScribbleExpr Expr { get; }

        public static ScribbleStatementExpr Create(ScribbleExpr expr)
        {
            return new ScribbleStatementExpr(expr);
        }

        public override string ToString()
        {
            return Expr.ToString() + ";";
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            if(Expr.Bind(context) == Reflection.UnknownType)
                throw new InvalidOperationException("Unable to resolve type of expression: " + Expr);
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            await context.EvaluateExpressionAsync(Expr);
        }
    }
}
