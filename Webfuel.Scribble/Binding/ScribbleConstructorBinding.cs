using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    class ScribbleConstructorBinding
    {
        ScribbleConstructorBinding(ScribbleConstructorInfo methodInfo, int defaultCount, List<int>? argumentMap)
        {
            ConstructorInfo = methodInfo;
            DefaultCount = defaultCount;
            ArgumentMap = argumentMap;
            ReturnType = ConstructorInfo.DeclaringType;

            // Unwrap Task returned from Async methods
            if (ReturnType.IsGenericType && ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                ReturnType = ReturnType.GetGenericArguments()[0];
        }

        public ScribbleConstructorInfo ConstructorInfo { get; }

        public int DefaultCount { get; }

        // ArgumentMap is either null, or maps each parameter index to an argument index
        public IReadOnlyList<int>? ArgumentMap { get; } = null;

        public Type ReturnType { get; }

        public static ScribbleConstructorBinding Create(ScribbleConstructorInfo methodInfo, int defaultCount, List<int>? argumentMap)
        {
            return new ScribbleConstructorBinding(methodInfo, defaultCount, argumentMap);
        }

        public Type GetArgumentType(int argumentIndex)
        {
            if(ArgumentMap == null)
            {
                return ConstructorInfo.Parameters[argumentIndex].ParameterType;
            }

            for(var pi = 0; pi < ConstructorInfo.Parameters.Count; pi++)
            {
                if (ArgumentMap[pi] == argumentIndex)
                    return ConstructorInfo.Parameters[pi].ParameterType;
            }

            throw new InvalidOperationException("Unable to deduce argument type");
        }

        public object? Invoke(object?[] values)
        {
            if(ArgumentMap == null)
                return ConstructorInfo.Invoke(values);
            
            var parameters = new object?[ConstructorInfo.Parameters.Count];
            for(var pi = 0; pi < parameters.Length; pi++)
            {
                var ai = ArgumentMap[pi];
                if (ai == -1)
                    parameters[pi] = ConstructorInfo.Parameters[pi].DefaultValue;
                else
                    parameters[pi] = values[ai];
            }
            return ConstructorInfo.Invoke(parameters);
        }
    }
}
