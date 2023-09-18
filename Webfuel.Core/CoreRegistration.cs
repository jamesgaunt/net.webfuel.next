using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.MediatR;

namespace Webfuel
{
    public static class CoreRegistration
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            RepositoryRegistration.AddRepositoryServices(services);
            
            services.RegisterServiceImplementationsFromAssembly(typeof(CoreRegistration).Assembly);

            services.RegisterCommandValidatorsFromAssembly(typeof(CoreRegistration).Assembly);

            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Webfuel.BlobStorage>());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
