using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    class ScribbleBindingContext<T> 
    {
        internal ScribbleBindingContext()
        {
        }

        public ScribbleScopeStack Scope { get; } = new ScribbleScopeStack();

        public Type IdentifierType(string name, bool set, bool get)
        {
            var var = Scope.GetVar(name);
            if (var != null)
                return var.Type;
            return ScribblePropertyBinder.PropertyType(typeof(T), name, set: set, get: get);
        }

        public List<ScribbleExprInvoke> AsyncValidatorInvokes { get; } = new List<ScribbleExprInvoke>();
    }
}
