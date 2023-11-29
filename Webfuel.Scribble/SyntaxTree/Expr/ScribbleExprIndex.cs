using System;
using System.Collections.Generic;
using System.Linq;

namespace Webfuel.Scribble
{
    public class ScribbleExprIndex : ScribbleExpr
    {
        private ScribbleExprIndex(ScribbleExpr lhs, IReadOnlyList<ScribbleExprArgument> arguments)
        {
            LHS = lhs;
            Arguments = arguments;
        }

        public ScribbleExpr LHS { get; private set; }

        public IReadOnlyList<ScribbleExprArgument> Arguments { get; private set; }

        public static ScribbleExprIndex Create(ScribbleExpr lhs, IReadOnlyList<ScribbleExprArgument> arguments)
        {
            return new ScribbleExprIndex(lhs, arguments);
        }

        public override string ToString()
        {
            return $"{LHS}[{String.Join(", ", Arguments)}]";
        }

        ScribbleArgInfo[] ArgumentInfos { get; set; } = new ScribbleArgInfo[0];

        internal ScribbleIndexBinding? IndexBinding { get; set; }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            ArgumentInfos = new ScribbleArgInfo[Arguments.Count];
            for (var i = 0; i < Arguments.Count; i++)
                ArgumentInfos[i] = new ScribbleArgInfo { ArgumentType = Arguments[i].Expr.Bind(context), Name = Arguments[i].Identifier?.Name };

            IndexBinding = ScribbleIndexBinder.GetIndexBinding(LHS.Bind(context), ArgumentInfos);
          
            return IndexBinding?.ReturnType ?? Reflection.UnknownType;
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            yield return LHS;
            foreach (var argument in Arguments)
                yield return argument.Expr;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            return IndexBinding!.Get(prerequisites[0]!, prerequisites.Skip(1).ToArray());
        }
    }
}
