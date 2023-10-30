using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class UserDisciplineApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/user-discipline", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/user-discipline", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/user-discipline/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/user-discipline/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/user-discipline/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<UserDiscipline> Create([FromBody] CreateUserDiscipline command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<UserDiscipline> Update([FromBody] UpdateUserDiscipline command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortUserDiscipline command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUserDiscipline { Id = id });
        }
        
        public static Task<QueryResult<UserDiscipline>> Query([FromBody] QueryUserDiscipline command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

