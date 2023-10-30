using Serilog;
using FluentValidation;
using Webfuel.Common;
using Webfuel.Domain;
using Serilog.Events;
using Webfuel.Domain.StaticData;

namespace Webfuel.Api
{
    public class Program
    {
        const string ApiCorsOptions = "ApiCorsOptions";

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

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: ApiCorsOptions,
                        policy =>
                        {
                            policy.WithOrigins(
                                "https://www.webfuel.com",
                                "https://localhost:44426",
                                "https://webfuel-rss-icl.azurewebsites.net");

                            policy.WithHeaders(
                                "content-type",
                                "identity-token");

                            policy.WithMethods("GET", "POST", "PUT", "DELETE");
                        });
                });

                var app = builder.Build();

                app.UseStaticFiles();
                app.UseSerilogRequestLogging();
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseMiddleware<IdentityMiddleware>();
                app.UseMiddleware<StaticDataMiddleware>();

                app.UseCors(ApiCorsOptions);

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
