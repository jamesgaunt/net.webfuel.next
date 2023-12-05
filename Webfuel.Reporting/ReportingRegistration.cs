using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.MediatR;

namespace Webfuel.Reporting
{
    public class ReportingAssemblyMarker { }

    public static class ReportingRegistration
    {
        public static void RegisterReportingServices(this IServiceCollection services)
        {
            services.RegisterServicesFromAssembly(typeof(ReportingAssemblyMarker).Assembly);
        }
    }
}
