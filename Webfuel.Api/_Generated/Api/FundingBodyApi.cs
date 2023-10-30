using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class FundingBodyApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/funding-body", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-body", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/funding-body/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/funding-body/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/funding-body/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<FundingBody> Create([FromBody] CreateFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<FundingBody> Update([FromBody] UpdateFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteFundingBody { Id = id });
        }
        
        public static Task<QueryResult<FundingBody>> Query([FromBody] QueryFundingBody command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

