using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    internal static class ScribbleConstructorBinder
    {
        public static ScribbleConstructorBinding? GetConstructorBinding(ScribbleTypeName typeName, ScribbleArgInfo[] args)
        {
            // Check to see if we have already cached this binding
            var key = ConstructorBindingKey(typeName, args);
            if (ConstructorBindingCache.TryGetValue(key, out var cached))
                return cached;

            return ConstructorBindingCache[key] = GetConstructorBindingImpl(typeName, args);
        }

        static ScribbleConstructorBinding GetConstructorBindingImpl(ScribbleTypeName typeName, ScribbleArgInfo[] args)
        {
            var typeInfo = Reflection.GetConstructableType(typeName);
            if (typeInfo == null)
                throw new InvalidOperationException($"Type '{typeName}' is not constructable");

            var methodBindings = GetConstructorBindings(typeInfo, args);

            if (methodBindings.Count == 1)
                return methodBindings[0];

            if (methodBindings.Count == 0)
                throw new InvalidOperationException($"No suitable constructor found on type '{typeInfo.Name}'");

            // We have multiple bindings, first filter out any with more than the minimum number of defaults
            var minimumDefaultCount = methodBindings.Min(p => p.DefaultCount);
            methodBindings = methodBindings.Where(p => p.DefaultCount == minimumDefaultCount).ToList();

            if (methodBindings.Count > 0)
                throw new InvalidOperationException($"Ambiguous binding for constructor on type '{typeInfo.Name}'");

            return methodBindings[0];
        }

        static List<ScribbleConstructorBinding> GetConstructorBindings(ScribbleTypeInfo typeInfo, ScribbleArgInfo[] args)
        {
            var minimumDefaultCount = 999;
            var methodBindings = new List<ScribbleConstructorBinding>();

            // Check for methods on the object itself
            foreach (var method in typeInfo.Constructors)
            {
                var binding = AttemptConstructorBinding(method, args);
                if (binding == null)
                    continue;

                if (binding.DefaultCount > minimumDefaultCount)
                    continue;
                minimumDefaultCount = binding.DefaultCount;

                methodBindings.Add(binding);
            }

            return methodBindings;
        }

        static ScribbleConstructorBinding? AttemptConstructorBinding(ScribbleConstructorInfo methodInfo, ScribbleArgInfo[] args)
        {
            int defaultCount = 0;

            // Extract parameters
            var parameters = new List<ScribbleParameterInfo>();
            parameters.AddRange(methodInfo.Parameters);

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
                return ScribbleConstructorBinding.Create(methodInfo, defaultCount, null);
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

            return ScribbleConstructorBinding.Create(methodInfo, defaultCount, positional ? null : argumentMap);
        }

        static bool AreCompatible(Type argumentType, Type parameterType)
        {
            if (Reflection.CanCast(argumentType, parameterType))
                return true;

            // Need to check for lambda compatibility
            /* 
            if(argumentType.Name.StartsWith("ScribbleFunc`") && parameterType.Name.StartsWith("ScribbleFunc`"))
            {
                var argumentParams = int.Parse(argumentType.Name.Remove(0, "ScribbleFunc`".Length)) - 1;
                var parameterParams = int.Parse(parameterType.Name.Remove(0, "ScribbleFunc`".Length)) - 1;

                if (argumentParams != parameterParams)
                    return false;

                return true;
            }
            */
            
            return false;
        }

        // Constructor Binding Cache

        static ConcurrentDictionary<string, ScribbleConstructorBinding?> ConstructorBindingCache = new ConcurrentDictionary<string, ScribbleConstructorBinding?>();

        static string ConstructorBindingKey(ScribbleTypeName typeName, ScribbleArgInfo[] args)
        {
            var sb = new StringBuilder($"{typeName}#");
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