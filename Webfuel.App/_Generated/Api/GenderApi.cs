using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class GenderApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/gender", CreateGender)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/gender", UpdateGender)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/gender/sort", SortGender)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/gender/{id:guid}", DeleteGender)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/gender/query", QueryGender)
                .RequireIdentity();
        }
        
        public static Task<Gender> CreateGender([FromBody] CreateGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Gender> UpdateGender([FromBody] UpdateGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task SortGender([FromBody] SortGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task DeleteGender(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteGender { Id = id });
        }
        
        public static Task<QueryResult<Gender>> QueryGender([FromBody] QueryGender command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

