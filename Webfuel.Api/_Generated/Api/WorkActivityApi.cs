using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class WorkActivityApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/work-activity", Create)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/work-activity", Update)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/work-activity/sort", Sort)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapDelete("api/work-activity/{id:guid}", Delete)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/work-activity/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<WorkActivity> Create([FromBody] CreateWorkActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<WorkActivity> Update([FromBody] UpdateWorkActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortWorkActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteWorkActivity { Id = id });
        }
        
        public static Task<QueryResult<WorkActivity>> Query([FromBody] QueryWorkActivity command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

