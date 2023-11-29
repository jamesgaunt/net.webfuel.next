using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Scribble
{
    internal class ScribbleMethodBindingAttempt
    {
        public ScribbleMethodBindingAttempt(ScribbleMethodBindingContext context, ScribbleMethodInfo methodInfo)
        {
            Context = context;
            MethodInfo = methodInfo;
            NonExtensionParameters = MethodInfo.ExtensionMethod ? MethodInfo.Parameters.Skip(1).ToList() : MethodInfo.Parameters;
        }

        public ScribbleMethodBindingContext Context { get; set; }

        public ScribbleMethodInfo MethodInfo { get; set; }

        public List<int>? ArgumentMap { get; set; }

        public IReadOnlyList<ScribbleParameterInfo> NonExtensionParameters { get; }

        public int DefaultCount { get { return NonExtensionParameters.Count - Context.Args.Length; } }

        public int ConversionCount { get; set; }

        /// <summary>
        /// Build a map from parameters to argument by index, using named and position argument rules. This method makes no check on type compatibility.
        /// </summary>
        /// <returns>true if a valid argument map could be built</returns>
        public bool BuildArgumentMap()
        {
            // We can never have more arguments than parameters (we don't support params arrays)
            if (Context.Args.Length > NonExtensionParameters.Count)
                return false;

            // Trivial case
            if (Context.Args.Length == 0)
            {
                // All parameters must have default values
                foreach (var parameter in NonExtensionParameters)
                {
                    if (!parameter.HasDefaultValue)
                        return false;
                }
                return true;
            }

            // For each parameter, give the index of the argument to use, or -1 for default value
            bool positional = true;
            var argumentMap = new List<int>(NonExtensionParameters.Count);
            for (var pi = 0; pi < NonExtensionParameters.Count; pi++)
                argumentMap.Add(-1);

            // Build the argument map
            {
                // Are we dealing with positional or named parameters

                for (var ai = 0; ai < Context.Args.Length; ai++)
                {
                    if (Context.Args[ai].Name == null)
                    {
                        if (positional == false)
                            return false; // We can't have a positional argument after a named argument (the parser should enforce this anyway)

                        // This is an easy one!
                        argumentMap[ai] = ai;
                    }
                    else
                    {
                        positional = false;

                        // Named argument, so find the matching parameter
                        var match = false;
                        for (var pi = 0; pi < NonExtensionParameters.Count; pi++)
                        {
                            if (Context.Args[ai].Name != NonExtensionParameters[pi].Name)
                                continue;

                            if (argumentMap[pi] != -1)
                                return false; // We already have a candidate match for this parameter

                            argumentMap[pi] = ai; // Map argument ai to parameter pi
                            match = true;
                        }
                        if (!match)
                            return false; // We couldn't match this named argument
                    }
                }
            }

            // Check all parameters have values (mapped argument or default)
            for (var pi = 0; pi < NonExtensionParameters.Count; pi++)
            {
                if (argumentMap[pi] != -1)
                    continue;
                positional = false;
                if (NonExtensionParameters[pi].HasDefaultValue)
                    continue;
                return false; // One of our unsupplied parameters doesn't have a default
            }

            ArgumentMap = positional ? null : argumentMap;
            return true;
        }

        public Type GetInvokedParameterType(int pi)
        {
            if (MethodInfo.ExtensionMethod)
            {
                if (pi == 0)
                    return Context.TargetType;
                else
                    pi -= 1;
            }
            var ai = MapParameterIndexToArgumentIndex(pi);
            return ai == -1 ? NonExtensionParameters[pi].ParameterType : Context.Args[ai].ArgumentType;
        }

        public int MapParameterIndexToArgumentIndex(int parameterIndex)
        {
            if (ArgumentMap == null)
                return parameterIndex < Context.Args.Length ? parameterIndex : -1;
            return ArgumentMap[parameterIndex];
        }

        public int MapArgumentIndexToParameterIndex(int argumentIndex)
        {
            if (ArgumentMap == null)
                return argumentIndex;

            for (int pi = 0; pi < ArgumentMap.Count; pi++)
            {
                if (ArgumentMap[pi] == argumentIndex)
                    return pi;
            }
            return -1;
        }

        public bool ValidateTypeCompatibility()
        {
            for (var ai = 0; ai < Context.Args.Count(); ai++)
            {
                var pi = MapArgumentIndexToParameterIndex(ai);

                if (NonExtensionParameters[pi].ParameterType.IsGenericMethodParameter)
                    continue; // We can't check generic parameters directly, will be resolved by type inference

                if (!AreTypeCompatible(Context.Args[ai].ArgumentType, NonExtensionParameters[pi].ParameterType))
                    return false;
            }
            return true;
        }

        bool AreTypeCompatible(Type argumentType, Type parameterType)
        {
            if (argumentType == parameterType)
                return true;

            if (Reflection.CanCast(argumentType, parameterType))
            {
                ConversionCount++;
                return true;
            }

            // Need to check for lambda compatibility, for now just on the parameter count
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

        public bool ResolveGenericTypeDefinition()
        {
            // If the method is not generic we must have specified no type arguments
            if (!MethodInfo.IsGenericMethodDefinition)
                return Context.TypeArguments == null;

            // If we have explicit type arguments then use them
            if (Context.TypeArguments != null)
            {
                if (Context.TypeArguments.Count != MethodInfo.GetGenericArguments().Length)
                    return false;

                try
                {
                    MethodInfo = MethodInfo.MakeGenericMethod(Context.TypeArguments);
                    return true;
                }
                catch
                {
                    // Failed to create a concrete method
                    return false;
                }
            }

            if (AttemptTypeInference())
                return true;

            // Unable to infer type arguments
            return false;
        }

        bool AttemptTypeInference()
        {
            var methodGenericParameters = MethodInfo.GetGenericArguments();

            var typeArguments = new Type[methodGenericParameters.Length];
            for (var i = 0; i < typeArguments.Length; i++)
                typeArguments[i] = Reflection.UnknownType;

            var unknownTypeArgumentCount = typeArguments.Count(p => p == Reflection.UnknownType);

            // NOTE: We may need to loop here if parameters are not solveable in parameter order!

            for (var pi = 0; pi < MethodInfo.Parameters.Count; pi++)
            {
                var parameter = MethodInfo.Parameters[pi];

                if (parameter.ParameterType.IsGenericType)
                {
                    // This parameter is a generic type

                    var pTypeArguments = parameter.ParameterType.GetGenericArguments();
                    var argumentType = GetInvokedParameterType(pi);
                    var aTypeArguments = argumentType.GetGenericArguments();

                    if (argumentType.Name.StartsWith("ScribbleFunc`"))
                    {
                        // Lambda Return Type Solution. We need to have solved all lambda parameter types first, then deduce return value.
                        if (Context.LambdaTypeResolver != null)
                        {
                            var lambdaParameterTypes = new Type[pTypeArguments.Length - 1];
                            for (var lpi = 0; lpi < pTypeArguments.Length - 1; lpi++)
                            {
                                for (var mgi = 0; mgi < methodGenericParameters.Length; mgi++)
                                {
                                    if (methodGenericParameters[mgi].Name == pTypeArguments[lpi].Name)
                                    {
                                        lambdaParameterTypes[lpi] = typeArguments[mgi];
                                    }
                                }
                            }

                            var lambdaType = Context.LambdaTypeResolver(lambdaParameterTypes);
                            if (lambdaType != Reflection.UnknownType)
                            {
                                var sigType = pTypeArguments[pTypeArguments.Length - 1];
                                if(lambdaType == sigType || sigType.IsGenericParameter)
                                {
                                    // We are good
                                }
                                else if(Reflection.CanCast(lambdaType, sigType))
                                {
                                    // Requires a conversion on the lambda return value
                                    ConversionCount++;
                                }
                                else
                                {
                                    // We cannot convert the lambda return value
                                    return false;
                                }

                                // We can no longer cache this binding
                                Context.DoNotCache = true;

                                for (var mgi = 0; mgi < methodGenericParameters.Length; mgi++)
                                {
                                    if (typeArguments[mgi] != Reflection.UnknownType)
                                        continue; // We already solved this type

                                    if (methodGenericParameters[mgi].Name == pTypeArguments[pTypeArguments.Length - 1].Name)
                                    {
                                        // We seem to have solved this type argument!
                                        typeArguments[mgi] = lambdaType;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Placewise solution. Match type parameters against arguments by name.
                        if (pTypeArguments.Length == aTypeArguments.Length)
                        {
                            for (var pti = 0; pti < pTypeArguments.Length; pti++)
                            {
                                if (aTypeArguments[pti].IsGenericTypeParameter)
                                    continue; // This type isn't resolved 

                                for (var mgi = 0; mgi < methodGenericParameters.Length; mgi++)
                                {
                                    if (typeArguments[mgi] != Reflection.UnknownType)
                                        continue; // We already solved this type

                                    if (methodGenericParameters[mgi].Name == pTypeArguments[pti].Name)
                                    {
                                        // We seem to have solved this type argument!
                                        typeArguments[mgi] = aTypeArguments[pti];
                                    }
                                }
                            }
                        }
                    }
                }
                else if(parameter.ParameterType.IsGenericMethodParameter)
                {
                    // This parameter is simply generic, so we need a concrete type from argument

                    var argumentType = GetInvokedParameterType(pi);

                    for (var mgi = 0; mgi < methodGenericParameters.Length; mgi++)
                    {
                        if (typeArguments[mgi] != Reflection.UnknownType)
                            continue; // We already solved this type

                        if (methodGenericParameters[mgi].Name == parameter.ParameterType.Name)
                        {
                            // We seem to have solved this type argument!
                            typeArguments[mgi] = argumentType;
                        }
                    }
                }
            }

            if (typeArguments.Any(p => p == Reflection.UnknownType))
                return false;

            // Try to construct a suitable method using the calculated type arguments
            try
            {
                var constructedMethod = MethodInfo.MakeGenericMethod(typeArguments);

                // For an extension method we need the first parameter to match the target type
                if (MethodInfo.ExtensionMethod && !Reflection.CanCast(Context.TargetType, constructedMethod.Parameters[0].ParameterType))
                    return false;

                MethodInfo = constructedMethod;
                return true;

            }
            catch { /* GULP */ }

            return false;
        }
    }
}
