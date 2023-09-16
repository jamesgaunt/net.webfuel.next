namespace Webfuel
{

    [AttributeUsage(AttributeTargets.Class)]
    public class TypefuelApiAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class TypefuelIgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class TypefuelStaticAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TypefuelInterfaceAttribute : Attribute
    {
        public string Signature { get; set; } = String.Empty;
    }
}
