using Microsoft.Extensions.DependencyInjection;

namespace Webfuel
{
    public interface IServiceEnumerator
    {
        List<T> EnumerateServices<T>();
    }

    [ServiceImplementation(typeof(IServiceEnumerator))]
    class ServiceEnumerator : IServiceEnumerator
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
