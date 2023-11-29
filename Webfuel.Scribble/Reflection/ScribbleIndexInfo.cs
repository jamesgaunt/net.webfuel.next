using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    class ScribbleIndexInfo
    {
        private readonly PropertyInfo PropertyInfo;

        public ScribbleIndexInfo(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;

            Name = propertyInfo.Name;
            PropertyType = propertyInfo.PropertyType;
            DeclaringType = propertyInfo.DeclaringType!;

            Parameters = propertyInfo.GetIndexParameters().Select(p => new ScribbleParameterInfo(p)).ToList();

            CanSet = propertyInfo.GetSetMethod() != null;
            CanGet = propertyInfo.GetGetMethod() != null;
        }

        public string Name { get; }

        public Type PropertyType { get; }

        public Type DeclaringType { get; }

        public IReadOnlyList<ScribbleParameterInfo> Parameters { get; }

        public bool CanSet { get; }

        public bool CanGet { get; }

        // Get / Set

        public object? GetValue(object obj, object?[] args)
        {
            return PropertyInfo.GetValue(obj, args);
        }

        public void SetValue(object obj, object? value, object?[] args)
        {
            PropertyInfo.SetValue(obj, value, args);
        }
    }
}
