using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class FundingStreamApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/funding-stream", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-stream", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-stream/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/funding-stream/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/funding-stream/query", Query)
                .RequireIdentity();
        }
        
        public static Task<FundingStream> Create([FromBody] CreateFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<FundingStream> Update([FromBody] UpdateFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteFundingStream { Id = id });
        }
        
        public static Task<QueryResult<FundingStream>> Query([FromBody] QueryFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

