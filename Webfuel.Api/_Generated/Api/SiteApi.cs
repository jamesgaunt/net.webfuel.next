using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class SiteApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/site", Create)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/site", Update)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapPut("api/site/sort", Sort)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            app.MapDelete("api/site/{id:guid}", Delete)
                .RequireClaim(c => c.UserGroupClaims.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/site/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<Site> Create([FromBody] CreateSite command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<Site> Update([FromBody] UpdateSite command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortSite command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteSite { Id = id });
        }
        
        public static Task<QueryResult<Site>> Query([FromBody] QuerySite command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

