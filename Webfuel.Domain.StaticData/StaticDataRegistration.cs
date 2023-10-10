using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.MediatR;

namespace Webfuel.Domain.StaticData;

public class StaticDataAssemblyMarker { }

public static class StaticDataRegistration
{
    public static void RegisterStaticDataServices(this IServiceCollection services)
    {
        services.RegisterServicesFromAssembly(typeof(StaticDataAssemblyMarker).Assembly);

        services.RegisterValidatorsFromAssembly(typeof(StaticDataAssemblyMarker).Assembly);
    }
}
