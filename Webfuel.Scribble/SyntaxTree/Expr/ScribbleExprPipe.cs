using System;
using System.Collections.Generic;
using System.Linq;

namespace Webfuel.Scribble
{
    public class ScribbleExprPipe : ScribbleExpr
    {
        private ScribbleExprPipe(ScribbleExpr lhs, ScribbleExprIdentifier identifier, IReadOnlyList<ScribbleExprArgument> arguments)
        {
            LHS = lhs;
            Identifier = identifier;
            Arguments = arguments;
        }

        public ScribbleExpr LHS { get; private set; }

        public ScribbleExprIdentifier Identifier { get; private set; }

        public IReadOnlyList<ScribbleExprArgument> Arguments { get; private set; }

        public static ScribbleExprPipe Create(ScribbleExpr lhs, ScribbleExprIdentifier identifier, IReadOnlyList<ScribbleExprArgument> arguments)
        {
            return new ScribbleExprPipe(lhs, identifier, arguments);
        }

        public override string ToString()
        {
            return $"({LHS} | {Identifier}{(Arguments.Count > 0 ? ": " : "")}{String.Join(", ", Arguments)})";
        }

        ScribbleArgInfo[] ArgumentInfos { get; set; } = new ScribbleArgInfo[0];

        ScribbleMethodBinding? MethodBinding { get; set; }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            ArgumentInfos = new ScribbleArgInfo[Arguments.Count];
            for (var i = 0; i < Arguments.Count; i++)
                ArgumentInfos[i] = new ScribbleArgInfo { ArgumentType = Arguments[i].Expr.Bind(context), Name = Arguments[i].Identifier?.Name };

            MethodBinding = ScribbleMethodBinder.GetMethodBinding(new ScribbleMethodBindingContext(
                LHS.Bind(context), 
                Identifier.Name, 
                ArgumentInfos, 
                null,
                null));
            return MethodBinding?.ReturnType ?? Reflection.UnknownType;
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            yield return LHS;
            foreach (var argument in Arguments)
                yield return argument.Expr;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            return MethodBinding!.Invoke(prerequisites[0]!, prerequisites.Skip(1).ToArray());
        }
    }
}
