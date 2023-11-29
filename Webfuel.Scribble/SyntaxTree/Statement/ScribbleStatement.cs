using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public abstract class ScribbleStatement: ScribbleNode
    {
        internal abstract void Bind<T>(ScribbleBindingContext<T> context);

        internal abstract Task ExecuteAsync<T>(ScribbleExecutionContext<T> context);
    }
}
