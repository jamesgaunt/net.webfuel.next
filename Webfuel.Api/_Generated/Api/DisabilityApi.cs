using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class DisabilityApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/disability", Create)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/disability", Update)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/disability/sort", Sort)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapDelete("api/disability/{id:guid}", Delete)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/disability/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<Disability> Create([FromBody] CreateDisability command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Disability> Update([FromBody] UpdateDisability command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortDisability command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteDisability { Id = id });
        }
        
        public static Task<QueryResult<Disability>> Query([FromBody] QueryDisability command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

