using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class TriageTemplateApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands
            
            app.MapPost("api/triage-template", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/triage-template", Update)
                .RequireClaim(c => c.CanEditStaticData);

            app.MapPut("api/triage-template/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);

            app.MapDelete("api/triage-template/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);

            app.MapPut("api/triage-template/generate-email", GenerateEmail)
                .RequireIdentity();

            // Querys

            app.MapPost("api/triage-template/query", Query)
                .RequireIdentity();

            app.MapGet("api/triage-template/{id:guid}", Get)
                .RequireIdentity();
        }
        
        public static Task<TriageTemplate> Create([FromBody] CreateTriageTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<TriageTemplate> Update([FromBody] UpdateTriageTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Sort([FromBody] SortTriageTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteTriageTemplate { Id = id });
        }

        public static Task<SendEmailRequest> GenerateEmail([FromBody] GenerateTriageTemplateEmail command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<QueryResult<TriageTemplate>> Query([FromBody] QueryTriageTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<TriageTemplate?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetTriageTemplate { Id = id });
        }
    }
}

