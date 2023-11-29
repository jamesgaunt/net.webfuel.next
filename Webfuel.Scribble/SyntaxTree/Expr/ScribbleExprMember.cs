using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleExprMember : ScribbleExpr
    {
        private ScribbleExprMember(ScribbleExpr lhs, ScribbleExprIdentifier identifier)
        {
            LHS = lhs;
            Identifier = identifier;
        }

        public ScribbleExpr LHS { get; private set; }

        public ScribbleExprIdentifier Identifier { get; private set; }

        public static ScribbleExprMember Create(ScribbleExpr lhs, ReadOnlySpan<char> identifier)
        {
            return new ScribbleExprMember(lhs, ScribbleExprIdentifier.Create(identifier));
        }

        public override string ToString()
        {
            return $"{LHS}.{Identifier}";
        }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            return ScribblePropertyBinder.PropertyType(LHS.Bind(context), Identifier.Name, set: false, get: true);
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            return new List<ScribbleExpr> { LHS };
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            return ScribblePropertyBinder.GetProperty(prerequisites[0], Identifier.Name);
        }
    }
}
