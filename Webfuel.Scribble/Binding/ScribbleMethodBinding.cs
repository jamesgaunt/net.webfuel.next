using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    class ScribbleMethodBinding
    {
        ScribbleMethodBinding(ScribbleMethodInfo methodInfo, List<int>? argumentMap)
        {
            MethodInfo = methodInfo;
            ArgumentMap = argumentMap;
            ReturnType = MethodInfo.ReturnType;

            // Unwrap Task returned from Async methods
            if (ReturnType.IsGenericType && ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                ReturnType = ReturnType.GetGenericArguments()[0];
        }

        public ScribbleMethodInfo MethodInfo { get; }

        // ArgumentMap is either null, or maps each parameter index to an argument index
        public IReadOnlyList<int>? ArgumentMap { get; } = null;

        public Type ReturnType { get; }

        public static ScribbleMethodBinding Create(ScribbleMethodInfo methodInfo, List<int>? argumentMap)
        {
            return new ScribbleMethodBinding(methodInfo, argumentMap);
        }

        public Type GetArgumentType(int argumentIndex)
        {
            if(ArgumentMap == null)
            {
                if (MethodInfo.ExtensionMethod)
                    argumentIndex++;
                return MethodInfo.Parameters[argumentIndex].ParameterType;
            }

            for(var pi = 0; pi < MethodInfo.Parameters.Count; pi++)
            {
                if (ArgumentMap[pi] == argumentIndex)
                    return MethodInfo.Parameters[pi].ParameterType;
            }

            throw new InvalidOperationException("Unable to deduce argument type");
        }

        public object? Invoke(object obj, object?[] values)
        {
            if (MethodInfo.ExtensionMethod)
                return InvokeExtensionMethod(obj, values);

            if(ArgumentMap == null)
                return MethodInfo.Invoke(obj, values);
            
            var parameters = new object?[MethodInfo.Parameters.Count];
            for(var pi = 0; pi < parameters.Length; pi++)
            {
                var ai = ArgumentMap[pi];
                if (ai == -1)
                    parameters[pi] = MethodInfo.Parameters[pi].DefaultValue;
                else
                    parameters[pi] = values[ai];
            }
            return MethodInfo.Invoke(obj, parameters);
        }

        public object? InvokeExtensionMethod(object obj, object?[] values)
        {
            // Jigger the parameters to add obj as the first parameter, otherwise invoke is identical

            if (ArgumentMap == null)
            {
                var parameters = new object?[values.Length + 1];
                parameters[0] = obj;

                for (var vi = 0; vi < values.Length; vi++)
                    parameters[vi + 1] = values[vi];

                return MethodInfo.Invoke(obj, parameters);
            }
            else
            {
                var parameters = new object?[MethodInfo.Parameters.Count + 1];
                parameters[0] = obj;

                for (var pi = 0; pi < parameters.Length; pi++)
                {
                    var ai = ArgumentMap[pi];
                    if (ai == -1)
                        parameters[pi + 1] = MethodInfo.Parameters[pi].DefaultValue;
                    else
                        parameters[pi + 1] = values[ai];
                }
                return MethodInfo.Invoke(obj, parameters);
            }
        }
    }
}
