using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class EmailTemplateApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands
            
            app.MapPost("api/emailTemplate", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/emailTemplate", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/emailTemplate/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/emailTemplate/query", Query)
                .RequireIdentity();

            app.MapGet("api/emailTemplate/{id:guid}", Get)
                .RequireIdentity();
        }
        
        public static Task<EmailTemplate> Create([FromBody] CreateEmailTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<EmailTemplate> Update([FromBody] UpdateEmailTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }
     
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteEmailTemplate { Id = id });
        }
        
        public static Task<QueryResult<EmailTemplate>> Query([FromBody] QueryEmailTemplate command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        public static async Task<EmailTemplate?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetEmailTemplate { Id = id });
        }
    }
}

