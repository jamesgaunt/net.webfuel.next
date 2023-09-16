using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace Webfuel
{
    public interface ITypefuelApi
    {
        void RegisterEndpoints(IEndpointRouteBuilder app);
    }

    public static class TypefuelApiExtensions
    {
        public static void RegisterEndpointsFromAssemblyContaining<TMarker>(this IEndpointRouteBuilder app)
        {
            foreach (var type in typeof(TMarker).Assembly.DefinedTypes)
            {
                if (!type.ImplementedInterfaces.Contains(typeof(ITypefuelApi)))
                    continue;

                var instance = Activator.CreateInstance(type) as ITypefuelApi;
                if (instance == null)
                    continue;

                instance.RegisterEndpoints(app);
            }
        }

        public static void MapCommand<TCommand>(this IEndpointRouteBuilder app, string? pattern = null) where TCommand : class
        {
            pattern = BuildPattern<TCommand>(pattern);

            if (app is TypefuelApiExtractor)
            {
                // Typefuel is running
                ((TypefuelApiExtractor)app).ExtractCommand<TCommand>(pattern);
            }
            else
            {
                // Server is running
                app.MapPost(pattern, async ([FromBody] TCommand command, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var result = await mediator.Send(command, cancellationToken);
                        return Results.Ok(result);
                    }
                    catch (Exception ex)
                    {
                        return Results.BadRequest(ex);
                    }
                });
            }
        }

        static string BuildPattern<TCommand>(string? pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                pattern = typeof(TCommand).Name.Replace("Command", "");
            }
            return "api/" + pattern;
        }
    }

    public class TypefuelApiExtractor : IEndpointRouteBuilder
    {
        public void ExtractCommand<TCommand>(string pattern)
        {
            Commands.Add(new TypefuelApiCommand
            {
                CommandType = typeof(TCommand),
                Pattern = pattern
            });
        }

        public List<TypefuelApiCommand> Commands { get; } = new List<TypefuelApiCommand>();

        public IServiceProvider ServiceProvider => throw new NotImplementedException();

        public ICollection<EndpointDataSource> DataSources => throw new NotImplementedException();

        public IApplicationBuilder CreateApplicationBuilder()
        {
            throw new NotImplementedException();
        }
    }

    public class TypefuelApiCommand
    {
        public Type CommandType { get; set; } = typeof(void);

        public string Pattern { get; set; } = string.Empty;
    }
}
