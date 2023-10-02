namespace Webfuel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(Type implements, params Type[] interfaces)
        {
            Implements = implements;
            Interfaces.AddRange(interfaces);
        }

        public Type Implements { get; private set; }

        public List<Type> Interfaces { get; } = new List<Type>();
    }
}
