using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleExpression<T>
    {
        private readonly ScribbleExpr Expr;

        internal ScribbleExpression(ScribbleExpr expr)
        {
            Expr = expr;
        }

        public async Task<object?> EvaluateAsync(T env)
        {
            return await new ScribbleExecutionContext<T>(env).EvaluateExpressionAsync(Expr);
        }
    }
}
