using Webfuel;
using FluentValidation;
using Webfuel.MediatR;
using MediatR;
using Webfuel.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Webfuel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterCoreServices();
            builder.Services.RegisterCommonServices();



            var app = builder.Build();

            app.UseMiddleware<ValidationMiddleware>();

            app.UseStaticFiles();

            app.RegisterApiServicesFromAssemblyContaining<Program>();            

            app.Run();
        }
    }
}
