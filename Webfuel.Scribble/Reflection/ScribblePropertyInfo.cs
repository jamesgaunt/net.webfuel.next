using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    class ScribblePropertyInfo
    {
        private readonly PropertyInfo PropertyInfo;

        public ScribblePropertyInfo(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;

            Name = propertyInfo.Name;
            PropertyType = propertyInfo.PropertyType;
            DeclaringType = propertyInfo.DeclaringType!;

            CanSet = propertyInfo.GetSetMethod() != null;
            CanGet = propertyInfo.GetGetMethod() != null;
        }

        public string Name { get; }

        public Type PropertyType { get; }

        public Type DeclaringType { get; }

        public bool CanSet { get; }

        public bool CanGet { get; }

        // Get / Set

        public object? GetValue(object obj)
        {
            return PropertyInfo.GetValue(obj);
        }

        public void SetValue(object obj, object? value)
        {
            // There are some weaknesses in terms of type converion here - so we just manually deal with them as they arise!
            if(value != null)
            {
                if(Reflection.IsNumeric(PropertyType) && Reflection.IsNumeric(value.GetType()))
                    value = Convert.ChangeType(value, PropertyType);
            }

            PropertyInfo.SetValue(obj, value);
        }
    }
}
