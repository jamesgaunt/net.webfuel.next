using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class HeartbeatApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/heartbeat", Create)
                .RequireIdentity();

            app.MapPut("api/heartbeat", Update)
                .RequireIdentity();

            app.MapDelete("api/heartbeat/{id:guid}", Delete)
                .RequireIdentity();

            app.MapPost("api/heartbeat/execute/{id:guid}", Execute)
                .RequireIdentity();

            // Querys

            app.MapPost("api/heartbeat/query", Query)
                .RequireIdentity();

            app.MapGet("api/heartbeat/{id:guid}", Get)
                .RequireIdentity();
        }

        public static Task<Heartbeat> Create([FromBody] CreateHeartbeat command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Heartbeat> Update([FromBody] UpdateHeartbeat command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteHeartbeat { Id = id });
        }

        public static Task Execute(Guid id, IHeartbeatService heartbeatService)
        {
            return heartbeatService.ExecuteHeartbeat(id);
        }

        public static Task<QueryResult<Heartbeat>> Query([FromBody] QueryHeartbeat command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<Heartbeat?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetHeartbeat { Id = id });
        }
    }
}
