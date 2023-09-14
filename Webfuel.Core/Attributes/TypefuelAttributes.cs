using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TypefuelControllerAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class TypefuelIgnoreAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class TypefuelStaticAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TypefuelActionAttribute: Attribute
    {
        public int RetryCount { get; set; } = -1;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TypefuelInterfaceAttribute : Attribute
    {
        public string Signature { get; set; } = String.Empty;
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class OmitAttribute: Attribute
    {
        public OmitAttribute(params string[] keys)
        {
            Keys.AddRange(Keys);
        }

        public List<string> Keys { get; } = new List<string>();
    }
}
