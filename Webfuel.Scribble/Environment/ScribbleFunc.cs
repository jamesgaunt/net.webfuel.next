using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleFunc
    {
        internal ScribbleFunc(IScribbleExecutionContext context, ScribbleExprLambda lambda)
        {
            Context = context;
            Lambda = lambda;
        }

        internal IScribbleExecutionContext Context { get; set; }

        internal ScribbleExprLambda Lambda { get; set; }
    }

    public class ScribbleFunc<TR> : ScribbleFunc
    {
        internal ScribbleFunc(IScribbleExecutionContext context, ScribbleExprLambda lambda) : base(context, lambda)
        {
        }
    }

    public class ScribbleFunc<T0, TR>: ScribbleFunc
    {
        internal ScribbleFunc(IScribbleExecutionContext context, ScribbleExprLambda lambda) : base(context, lambda)
        {
        }

        public async Task<TR> InvokeAsync(T0 _0)
        {
            Context.Scope.PushScope();
            Context.Scope.DeclareVar(Lambda.Parameters[0].Name, Lambda.ParameterTypes[0], _0);
            var result = (TR)(await Context.EvaluateExpressionAsync(Lambda.Expr))!;
            Context.Scope.PopScope();        
            return result;
        }
    }

    public class ScribbleFunc<T0, T1, TR> : ScribbleFunc
    {
        internal ScribbleFunc(IScribbleExecutionContext context, ScribbleExprLambda lambda) : base(context, lambda)
        {
        }
    }

    public class ScribbleFunc<T0, T1, T2, TR> : ScribbleFunc
    {
        internal ScribbleFunc(IScribbleExecutionContext context, ScribbleExprLambda lambda) : base(context, lambda)
        {
        }
    }

    public class ScribbleFunc<T0, T1, T2, T3, TR> : ScribbleFunc
    {
        internal ScribbleFunc(IScribbleExecutionContext context, ScribbleExprLambda lambda) : base(context, lambda)
        {
        }
    }
}
