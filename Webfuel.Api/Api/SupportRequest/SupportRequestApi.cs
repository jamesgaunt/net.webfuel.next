using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class SupportRequestApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/support-request", Create); // Called from external support request form
                

            app.MapPut("api/support-request", Update)
                .RequireIdentity();

            app.MapPut("api/support-request/researcher", UpdateResearcher)
                .RequireIdentity();

            app.MapPost("api/support-request/triage", Triage)
                .RequireIdentity();

            app.MapDelete("api/support-request/{id:guid}", Delete)
                .RequireIdentity();

            // Querys

            app.MapPost("api/support-request/query", Query)
                .RequireIdentity();

            app.MapGet("api/support-request/{id:guid}", Get)
                .RequireIdentity();
        }

        public static Task<SupportRequest> Create([FromBody] CreateSupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> Update([FromBody] UpdateSupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<SupportRequest> UpdateResearcher([FromBody] UpdateSupportRequestResearcher command, IMediator mediator)
        {
            return mediator.Send(command);
        }


        public static Task<Project?> Triage([FromBody] TriageSupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteSupportRequest { Id = id });
        }

        public static Task<QueryResult<SupportRequest>> Query([FromBody] QuerySupportRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<SupportRequest?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetSupportRequest { Id = id });
        }
    }
}
