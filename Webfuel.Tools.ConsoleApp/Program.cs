using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Tools.ConsoleApp;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = RegisterServices();
        while (await MenuLoop(serviceProvider)) ;
    }

    private static IConfiguration Configuration()
    {
        return new ConfigurationBuilder()
            .AddUserSecrets("b61c8db6-ad36-4617-a0c7-eee4e6441a20")
            .Build();
    }

    private static ServiceProvider RegisterServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton(typeof(IConfiguration), Configuration());
        services.AddSingleton<IHttpContextAccessor, ConsoleHttpContextAccessor>();

        services.RegisterCoreServices();
        services.RegisterCommonServices();
        services.RegisterDomainServices();
        services.RegisterStaticDataServices();

        AnnotatedServiceRegistration.RegisterServicesFromAssembly(services, typeof(Program).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }

    private static async Task<bool> MenuLoop(IServiceProvider serviceProvider)
    {
        Console.Clear();

        Console.WriteLine("Commands:");
        Console.WriteLine("-- fix users");
        Console.WriteLine("-- fix projects");
        Console.WriteLine("-- fix project supports");
        Console.WriteLine("-- fix project submissions");
        Console.WriteLine("-- fix support requests");
        Console.WriteLine("-- migrate support");
        Console.WriteLine("-- exit");

        Console.Write("> ");
        var command = Console.ReadLine()?.Trim();

        if (command == "exit")
            return false;

        switch (command)
        {
            case "fix users":
                await serviceProvider.GetRequiredService<IUserFix>().FixUsers();
                break;

            case "fix projects":
                await serviceProvider.GetRequiredService<IProjectFix>().FixProjects();
                break;

            case "fix project submissions":
                await serviceProvider.GetRequiredService<IProjectSubmissionFix>().FixProjectSubmissions();
                break;


            case "fix project supports":
                await serviceProvider.GetRequiredService<IProjectSupportFix>().FixProjectSupports();
                break;

            case "fix support requests":
                await serviceProvider.GetRequiredService<ISupportRequestFix>().FixSupportRequests();
                break;

            case "migrate support":
                await serviceProvider.GetRequiredService<ISupportMigration>().Migrate();
                break;

            default:
                Console.WriteLine("Unrecognised command");
                break;
        }

        Console.WriteLine("Command finished. Press any key.");
        Console.ReadKey();

        return true;
    }
}

public class ConsoleHttpContextAccessor : IHttpContextAccessor
{
    public HttpContext? HttpContext
    {
        get; set;
    }
}