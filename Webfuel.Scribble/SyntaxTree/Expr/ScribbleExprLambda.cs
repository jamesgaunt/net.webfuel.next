using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Scribble
{
    public class ScribbleExprLambda : ScribbleExpr
    {
        private ScribbleExprLambda(IReadOnlyList<ScribbleExprIdentifier> parameters, ScribbleExpr expr)
        {
            Parameters = parameters;
            Expr = expr;
        }

        public IReadOnlyList<ScribbleExprIdentifier> Parameters { get; private set; }

        public ScribbleExpr Expr { get; private set; }

        public static ScribbleExprLambda Create(IReadOnlyList<ScribbleExprIdentifier> parameters, ScribbleExpr expr)
        {
            return new ScribbleExprLambda(parameters, expr);
        }

        public override string ToString()
        {
            var sb = new StringBuilder("(");
            sb.Append(String.Join(", ", Parameters));
            sb.Append(") => ");
            sb.Append(Expr.ToString());
            return sb.ToString();
        }

        // This method simply indicates the number of parameters to the lamda, which is all we use to constrain the method binding on
        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            switch (Parameters.Count)
            {
                case 0: return typeof(ScribbleFunc<>);
                case 1: return typeof(ScribbleFunc<,>);
                case 2: return typeof(ScribbleFunc<,,>);
                case 3: return typeof(ScribbleFunc<,,,>);
                case 4: return typeof(ScribbleFunc<,,,,>);
            }
            return Reflection.UnknownType;
        }

        internal Type[] ParameterTypes { get; set; } = new Type[0];

        internal Type ReturnType { get; set; } = Reflection.UnknownType;

        // This method is called after we have bound to a method and we know the type of the ScribbleFunc we are binding to

        internal Type LateBind<T>(ScribbleBindingContext<T> context, Type funcType)
        {
            var genericArguments = funcType.GetGenericArguments();
            
            ParameterTypes = genericArguments.Take(genericArguments.Length - 1).ToArray();

            ReturnType = EvaluateReturnType(context, ParameterTypes);

            if (ReturnType != genericArguments.Last())
                throw new InvalidOperationException($"Lamda returns '{ReturnType.Name}' but expected '{genericArguments.Last().Name}'");

            return ReturnType;
        }

        internal Type EvaluateReturnType<T>(ScribbleBindingContext<T> context, Type[] parameterTypes)
        {
            if (parameterTypes.Length != Parameters.Count)
                return Reflection.UnknownType;

            context.Scope.PushScope();
            {
                for (var i = 0; i < Parameters.Count; i++)
                {
                    context.Scope.DeclareVar(Parameters[i].Name, parameterTypes[i], null);
                }

                ReturnType = Expr.Bind(context);
            }
            context.Scope.PopScope();

            return ReturnType;
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            yield break;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            throw new NotImplementedException();
        }
    }
}
