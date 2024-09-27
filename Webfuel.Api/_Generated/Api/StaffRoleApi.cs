using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    [ApiStaticData]
    public static class StaffRoleApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            
            // Commands
            
            app.MapPost("api/staff-role", Create)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/staff-role", Update)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapPut("api/staff-role/sort", Sort)
                .RequireClaim(c => c.CanEditStaticData);
            
            app.MapDelete("api/staff-role/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditStaticData);
            
            // Querys
            
            app.MapPost("api/staff-role/query", Query)
                .RequireIdentity();
            
            
        }
        
        public static Task<StaffRole> Create([FromBody] CreateStaffRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task<StaffRole> Update([FromBody] UpdateStaffRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Sort([FromBody] SortStaffRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteStaffRole { Id = id });
        }
        
        public static Task<QueryResult<StaffRole>> Query([FromBody] QueryStaffRole command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}

