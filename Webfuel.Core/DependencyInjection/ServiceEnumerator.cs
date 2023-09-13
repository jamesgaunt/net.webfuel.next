using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Webfuel
{
    public interface IServiceEnumerator
    {
        List<T> EnumerateServices<T>();
    }

    [ServiceImplementation(typeof(IServiceEnumerator))]
    class ServiceEnumerator: IServiceEnumerator
    {
        private readonly IServiceProvider ServiceProvider;

        public ServiceEnumerator(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public List<T> EnumerateServices<T>()
        {
            var services = new List<T>();
            services.AddRange(ServiceProvider.GetServices<T>());
            return services;
        }
    }
}
