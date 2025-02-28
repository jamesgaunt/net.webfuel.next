using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Reporting;
using Webfuel.ServiceDefaults;

namespace Webfuel.Api;

public class Program
{
    const string ApiCorsOptions = "ApiCorsOptions";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddLoggingServices();
        builder.AddTracingServices("rss-icl");

        builder.Services.RegisterCoreServices();
        builder.Services.RegisterCommonServices();
        builder.Services.RegisterDomainServices();
        builder.Services.RegisterStaticDataServices();
        builder.Services.RegisterExcelServices();
        builder.Services.RegisterReportingServices();

        builder.AddTerminal();
        builder.AddClientServices(x =>
        {
            x.ClientId = Guid.Parse("b03435ab-2375-c840-7415-08dd57f0f2b7");
            x.AccessToken = "ABCD";
        });

        if (builder.Environment.IsProduction())
        {
            builder.AddBackgroundJob<IProjectEnrichmentService>();
        }

        builder.Services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining<CoreAssemblyMarker>();
            c.RegisterServicesFromAssemblyContaining<DomainAssemblyMarker>();
            c.RegisterServicesFromAssemblyContaining<CommonAssemblyMarker>();
            c.RegisterServicesFromAssemblyContaining<StaticDataAssemblyMarker>();
            c.RegisterServicesFromAssemblyContaining<ReportingAssemblyMarker>();
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: ApiCorsOptions,
                policy =>
                {
                    policy.WithOrigins(
                        "https://www.webfuel.com",

                        "http://localhost:4200",
                        "https://localhost:44426",
                        "https://localhost:7076",

                        "https://db.rssimperialpartners.org.uk");

                    policy.WithHeaders(
                        "content-type",
                        "identity-token");

                    policy.WithMethods("GET", "POST", "PUT", "DELETE");
                });
        });

        var app = builder.Build();

        app.UseStandardShortCircuits();
        app.UseRequestTrace();

        app.UseCors(ApiCorsOptions);

        app.UseTerminal();
        app.UseMiddleware<IdentityMiddleware>();
        app.UseMiddleware<AngularMiddleware>();
        app.UseStaticFiles();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<StaticDataMiddleware>();

        app.UseApiServices<Program>();

        app.Run();
    }
}
