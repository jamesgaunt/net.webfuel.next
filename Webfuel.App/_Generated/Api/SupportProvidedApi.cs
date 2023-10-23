using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SupportProvidedApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/support-provided", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/support-provided", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/support-provided/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/support-provided/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/support-provided/query", Query)
                .RequireIdentity();
        }
        
        public static Task<SupportProvided> Create([FromBody] CreateSupportProvided command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<SupportProvided> Update([FromBody] UpdateSupportProvided command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortSupportProvided command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteSupportProvided { Id = id });
        }
        
        public static Task<QueryResult<SupportProvided>> Query([FromBody] QuerySupportProvided command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

