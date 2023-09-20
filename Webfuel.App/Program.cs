using Webfuel.Domain.Common;

namespace Webfuel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterCoreServices();
            builder.Services.RegisterCommonServices();

            builder.Services.AddMediatR(c => {
                c.RegisterServicesFromAssemblyContaining<CoreAssemblyMarker>();
                c.RegisterServicesFromAssemblyContaining<CommonAssemblyMarker>();
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<IdentityMiddleware>();
            
            app.UseStaticFiles();
            app.UseApiServices<Program>();            
            
            app.Run();
        }
    }
}
