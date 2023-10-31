using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class AgeRangeApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/age-range", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/age-range", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/age-range/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/age-range/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/age-range/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<AgeRange> Create([FromBody] CreateAgeRange command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<AgeRange> Update([FromBody] UpdateAgeRange command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortAgeRange command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteAgeRange { Id = id });
        }
        
        public static Task<QueryResult<AgeRange>> Query([FromBody] QueryAgeRange command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

