using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class ApplicationStageApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/application-stage", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/application-stage", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/application-stage/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/application-stage/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/application-stage/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<ApplicationStage> Create([FromBody] CreateApplicationStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<ApplicationStage> Update([FromBody] UpdateApplicationStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortApplicationStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteApplicationStage { Id = id });
        }
        
        public static Task<QueryResult<ApplicationStage>> Query([FromBody] QueryApplicationStage command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

