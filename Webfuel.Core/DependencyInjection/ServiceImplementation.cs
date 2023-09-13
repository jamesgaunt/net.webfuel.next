using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Webfuel
{
    public static class ServiceImplementation
    {
        public static void Discover(Assembly assembly, IServiceCollection services)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                var attribute = type.GetCustomAttribute<ServiceImplementationAttribute>();
                if (attribute == null)
                    continue;

                // Primary Interface
                if (!type.ImplementedInterfaces.Contains(attribute.Implements))
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
