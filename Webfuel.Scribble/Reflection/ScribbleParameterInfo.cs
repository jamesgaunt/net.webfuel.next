using System;
using System.Reflection;

namespace Webfuel.Scribble
{
    class ScribbleParameterInfo
    {
        public ScribbleParameterInfo(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name ?? throw new InvalidOperationException("Parameter name missing");
            ParameterType = parameterInfo.ParameterType;
            HasDefaultValue = parameterInfo.HasDefaultValue;
            if (HasDefaultValue)
                DefaultValue = parameterInfo.RawDefaultValue;

        }

        public string Name { get; }

        public Type ParameterType { get; }

        public bool HasDefaultValue { get; }
        
        public object? DefaultValue { get; }
    }
}
