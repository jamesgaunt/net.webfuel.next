using Serilog;
using FluentValidation;
using Webfuel.Common;
using Webfuel.Domain;
using Serilog.Events;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
               .CreateLogger();

            try
            {
                Log.Information("Starting web application");

                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddRazorPages()
                    .AddRazorRuntimeCompilation(); // Development only!

                builder.Host.UseSerilog();

                builder.Services.RegisterCoreServices();
                builder.Services.RegisterCommonServices();
                builder.Services.RegisterDomainServices();
                builder.Services.RegisterStaticDataServices();

                builder.Services.AddMediatR(c =>
                {
                    c.RegisterServicesFromAssemblyContaining<CoreAssemblyMarker>();
                    c.RegisterServicesFromAssemblyContaining<DomainAssemblyMarker>();
                    c.RegisterServicesFromAssemblyContaining<CommonAssemblyMarker>();
                    c.RegisterServicesFromAssemblyContaining<StaticDataAssemblyMarker>();
                });

                var app = builder.Build();

                app.UseStaticFiles();

                app.UseSerilogRequestLogging();
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseMiddleware<IdentityMiddleware>();
                app.UseMiddleware<StaticDataMiddleware>();
                app.UseApiServices<Program>();

                app.MapRazorPages();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
