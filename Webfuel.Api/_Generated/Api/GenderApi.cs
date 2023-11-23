using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class GenderApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/gender", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/gender", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/gender/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/gender/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/gender/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<Gender> Create([FromBody] CreateGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Gender> Update([FromBody] UpdateGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteGender { Id = id });
        }
        
        public static Task<QueryResult<Gender>> Query([FromBody] QueryGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

