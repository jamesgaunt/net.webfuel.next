using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleExprInvoke : ScribbleExpr
    {
        private ScribbleExprInvoke(ScribbleExpr lhs, IReadOnlyList<ScribbleExprArgument> arguments, IReadOnlyList<ScribbleTypeName>? typeArguments)
        {
            LHS = lhs;
            Arguments = arguments;
            TypeArguments = typeArguments;
        }

        public ScribbleExpr LHS { get; private set; }

        public IReadOnlyList<ScribbleExprArgument> Arguments { get; private set; }

        public IReadOnlyList<ScribbleTypeName>? TypeArguments { get; private set; }

        public static ScribbleExprInvoke Create(ScribbleExpr lhs, IReadOnlyList<ScribbleExprArgument> arguments, IReadOnlyList<ScribbleTypeName>? typeArguments)
        {
            return new ScribbleExprInvoke(lhs, arguments, typeArguments);
        }

        public override string ToString()
        {
            return $"{LHS}({String.Join(", ", Arguments)})";
        }

        internal ScribbleArgInfo[] ArgumentInfos { get; set; } = new ScribbleArgInfo[0];

        internal ScribbleMethodBinding? MethodBinding { get; set; }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            ArgumentInfos = new ScribbleArgInfo[Arguments.Count];
            for (var i = 0; i < Arguments.Count; i++)
                ArgumentInfos[i] = new ScribbleArgInfo { ArgumentType = Arguments[i].Expr.Bind(context), Name = Arguments[i].Identifier?.Name };

            if (LHS is ScribbleExprMember)
            {
                var member = (ScribbleExprMember)LHS;
                MethodBinding = ScribbleMethodBinder.GetMethodBinding(new ScribbleMethodBindingContext(
                    member.LHS.Bind(context),
                    member.Identifier.Name,
                    ArgumentInfos,
                    TypeArguments,
                    lambdaTypeResolver: (parameterTypes) =>
                    {
                        foreach (var argument in Arguments)
                        {
                            var lambda = argument.Expr as ScribbleExprLambda;
                            if (lambda != null)
                            {
                                return lambda.EvaluateReturnType(context, parameterTypes);
                            }
                        }
                        return Reflection.UnknownType;
                    }));
            }

            if (LHS is ScribbleExprIdentifier)
            {
                var identifier = (ScribbleExprIdentifier)LHS;
                MethodBinding = ScribbleMethodBinder.GetMethodBinding(new ScribbleMethodBindingContext(
                    typeof(T),
                    identifier.Name,
                    ArgumentInfos,
                    TypeArguments,
                    lambdaTypeResolver: (parameterTypes) =>
                    {
                        foreach (var argument in Arguments)
                        {
                            var lambda = argument.Expr as ScribbleExprLambda;
                            if (lambda != null)
                            {
                                return lambda.EvaluateReturnType(context, parameterTypes);
                            }
                        }
                        return Reflection.UnknownType;
                    }));
            }

            // Late Bind Lambdas
            for (var i = 0; i < Arguments.Count(); i++)
            {
                if (Arguments[i].Expr is ScribbleExprLambda)
                {
                    var lambda = (ScribbleExprLambda)Arguments[i].Expr;
                    var funcType = MethodBinding!.GetArgumentType(i);
                    lambda.LateBind(context, funcType);
                }
            }

            var returnType = MethodBinding?.ReturnType ?? Reflection.UnknownType;
            if (returnType == Reflection.UnknownType)
                return returnType;

            // If this method binding has a validation attribute, store it to be validated 
            var validationAttribute = MethodBinding!.MethodInfo.GetCustomAttribute<ScribbleAsyncValidatorAttribute>();
            if (validationAttribute != null)
                context.AsyncValidatorInvokes.Add(this);

            return returnType;
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            foreach (var argument in Arguments)
                yield return argument.Expr;

            // If LHS is a member evaluate its LHS to get the target of the call
            if (LHS is ScribbleExprMember)
                yield return ((ScribbleExprMember)LHS).LHS;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            if (LHS is ScribbleExprMember)
            {
                var member = (ScribbleExprMember)LHS;
                return MethodBinding!.Invoke(prerequisites.Last()!, prerequisites.Take(prerequisites.Count - 1).ToArray());
            }

            if (LHS is ScribbleExprIdentifier)
            {
                var identifier = (ScribbleExprIdentifier)LHS;
                return context.InvokeMethod(MethodBinding!, prerequisites.ToArray());
            }

            throw new InvalidOperationException("Methods can only be invoked on members or identifiers");
        }

        internal async Task<string?> ValidateAsync(object? validationContext)
        {
            var validationAttribute = MethodBinding!.MethodInfo.GetCustomAttribute<ScribbleAsyncValidatorAttribute>();
            if (validationAttribute == null)
                return null;

            var validator = Activator.CreateInstance(validationAttribute.ValidatorType) as IScribbleAsyncValidator;
            if (validator == null)
                throw new InvalidOperationException("Invalid async validator type");

            // Extrat literal parameters
            var literals = new Dictionary<string, object?>();
            for (var ai = 0; ai < Arguments.Count; ai++)
            {

                var arg = Arguments[ai];
                var literalExpr = arg.Expr as IScribbleExprLiteral;
                if (literalExpr != null)
                {
                    var parameterName = arg.Identifier == null ? MethodBinding.MethodInfo.Parameters[ai].Name : arg.Identifier.Name;
                    literals[parameterName] = literalExpr.GetValue();
                }
            }

            return await validator.ValidateAsync(MethodBinding!.MethodInfo.MethodInfo, literals, validationContext);
        }
    }
}
