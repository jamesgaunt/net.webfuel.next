namespace Webfuel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiServiceAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property)]
    public class ApiIgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class ApiStaticAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ApiTypeAttribute : Attribute
    {
        public string Signature { get; set; } = String.Empty;
    }
}
