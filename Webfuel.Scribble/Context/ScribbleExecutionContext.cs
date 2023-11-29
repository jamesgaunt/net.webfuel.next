using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public enum ScribbleExecutionState
    {
        Executing = 1,
        ReturnWithValue = 2,
        ReturnWithoutValue = 3
    }

    interface IScribbleExecutionContext
    {
        ScribbleScopeStack Scope { get; }

        ValueTask<object?> EvaluateExpressionAsync(ScribbleExpr input);
    }

    class ScribbleExecutionContext<T>: IScribbleExecutionContext
    {
        internal ScribbleExecutionContext(T env)
        {
            Env = env;
        }

        T Env { get; }

        public ScribbleScopeStack Scope { get; } = new ScribbleScopeStack();

        public async ValueTask<object?> EvaluateExpressionAsync(ScribbleExpr input)
        {
            var prerequisites = await GetPrerequisitesAsync(input);

            var value = input.Evaluate(this, prerequisites);

            // If value is a Task then await it
            if (value is Task)
            {
                var task = (Task)value;
                await task;
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                    value = resultProperty.GetValue(task);
            }

            return value;
        }

        async ValueTask<List<object?>> GetPrerequisitesAsync(ScribbleExpr input)
        {
            // Evaluate prerequisites
            var prerequisites = new List<object?>();

            // Runtime hacks for short circuiting conditional expression evaluation - these need to be mirrored in the syntax nodes
            {
                if (input is ScribbleExprConditional)
                {
                    var parts = input.GetPrerequisites(this).ToList();
                    var condition = await EvaluateExpressionAsync(parts[0]);
                    if ((Boolean)condition!)
                        prerequisites.Add(await EvaluateExpressionAsync(parts[1]));
                    else
                        prerequisites.Add(await EvaluateExpressionAsync(parts[2]));

                    return prerequisites;
                }

                if (input is ScribbleExprBinaryOp)
                {
                    var binaryOp = (ScribbleExprBinaryOp)input;

                    if (binaryOp.Op == ScribbleBinaryOp.LogicalOr)
                    {
                        var parts = input.GetPrerequisites(this).ToList();
                        var first = await EvaluateExpressionAsync(parts[0]);

                        if ((Boolean)first!)
                        {
                            // TRUE || ??? = TRUE
                            prerequisites.Add(true);
                        }
                        else
                        {
                            // FALSE || ??? = ???
                            prerequisites.Add(await EvaluateExpressionAsync(parts[1]));
                        }
                        return prerequisites;
                    }
                    else if (binaryOp.Op == ScribbleBinaryOp.LogicalAnd)
                    {
                        var parts = input.GetPrerequisites(this).ToList();
                        var first = await EvaluateExpressionAsync(parts[0]);

                        if (!(Boolean)first!)
                        {
                            // FALSE && ??? = FALSE
                            prerequisites.Add(false);
                        }
                        else
                        {
                            // TRUE && ??? = ???
                            prerequisites.Add(await EvaluateExpressionAsync(parts[1]));
                        }
                        return prerequisites;
                    }
                }
            }

            // For non short-circuiting expressions just evaluate and return all the prerequisites

            foreach (var prerequisite in input.GetPrerequisites(this))
            {
                if (prerequisite is ScribbleExprLambda)
                    prerequisites.Add(GenerateScribbleFunc((ScribbleExprLambda)prerequisite));
                else
                    prerequisites.Add(await EvaluateExpressionAsync(prerequisite));
            }

            return prerequisites;
        }

        public ScribbleExecutionState ExecutionState { get; private set; } = ScribbleExecutionState.Executing;

        internal object? ReturnValue { get; private set; }

        internal void Return()
        {
            ReturnValue = null;
            ExecutionState = ScribbleExecutionState.ReturnWithoutValue;
        }

        internal void Return(object? value)
        {
            ReturnValue = value;
            ExecutionState = ScribbleExecutionState.ReturnWithValue;
        }

        public Type IdentifierType(string name, bool set, bool get)
        {
            var var = Scope.GetVar(name);
            if (var != null)
                return var.Type;
            return ScribblePropertyBinder.PropertyType(typeof(T), name, set: set, get: get);
        }

        public object? GetProperty(string name)
        {
            var var = Scope.GetVar(name);
            if (var != null)
            {
                return var.Value;
            }

            return ScribblePropertyBinder.GetProperty(Env, name);
        }

        public object? SetProperty(string name, object? value)
        {
            var var = Scope.GetVar(name);
            if (var != null)
            {
                var.Value = value;
                return var.Value;
            }

            return ScribblePropertyBinder.SetProperty(Env, name, value);
        }

        public object? InvokeMethod(ScribbleMethodBinding binding, object?[] values)
        {
            return binding.Invoke(Env!, values);
        }

        public StringWriter Writer { get; } = new StringWriter();

        // Create Runtime ScribbleFunc

        object? GenerateScribbleFunc(ScribbleExprLambda lambda)
        {
            var funcType = GenerateScribbleFuncType(lambda.Parameters.Count);
            funcType = funcType.MakeGenericType(GetTypeArguments(lambda));
            var constructor = funcType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(IScribbleExecutionContext), typeof(ScribbleExprLambda) }, null)!;
            var func = constructor.Invoke(new object?[] { this, lambda });
            return func;
        }

        Type GenerateScribbleFuncType(int parameterCount)
        {
            switch (parameterCount)
            {
                case 0: return typeof(ScribbleFunc<>);
                case 1: return typeof(ScribbleFunc<,>);
                case 2: return typeof(ScribbleFunc<,,>);
                case 3: return typeof(ScribbleFunc<,,,>);
                case 4: return typeof(ScribbleFunc<,,,,>);
            }
            throw new InvalidOperationException("Too many lambda parameters");
        }

        Type[] GetTypeArguments(ScribbleExprLambda lambda)
        {
            var typeArguments = new Type[lambda.Parameters.Count + 1];
            lambda.ParameterTypes.CopyTo(typeArguments, 0);
            typeArguments[lambda.Parameters.Count] = lambda.ReturnType;
            return typeArguments;
        }
       
    }
}
