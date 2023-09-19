using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.MediatR;

namespace Webfuel
{
    public class CoreAssemblyMarker { }

    public static class CoreRegistration
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.RegisterServicesFromAssembly(typeof(CoreRegistration).Assembly);

            services.RegisterValidatorsFromAssembly(typeof(CoreRegistration).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
