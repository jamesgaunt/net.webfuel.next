using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class FundingCallTypeApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/funding-call-type", Create)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/funding-call-type", Update)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/funding-call-type/sort", Sort)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapDelete("api/funding-call-type/{id:guid}", Delete)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/funding-call-type/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<FundingCallType> Create([FromBody] CreateFundingCallType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<FundingCallType> Update([FromBody] UpdateFundingCallType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortFundingCallType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteFundingCallType { Id = id });
        }
        
        public static Task<QueryResult<FundingCallType>> Query([FromBody] QueryFundingCallType command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

