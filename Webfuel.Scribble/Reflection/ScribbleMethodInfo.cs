using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{

    class ScribbleMethodInfo
    {
        internal readonly MethodInfo MethodInfo;

        public ScribbleMethodInfo(MethodInfo methodInfo, bool extensionMethod)
        {
            MethodInfo = methodInfo;

            Name = methodInfo.Name;
            Parameters = methodInfo.GetParameters().Select(p => new ScribbleParameterInfo(p)).ToList();
            ReturnType = methodInfo.ReturnType;
            DeclaringType = methodInfo.DeclaringType!;
            ExtensionMethod = extensionMethod;
        }

        public string Name { get; }

        public bool IsGenericMethodDefinition 
        { 
            get { return MethodInfo.IsGenericMethodDefinition; } 
        }

        public Type[] GetGenericArguments()
        {
            return MethodInfo.GetGenericArguments();
        }

        public ScribbleMethodInfo MakeGenericMethod(IEnumerable<ScribbleTypeName> typeNames)
        {
            return new ScribbleMethodInfo(MethodInfo.MakeGenericMethod(Reflection.MapConstructableTypes(typeNames).ToArray()), extensionMethod: ExtensionMethod);
        }

        public ScribbleMethodInfo MakeGenericMethod(Type[] typeArguments)
        {
            return new ScribbleMethodInfo(MethodInfo.MakeGenericMethod(typeArguments), extensionMethod: ExtensionMethod);
        }

        public T? GetCustomAttribute<T>() where T: Attribute
        {
            return MethodInfo.GetCustomAttribute<T>(true);
        }

        public IReadOnlyList<ScribbleParameterInfo> Parameters { get; }

        public Type ReturnType { get; }

        public Type DeclaringType { get; }

        public bool ExtensionMethod { get; }

        // Here as a helper for unit testing
        public string DisplayName
        {
            get
            {
                var attribute = MethodInfo.GetCustomAttribute<DisplayNameAttribute>();
                if (attribute == null)
                    return String.Empty;
                return attribute.DisplayName;
            }
        }

        // Invoke

        public object? Invoke(object obj, object?[] parameters)
        {
            return MethodInfo.Invoke(obj, parameters);
        }
    }
}
