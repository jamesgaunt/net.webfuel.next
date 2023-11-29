using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    class ScribbleIndexBinding
    {
        ScribbleIndexBinding(ScribbleIndexInfo indexInfo, int defaultCount, List<int>? argumentMap)
        {
            IndexInfo = indexInfo;
            DefaultCount = defaultCount;
            ArgumentMap = argumentMap;
            ReturnType = IndexInfo.PropertyType;

            // Unwrap Task returned from Async methods
            if (ReturnType.IsGenericType && ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                ReturnType = ReturnType.GetGenericArguments()[0];
        }

        public ScribbleIndexInfo IndexInfo { get; }

        public bool CanSet { get => IndexInfo.CanSet; }

        public bool CanGet { get => IndexInfo.CanGet; }

        public Type PropertyType { get => IndexInfo.PropertyType; }

        public int DefaultCount { get; }

        // ArgumentMap is either null, or maps each parameter index to an argument index
        public IReadOnlyList<int>? ArgumentMap { get; } = null;

        public Type ReturnType { get; }

        public static ScribbleIndexBinding Create(ScribbleIndexInfo indexInfo, int defaultCount, List<int>? argumentMap)
        {
            return new ScribbleIndexBinding(indexInfo, defaultCount, argumentMap);
        }

        public Type GetArgumentType(int argumentIndex)
        {
            if(ArgumentMap == null)
            {
                return IndexInfo.Parameters[argumentIndex].ParameterType;
            }

            for(var pi = 0; pi < IndexInfo.Parameters.Count; pi++)
            {
                if (ArgumentMap[pi] == argumentIndex)
                    return IndexInfo.Parameters[pi].ParameterType;
            }

            throw new InvalidOperationException("Unable to deduce argument type");
        }

        public object? Get(object obj, object?[] args)
        {
            if (ArgumentMap == null)
                return IndexInfo.GetValue(obj, args);

            var parameters = new object?[IndexInfo.Parameters.Count];
            for (var pi = 0; pi < parameters.Length; pi++)
            {
                var ai = ArgumentMap[pi];
                if (ai == -1)
                    parameters[pi] = IndexInfo.Parameters[pi].DefaultValue;
                else
                    parameters[pi] = args[ai];
            }
            return IndexInfo.GetValue(obj, parameters);
        }

        public void Set(object obj, object? value, object?[] args)
        {
            if(ArgumentMap == null) { 
                IndexInfo.SetValue(obj, value, args);
                return;
            }

            var parameters = new object?[IndexInfo.Parameters.Count];
            for(var pi = 0; pi < parameters.Length; pi++)
            {
                var ai = ArgumentMap[pi];
                if (ai == -1)
                    parameters[pi] = IndexInfo.Parameters[pi].DefaultValue;
                else
                    parameters[pi] = args[ai];
            }
            IndexInfo.SetValue(obj, value, parameters);
        }
    }
}
