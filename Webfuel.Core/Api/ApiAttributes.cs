namespace Webfuel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiServiceAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ApiDataSource: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ApiStaticData : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Interface | AttributeTargets.Method)]
    public class ApiIgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class ApiEnumAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ApiTypeAttribute : Attribute
    {
        public string Signature { get; set; } = String.Empty;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ApiOptionalAttribute: Attribute
    {

    }
}
