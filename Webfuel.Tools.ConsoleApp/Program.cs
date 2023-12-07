using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Runtime.CompilerServices;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Tools.ConsoleApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = RegisterServices();
            while (await MenuLoop(serviceProvider));
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

            ServiceRegistration.RegisterServicesFromAssembly(services, typeof(Program).Assembly);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static async Task<bool> MenuLoop(IServiceProvider serviceProvider)
        {
            Console.Clear();

            Console.WriteLine("Commands:");
            Console.WriteLine("-- user test data");
            Console.WriteLine("-- exit");

            Console.Write("> ");
            var command = Console.ReadLine()?.Trim();

            if (command == "exit")
                return false;

            switch (command)
            {
                case "user test data":
                    await serviceProvider.GetRequiredService<IUserTestData>().GenerateTestData();
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
}