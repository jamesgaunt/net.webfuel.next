using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceImplementationAttribute : Attribute
    {
        public ServiceImplementationAttribute(Type implements, params Type[] interfaces)
        {
            Implements = implements;
            Interfaces.AddRange(interfaces);
        }

        public Type Implements { get; private set; }

        public List<Type> Interfaces { get; } = new List<Type>();
    }
}
