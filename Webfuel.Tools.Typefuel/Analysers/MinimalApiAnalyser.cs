using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Metadata;
using static System.Collections.Specialized.BitVector32;

namespace Webfuel.Tools.Typefuel
{
    public class MinimalApiAnalyser
    {
        public MinimalApiAnalyser AnalyseAssembly(ApiSchema schema, Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.GetCustomAttribute<ApiServiceAttribute>() == null)
                    continue;

                AnalyseApi(schema, type);
            }

            return this;
        }

        public void AnalyseApi(ApiSchema schema, Type serviceType)
        {
            var service = new ApiService(schema);
            service.Name = serviceType.Name.Replace("Api", "");

            var routeBuilder = new FakeEndpointRouteBuilder();

            var register = serviceType.GetMethod("RegisterEndpoints", BindingFlags.Static | BindingFlags.Public);

            if (register == null)
                throw new InvalidOperationException("No RegisterEndpoints static method on api service type: " + serviceType.Name);

            register.Invoke(null, new object[] { routeBuilder });

            var routeEntries = GetFieldValue<IEnumerable>(routeBuilder.DataSources.First(), "_routeEntries");

            foreach (var routeEntry in routeEntries)
                AnalyseRouteEntry(service, routeEntry);
            schema.Services.Add(service);
        }

        void AnalyseRouteEntry(ApiService service, object routeEntry)
        {
            var handler = GetPropertyValue<Delegate>(routeEntry, "RouteHandler");
            var methods = GetPropertyValue<IEnumerable<string>>(routeEntry, "HttpMethods");
            var pattern = GetPropertyValue<RoutePattern>(routeEntry, "RoutePattern");

            var methodInfo = handler.GetMethodInfo();
            var parameters = methodInfo.GetParameters();
            var returnType = methodInfo.ReturnType;

            var method = new ApiMethod(service);

            method.Name = GenerateMethodName(methodInfo, pattern.RawText);
            method.Verb = methods.First();
            method.Route = ApiRoute.Parse(pattern.RawText);
            method.ReturnTypeDescriptor = service.Schema.TypeContext.GetTypeDescriptor(returnType);

            foreach (var parameterInfo in parameters)
            {
                if (parameterInfo.GetCustomAttribute<ApiIgnoreAttribute>() != null)
                    continue;

                var methodParameter = new ApiMethodParameter(method);

                methodParameter.Source = GetParameterSource(method.Route, parameterInfo);
                if (methodParameter.Source == ApiMethodParameterSource.Unknown)
                    continue;

                methodParameter.Name = parameterInfo.Name;
                methodParameter.TypeDescriptor = method.Service.Schema.TypeContext.GetTypeDescriptor(parameterInfo.ParameterType);

                method.Parameters.Add(methodParameter);
            }

            service.Methods.Add(method);
        }

        string GenerateMethodName(MethodInfo methodInfo, string pattern)
        {
            if (!String.IsNullOrEmpty(methodInfo.Name))
                return methodInfo.Name;

            var parts = pattern.Split('/');
            if (parts.Length >= 2 && parts[0] == "api")
                return parts[1];
            throw new InvalidOperationException("Unable to generate method name");
        }

        ApiMethodParameterSource GetParameterSource(ApiRoute route, ParameterInfo parameterInfo)
        {
            if (parameterInfo.GetCustomAttribute<FromBodyAttribute>() != null)
                return ApiMethodParameterSource.Body;

            foreach (var segment in route.Segments)
            {
                if (segment is ApiRouteParameterSegment parameterSegment)
                {
                    if (parameterSegment.Name == parameterInfo.Name)
                        return ApiMethodParameterSource.Route;
                }
            }

            return ApiMethodParameterSource.Unknown;
        }

        TValue GetFieldValue<TValue>(object source, string name)
        {
            var type = source.GetType();

            return (TValue)type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Single(p => p.Name == name).GetValue(source);
        }

        TValue GetPropertyValue<TValue>(object source, string name)
        {
            var type = source.GetType();

            return (TValue)type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Single(p => p.Name == name).GetValue(source);
        }
    }

    public class FakeEndpointRouteBuilder : IEndpointRouteBuilder
    {
        public ICollection<EndpointDataSource> DataSources { get; } = new List<EndpointDataSource>();

        public IServiceProvider ServiceProvider { get; } = new FakeServiceProvider();

        public IApplicationBuilder CreateApplicationBuilder()
        {
            throw new NotImplementedException();
        }
    }

    public class FakeServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IOptions<RouteHandlerOptions>))
                return Options.Create(new RouteHandlerOptions { });

            if (serviceType == typeof(IServiceProviderIsService))
                return new FakeIsService();

            throw new NotImplementedException();
        }
    }

    public class FakeIsService : IServiceProviderIsService
    {
        public bool IsService(Type serviceType)
        {
            return true;
        }
    }
}
