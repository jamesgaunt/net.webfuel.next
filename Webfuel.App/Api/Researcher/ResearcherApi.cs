using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    public static class ResearcherApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/researcher", CreateResearcher)
                .RequireClaim(c => c.CanEditResearchers);

            app.MapPut("api/researcher", UpdateResearcher)
                .RequireClaim(c => c.CanEditResearchers);

            app.MapDelete("api/researcher/{id:guid}", DeleteResearcher)
                .RequireClaim(c => c.CanEditResearchers);

            // Querys

            app.MapPost("api/researcher/query", QueryResearcher)
                .RequireIdentity();

            app.MapGet("api/researcher/{id:guid}", ResolveResearcher)
                .RequireIdentity();
        }

        public static Task<Researcher> CreateResearcher([FromBody] CreateResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Researcher> UpdateResearcher([FromBody] UpdateResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteResearcher(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearcher { Id = id });
        }

        public static Task<QueryResult<Researcher>> QueryResearcher([FromBody] QueryResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<Researcher> ResolveResearcher(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetResearcher { Id = id }) ?? throw new InvalidOperationException("The specified researcher does not exist");
        }
    }
}
