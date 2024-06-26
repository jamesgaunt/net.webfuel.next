﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class UserApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/user", Create)
                .RequireClaim(c => c.CanEditUsers);

            app.MapPut("api/user", Update)
                .RequireClaim(c => c.CanEditUsers);

            app.MapDelete("api/user/{id:guid}", Delete)
                .RequireClaim(c => c.CanEditUsers);

            // Querys

            app.MapPost("api/user/query", Query)
                .RequireIdentity();

            app.MapGet("api/user/{id:guid}", Get)
                .RequireIdentity();

            app.MapPut("api/user/export", Export)
                    .RequireIdentity();
        }

        public static Task<User> Create([FromBody] CreateUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<User> Update([FromBody] UpdateUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteUser { Id = id });
        }

        public static Task<QueryResult<User>> Query([FromBody] QueryUser command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static async Task<User?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetUser { Id = id });
        }

        public static Task<ReportStep> Export([FromBody] QueryUser request, IExportUserService service)
        {
            return service.InitialiseReport(request);
        }
    }
}
