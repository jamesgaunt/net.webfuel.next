using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ResearcherApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/researcher", Create)
                .RequireClaim(c => c.CanEditResearchers);

            app.MapPut("api/researcher", Update)
                .RequireClaim(c => c.CanEditResearchers);

            app.MapDelete("api/researcher/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditResearchers);

            // Querys

            app.MapPost("api/researcher/query", Query)
                .RequireIdentity();

            app.MapGet("api/researcher/{id:guid}", Resolve)
                .RequireIdentity();
        }

        public static Task<Researcher> Create([FromBody] CreateResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Researcher> Update([FromBody] UpdateResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteResearcher { Id = id });
        }

        public static Task<QueryResult<Researcher>> Query([FromBody] QueryResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<Researcher> Resolve(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetResearcher { Id = id }) ?? throw new InvalidOperationException("The specified researcher does not exist");
        }
    }
}
