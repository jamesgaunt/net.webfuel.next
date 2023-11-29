using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    class ScribbleConstructorInfo
    {
        private readonly ConstructorInfo ConstructorInfo;

        public ScribbleConstructorInfo(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;

            Parameters = constructorInfo.GetParameters().Select(p => new ScribbleParameterInfo(p)).ToList();
            DeclaringType = constructorInfo.DeclaringType!;
        }

        public IReadOnlyList<ScribbleParameterInfo> Parameters { get; }

        public Type DeclaringType { get; }

        // Here as a helper for unit testing
        public string DisplayName
        {
            get
            {
                var attribute = ConstructorInfo.GetCustomAttribute<DisplayNameAttribute>();
                if (attribute == null)
                    return String.Empty;
                return attribute.DisplayName;
            }
        }

        // Invoke

        public object? Invoke(object?[] parameters)
        {
            return ConstructorInfo.Invoke(parameters);
        }
    }
}
