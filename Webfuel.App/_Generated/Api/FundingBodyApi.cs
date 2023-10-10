using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class FundingBodyApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/funding-body", CreateFundingBody)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-body", UpdateFundingBody)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-body/sort", SortFundingBody)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/funding-body/{id:guid}", DeleteFundingBody)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/funding-body/query", QueryFundingBody)
                .RequireIdentity();
        }
        
        public static Task<FundingBody> CreateFundingBody([FromBody] CreateFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<FundingBody> UpdateFundingBody([FromBody] UpdateFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task SortFundingBody([FromBody] SortFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task DeleteFundingBody(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteFundingBody { Id = id });
        }
        
        public static Task<QueryResult<FundingBody>> QueryFundingBody([FromBody] QueryFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

