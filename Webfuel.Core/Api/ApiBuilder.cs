using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace Webfuel
{
    public static class ApiBuilder
    {
        public static void UseApiServices<TMarker>(this IEndpointRouteBuilder app)
        {
            var services = GetApiServiceTypesFromAssemblyContaining<TMarker>();

            foreach(var service in services)
            {
                RegisterApiMethodsFromServiceType(service, app);
            }
        }

        public static IEnumerable<Type> GetApiServiceTypesFromAssemblyContaining<TMarker>()
        {
            foreach (var type in typeof(TMarker).Assembly.ExportedTypes)
            {
                if (type.GetCustomAttribute<ApiServiceAttribute>() != null)
                    yield return type;
            }
        }

        public static void RegisterApiMethodsFromServiceType(Type serviceType, IEndpointRouteBuilder app)
        {
            var register = serviceType.GetMethod("RegisterEndpoints", BindingFlags.Static | BindingFlags.Public);

            if (register == null)
                throw new InvalidOperationException("No RegisterEndpoints static method on api service type: " + serviceType.Name);

            register.Invoke(null, new object[] { app });
        }
    }
}
