using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    internal static class ScribbleMethodBinder
    {
        public static ScribbleMethodBinding? GetMethodBinding(ScribbleMethodBindingContext context)
        {
            var cacheKey = context.CacheKey;
            if (MethodBindingCache.TryGetValue(cacheKey, out var cached))
                return cached;

            var binding = GetMethodBindingWithUnwrap(context);

            // We cannot cache a binding if it required something in local scope to resolve (currently only lambda expressions)
            if (!context.DoNotCache)
                MethodBindingCache[cacheKey] = binding;

            return binding;
        }

        static ScribbleMethodBinding? GetMethodBindingWithUnwrap(ScribbleMethodBindingContext context)
        {
            try
            {
                return GetMethodBindingImpl(context);
            }
            catch
            {
                // If we can't find a binding then try the underlying type
                // Note we don't need to do this on execution as we are dealing
                // with object? which has already been unwrapped.

                var underlyingType = Nullable.GetUnderlyingType(context.TargetType);
                if (underlyingType == null)
                    throw;

                context = new ScribbleMethodBindingContext(underlyingType, context.Name, context.Args, context.TypeArguments, context.LambdaTypeResolver);

                return GetMethodBindingWithUnwrap(context);
            }
        }

        static ScribbleMethodBinding GetMethodBindingImpl(ScribbleMethodBindingContext context)
        {
            var attempts = GetMethodBindingAttempts(context);

            if (attempts.Count == 1)
                return ScribbleMethodBinding.Create(attempts[0].MethodInfo, attempts[0].ArgumentMap);

            if (attempts.Count == 0)
                throw new InvalidOperationException($"No suitable method '{context.Name}' found on type '{context.TypeInfo.Name}'");

            var minimumDefaultCount = attempts.Min(p => p.DefaultCount);
            attempts = attempts.Where(p => p.DefaultCount == minimumDefaultCount).ToList();

            var minimumConversionCount = attempts.Min(p => p.ConversionCount);
            attempts = attempts.Where(p => p.ConversionCount == minimumConversionCount).ToList();

            if (attempts.Count > 1)
                throw new InvalidOperationException($"Ambiguous binding for method '{context.Name}' on type '{context.TypeInfo.Name}'");

            return ScribbleMethodBinding.Create(attempts[0].MethodInfo, attempts[0].ArgumentMap);
        }

        static List<ScribbleMethodBindingAttempt> GetMethodBindingAttempts(ScribbleMethodBindingContext context)
        { 
            var attempts = new List<ScribbleMethodBindingAttempt>();

            // Check for methods on the object itself
            foreach (var method in context.TypeInfo.Methods)
            {
                if (method.Name != context.Name)
                    continue;

                var attempt = AttemptMethodBinding(new ScribbleMethodBindingAttempt(context, method));
                if (attempt == null)
                    continue;
                attempts.Add(attempt);
            }

            // Check for default interface methods
            if (attempts.Count == 0)
            {
                foreach (var method in context.TypeInfo.InterfaceMethods)
                {
                    if (method.Name != context.Name)
                        continue;

                    var attempt = AttemptMethodBinding(new ScribbleMethodBindingAttempt(context, method));
                    if (attempt == null)
                        continue;
                    attempts.Add(attempt);
                }
            }

            // Check for extension methods
            if(attempts.Count == 0)
            {
                foreach (var method in context.TypeInfo.GetExtensionMethods(context.Name))
                {
                    if (method.Name != context.Name)
                        continue;

                    var attempt = AttemptMethodBinding(new ScribbleMethodBindingAttempt(context, method)); 
                    if (attempt == null)
                        continue;
                    attempts.Add(attempt);
                }
            }

            return attempts;
        }

        static ScribbleMethodBindingAttempt? AttemptMethodBinding(ScribbleMethodBindingAttempt attempt)
        {
            if (!attempt.BuildArgumentMap())
                return null;

            if (!attempt.ValidateTypeCompatibility())
                return null;

            if (!attempt.ResolveGenericTypeDefinition())
                return null;

            return attempt;
        }

        // Method Binding Cache

        static ConcurrentDictionary<string, ScribbleMethodBinding?> MethodBindingCache = new ConcurrentDictionary<string, ScribbleMethodBinding?>();
    }
}