using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class FundingStreamApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/funding-stream", CreateFundingStream)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-stream", UpdateFundingStream)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-stream/sort", SortFundingStream)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/funding-stream/{id:guid}", DeleteFundingStream)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/funding-stream/query", QueryFundingStream)
                .RequireIdentity();
        }
        
        public static Task<FundingStream> CreateFundingStream([FromBody] CreateFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<FundingStream> UpdateFundingStream([FromBody] UpdateFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task SortFundingStream([FromBody] SortFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task DeleteFundingStream(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteFundingStream { Id = id });
        }
        
        public static Task<QueryResult<FundingStream>> QueryFundingStream([FromBody] QueryFundingStream command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

