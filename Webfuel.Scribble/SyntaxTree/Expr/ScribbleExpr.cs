using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public abstract class ScribbleExpr: ScribbleNode
    {
        internal abstract Type Bind<T>(ScribbleBindingContext<T> context);

        internal abstract IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context);

        internal abstract object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites);
    }
}
