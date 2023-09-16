using Webfuel;
using FluentValidation;
using Webfuel.MediatR;
using MediatR;
using Webfuel.Common;

namespace Webfuel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterCoreServices();
            builder.Services.RegisterCommonServices();
            builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Webfuel.BlobStorage>());
            builder.Services.AddScoped<IValidator<CreateWidgetCommand>, CreateWidgetCommandValidator>();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            var app = builder.Build();

            app.UseStaticFiles();

            app.RegisterEndpointsFromAssemblyContaining<Program>();            

            app.Run();
        }
    }
}
