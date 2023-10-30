using MediatR;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    public static class ConfigurationApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("api/configuration", GetConfiguration)
                .RequireIdentity();
        }

        public static Task<ClientConfiguration> GetConfiguration(IMediator mediator)
        {
            return mediator.Send(new GetClientConfiguration());
        }
    }
}
