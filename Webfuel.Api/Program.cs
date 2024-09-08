using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;
using Webfuel.Logging;
using Webfuel.Reporting;

namespace Webfuel.Api;

public class Program
{
    const string ApiCorsOptions = "ApiCorsOptions";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddLogging();
        builder.AddTracing();

        builder.Services.RegisterCoreServices();
        builder.Services.RegisterCommonServices();
        builder.Services.RegisterDomainServices();
        builder.Services.RegisterStaticDataServices();
        builder.Services.RegisterExcelServices();
        builder.Services.RegisterReportingServices();

        builder.AddPlatformClientServices(x =>
        {
            x.ClientId = Guid.Parse("2643cb0a-1ac2-b74b-4c69-08dccf4965da");
            x.AccessToken = "ABCD";
        });

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

        app.UseShortCircuits();
        app.UseRequestTrace();

        app.UseMiddleware<IdentityMiddleware>();
        app.UseMiddleware<AngularMiddleware>();
        app.UseStaticFiles();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<StaticDataMiddleware>();

        app.UseCors(ApiCorsOptions);

        app.UseApiServices<Program>();

        app.Run();
    }
}

public static class ServiceDefaults
{
    public static IHostApplicationBuilder AddLogging(this IHostApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddConsole();
        }
        else
        {
            builder.Logging.AddPlatformLogger();

            builder.Logging.AddOpenTelemetry(x =>
            {
                x.IncludeScopes = true;
                x.IncludeFormattedMessage = true;
            });

            builder.Services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("https://api.eu1.honeycomb.io/v1/logs");
                opt.Headers = "x-honeycomb-team=fsZ4cF3kvdv0D02QDX7mbH";
                opt.Protocol = OtlpExportProtocol.HttpProtobuf;
            }));
        }

        return builder;
    }

    public static IHostApplicationBuilder AddTracing(this IHostApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            // No tracing in development by default
        }
        else
        {
            builder.Services
                .AddOpenTelemetry()
                .ConfigureResource(x =>
                {
                    x.Clear();
                    x.AddService(builder.Environment.IsProduction() ? "rss-icl" : "rss-icl-dev");
                })
                .WithTracing(x =>
                {
                    x.SetSampler(new AlwaysOnSampler());
                    x.AddSource(RequestTraceActivitySource.Name);
                });

            builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("https://api.eu1.honeycomb.io/v1/traces");
                opt.Headers = "x-honeycomb-team=fsZ4cF3kvdv0D02QDX7mbH";
                opt.Protocol = OtlpExportProtocol.HttpProtobuf;
            }));

        }

        return builder;
    }

    public static void UseShortCircuits(this WebApplication app)
    {
        app.MapGet("/favicon.ico", () => Task.CompletedTask).ShortCircuit(404);
        app.MapGet("/robots.txt", () => """
            User-agent: *
            Disallow: /
            """).ShortCircuit(200);

        app.MapShortCircuit(404, ".well-known");
    }
}