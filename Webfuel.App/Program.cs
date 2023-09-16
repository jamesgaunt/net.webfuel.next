using Webfuel;
using FluentValidation;
using Webfuel.MediatR;
using MediatR;

namespace Webfuel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Webfuel.CoreRegistration.ConfigureServices(builder.Services);
            Webfuel.Common.CommonRegistration.ConfigureServices(builder.Services);

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
