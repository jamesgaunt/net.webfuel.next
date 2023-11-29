using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    // TODO: Create Cachable Binding

    static class ScribblePropertyBinder
    {
        public static object? GetProperty(object? target, string name)
        {
            if (target == null)
                throw new InvalidOperationException($"Attempt to get property '{name}' on null target");

            if (target is IScribbleDynamicProperties)
            {
                var bag = target as IScribbleDynamicProperties;
                return bag!.GetProperty(name);
            }

            var property = FindProperty(target.GetType(), name, set: false, get: true);
            return property.GetValue(target);
        }

        public static object? SetProperty(object? target, string name, object? value)
        {
            if (target == null)
                throw new InvalidOperationException($"Attempt to set property '{name}' on null target");

            if(target is IScribbleDynamicProperties)
            {
                var bag = target as IScribbleDynamicProperties;
                return bag!.SetProperty(name, value);
            }

            var property = FindProperty(target.GetType(), name, set: true, get: false);
            property.SetValue(target, value);

            if (property.CanGet)
                return property.GetValue(target);
            return null;
        }

        public static Type PropertyType(Type targetType, string name, bool set, bool get)
        {
            if (targetType.GetInterfaces().Any(p => p == typeof(IScribbleDynamicProperties)))
                return Reflection.DynamicType;

            var property = FindProperty(targetType, name, set: set, get: get);
            return property.PropertyType;
        }

        static ScribblePropertyInfo FindProperty(Type targetType, string name, bool set, bool get)
        {
            var properties = Reflection.GetTypeInfo(targetType).Properties.Where(p => p.Name == name).ToList();

            if (properties.Count == 0)
                throw new InvalidOperationException($"Property '{name}' does not exist on type '{targetType.Name}'");

            if (properties.Count > 1)
                throw new InvalidOperationException($"Property '{name}' is ambiguous on type '{targetType.Name}'");

            var property = properties[0];

            if (set && !property.CanSet)
                throw new InvalidOperationException($"Property '{name}' does not have a public setter on type '{targetType.Name}'");

            if (get && !property.CanGet)
                throw new InvalidOperationException($"Property '{name}' does not have a public getter on type '{targetType.Name}'");

            return property;
        }
    }
}
