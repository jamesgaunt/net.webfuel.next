namespace Webfuel
{
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
