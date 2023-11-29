using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Scribble
{
    static class ScribbleIndexBinder
    {
        public static ScribbleIndexBinding? GetIndexBinding(Type targetType, ScribbleArgInfo[] args)
        {
            // Check to see if we have already cached this binding
            var key = IndexBindingKey(targetType, args);
            if (IndexBindingCache.TryGetValue(key, out var cached))
                return cached;

            return IndexBindingCache[key] = GetIndexBindingWithUnwrap(targetType, args);
        }

        static ScribbleIndexBinding? GetIndexBindingWithUnwrap(Type targetType, ScribbleArgInfo[] args)
        {
            try
            {
                return GetIndexBindingImpl(targetType, args);
            }
            catch
            {
                // If we can't find a binding then try the underlying type
                // Note we don't need to do this on execution as we are dealing
                // with object? which has already been unwrapped.

                var underlyingType = Nullable.GetUnderlyingType(targetType);
                if (underlyingType == null)
                    throw;

                return GetIndexBindingWithUnwrap(underlyingType, args);
            }
        }

        static ScribbleIndexBinding GetIndexBindingImpl(Type targetType, ScribbleArgInfo[] args)
        {
            var typeInfo = Reflection.GetTypeInfo(targetType);

            var indexBindings = GetIndexBindings(typeInfo, args);

            if (indexBindings.Count == 1)
                return indexBindings[0];

            if (indexBindings.Count == 0)
                throw new InvalidOperationException($"No suitable index found on type '{typeInfo.Name}'");

            // We have multiple bindings, first filter out any with more than the minimum number of defaults
            var minimumDefaultCount = indexBindings.Min(p => p.DefaultCount);
            indexBindings = indexBindings.Where(p => p.DefaultCount == minimumDefaultCount).ToList();

            if (indexBindings.Count > 0)
                throw new InvalidOperationException($"Ambiguous binding for index on type '{typeInfo.Name}'");

            return indexBindings[0];
        }

        static List<ScribbleIndexBinding> GetIndexBindings(ScribbleTypeInfo typeInfo, ScribbleArgInfo[] args)
        {
            var minimumDefaultCount = 999;
            var indexBindings = new List<ScribbleIndexBinding>();

            // Check for indexes on the object itself
            foreach (var index in typeInfo.Indexes)
            {
                var binding = AttemptIndexBinding(index, args);
                if (binding == null)
                    continue;

                if (binding.DefaultCount > minimumDefaultCount)
                    continue;
                minimumDefaultCount = binding.DefaultCount;

                indexBindings.Add(binding);
            }

            return indexBindings;
        }

        static ScribbleIndexBinding? AttemptIndexBinding(ScribbleIndexInfo indexInfo, ScribbleArgInfo[] args)
        {
            int defaultCount = 0;

            // Extract parameters
            var parameters = new List<ScribbleParameterInfo>();
            parameters.AddRange(indexInfo.Parameters);

            // Trivial case
            if (args.Length == 0)
            {
                // All parameters must have default values
                foreach (var parameter in parameters)
                {
                    if (!parameter.HasDefaultValue)
                        return null;
                    defaultCount++;
                }
                return ScribbleIndexBinding.Create(indexInfo, defaultCount, null);
            }

            // We can never have more arguments than parameters (we don't support params arrays)
            if (args.Length > parameters.Count)
                return null;

            // For each parameter, give the index of the argument to use, or -1 for default value
            bool positional = true;
            var argumentMap = new List<int>(parameters.Count);
            for (var pi = 0; pi < parameters.Count; pi++)
                argumentMap.Add(-1);

            // Build the argument map
            {
                // Are we dealing with positional or named parameters

                for (var ai = 0; ai < args.Length; ai++)
                {
                    if (args[ai].Name == null)
                    {
                        if (positional == false)
                            return null; // We can't have a positional argument after a named argument (the parser should enforce this anyway)

                        if (!AreCompatible(args[ai].ArgumentType, parameters[ai].ParameterType))
                            return null; // The types are incompatible

                        // This is an easy one!
                        argumentMap[ai] = ai;
                    }
                    else
                    {
                        positional = false;

                        // Named argument, so find the matching parameter
                        var match = false;
                        for (var pi = 0; pi < parameters.Count; pi++)
                        {
                            if (args[ai].Name != parameters[pi].Name)
                                continue;

                            if (argumentMap[pi] != -1)
                                return null; // We already have a candidate match for this parameter

                            if (!AreCompatible(args[ai].ArgumentType, parameters[pi].ParameterType))
                                return null; // The types are incompatible

                            argumentMap[pi] = ai; // Map argument ai to parameter pi
                            match = true;
                        }
                        if (!match)
                            return null; // We couldn't match this named argument
                    }
                }
            }

            // Check all parameters have values (mapped argument or default)
            for (var pi = 0; pi < parameters.Count; pi++)
            {
                if (argumentMap[pi] != -1)
                    continue;
                defaultCount++;
                positional = false;
                if (parameters[pi].HasDefaultValue)
                    continue;
                return null; // One of our unsupplied parameters doesn't have a default
            }

            return ScribbleIndexBinding.Create(indexInfo, defaultCount, positional ? null : argumentMap);
        }

        static bool AreCompatible(Type argumentType, Type parameterType)
        {
            if (Reflection.CanCast(argumentType, parameterType))
                return true;

            // Need to check for lambda compatibility
            if (argumentType.Name.StartsWith("ScribbleFunc`") && parameterType.Name.StartsWith("ScribbleFunc`"))
            {
                var argumentParams = int.Parse(argumentType.Name.Remove(0, "ScribbleFunc`".Length)) - 1;
                var parameterParams = int.Parse(parameterType.Name.Remove(0, "ScribbleFunc`".Length)) - 1;

                if (argumentParams != parameterParams)
                    return false;

                return true;
            }

            return false;
        }

        // Index Binding Cache

        static ConcurrentDictionary<string, ScribbleIndexBinding?> IndexBindingCache = new ConcurrentDictionary<string, ScribbleIndexBinding?>();

        static string IndexBindingKey(Type targetType, ScribbleArgInfo[] args)
        {
            var sb = new StringBuilder($"{targetType.FullName}#");
            foreach (var arg in args)
            {
                if (arg.Name != null)
                    sb.Append($"{arg.Name}:");
                sb.Append(arg.ArgumentType.FullName);
                sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
