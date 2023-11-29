using System;
using System.Collections.Generic;
using System.Linq;

namespace Webfuel.Scribble
{
    public class ScribbleExprConstructor : ScribbleExpr
    {
        private ScribbleExprConstructor(ScribbleTypeName typeName, IReadOnlyList<ScribbleExprArgument> arguments, ScribbleExprInitialiser? initialiser)
        {
            TypeName = typeName;
            Arguments = arguments;
            Initialiser = initialiser;
        }

        public ScribbleTypeName TypeName { get; private set; }

        public IReadOnlyList<ScribbleExprArgument> Arguments { get; private set; }

        public ScribbleExprInitialiser? Initialiser { get; private set; }

        public static ScribbleExprConstructor Create(ScribbleTypeName typeName, IReadOnlyList<ScribbleExprArgument> arguments, ScribbleExprInitialiser? initialiser)
        {
            return new ScribbleExprConstructor(typeName, arguments, initialiser);
        }

        public override string ToString()
        {
            return $"new {TypeName}({String.Join(", ", Arguments)})";
        }

        internal ScribbleArgInfo[] ArgumentInfos { get; set; } = new ScribbleArgInfo[0];

        internal ScribbleConstructorBinding? ConstructorBinding { get; set; }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            ArgumentInfos = new ScribbleArgInfo[Arguments.Count];
            for (var i = 0; i < Arguments.Count; i++)
                ArgumentInfos[i] = new ScribbleArgInfo { ArgumentType = Arguments[i].Expr.Bind(context), Name = Arguments[i].Identifier?.Name };

            ConstructorBinding = ScribbleConstructorBinder.GetConstructorBinding(TypeName, ArgumentInfos);

            var valueType = ConstructorBinding?.ReturnType ?? Reflection.UnknownType;
            if (valueType == Reflection.UnknownType || Initialiser == null)
                return valueType;

            // Need to check initialisers make sense

            foreach(var initialiser in Initialiser.Initialisers)
            {
                var lhs = ScribblePropertyBinder.PropertyType(valueType, initialiser.Identifier.Name, set: true, get: false);
                var rhs = initialiser.Expr.Bind(context);


                if (!Reflection.CanCast(from: rhs, to: lhs))
                    throw new InvalidOperationException($"Cannot assign value of type '{rhs.Name}' to '{lhs}' when initialising '{initialiser.Identifier.Name}'");
            }

            return valueType;
        }
 
        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            foreach (var argument in Arguments)
                yield return argument.Expr;

            if (Initialiser != null)
            {
                foreach (var initialiser in Initialiser.Initialisers)
                    yield return initialiser.Expr;
            }
                
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            var value = ConstructorBinding!.Invoke(prerequisites.Take(Arguments.Count).ToArray());

            // If we have an initialiser, call all property assignments
            if(Initialiser != null)
            {
                var index = 0;
                foreach(var initialiser in Initialiser.Initialisers)
                {
                    ScribblePropertyBinder.SetProperty(value, initialiser.Identifier.Name, prerequisites[Arguments.Count + index]);
                    index++;
                }
            }

            return value;
        }
    }
}
