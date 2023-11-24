using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.MediatR;

namespace Webfuel
{
    public class ExcelAssemblyMarker { }

    public static class ExcelRegistration
    {
        public static void RegisterExcelServices(this IServiceCollection services)
        {
            services.RegisterServicesFromAssembly(typeof(ExcelRegistration).Assembly);
        }
    }
}
