using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Webfuel
{
    public static class ServiceRegistration
    {
        public static void RegisterServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                var attribute = type.GetCustomAttribute<ServiceAttribute>();
                if (attribute == null)
                    continue;

                // Primary Interface
                if (attribute.Implements.IsInterface && !type.ImplementedInterfaces.Contains(attribute.Implements))
                    throw new InvalidOperationException($"Type {type.Name} does not implement service interface {attribute.Implements.Name}");
                
                services.AddTransient(attribute.Implements, type);

                // Secondary Interfaces
                foreach (var @interface in attribute.Interfaces)
                {
                    if (!type.ImplementedInterfaces.Contains(@interface))
                        throw new InvalidOperationException($"Type {type.Name} does not implement service interface {@interface.Name}");
                    services.AddTransient(serviceType: @interface, x => x.GetRequiredService(attribute.Implements));
                }
            }
        }
    }
}
