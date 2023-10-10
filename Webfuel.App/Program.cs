using Serilog;
using FluentValidation;
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

                builder.Host.UseSerilog();

                builder.Services.RegisterCoreServices();
                builder.Services.RegisterDomainServices();
                builder.Services.RegisterStaticDataServices();

                builder.Services.AddMediatR(c =>
                {
                    c.RegisterServicesFromAssemblyContaining<CoreAssemblyMarker>();
                    c.RegisterServicesFromAssemblyContaining<DomainAssemblyMarker>();
                    c.RegisterServicesFromAssemblyContaining<StaticDataAssemblyMarker>();
                });

                var app = builder.Build();

                app.UseSerilogRequestLogging();

                app.UseMiddleware<ExceptionMiddleware>();
                app.UseMiddleware<IdentityMiddleware>();
                app.UseMiddleware<StaticDataMiddleware>();

                app.UseStaticFiles();

                app.UseApiServices<Program>();

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
