﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App
{
    [ApiService]
    [ApiDataSource]
    public static class ReportApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/report", Create)
                .RequireIdentity();

            app.MapPut("api/report", Update)
                .RequireIdentity();

            app.MapDelete("api/report/{id:guid}", Delete)
                .RequireIdentity();

            app.MapPost("api/report/run", Run)
                .RequireIdentity();

            // Querys

            app.MapPost("api/report/query", Query)
                .RequireIdentity();

            app.MapGet("api/report/{id:guid}", Get)
                .RequireIdentity();

            app.MapGet("api/report/list-head", ListHead)
                .RequireIdentity();
        }

        public static Task<Report> Create([FromBody] CreateReport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Report> Update([FromBody] UpdateReport command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteReport { Id = id });
        }

        public static Task<QueryResult<Report>> Query([FromBody] QueryReport command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        public static async Task<ReportStep> Run([FromBody] RunReport command, IMediator mediator)
        {
            return await mediator.Send(command);
        }

        public static async Task<Report?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetReport { Id = id });
        }

        public static async Task<List<Report>> ListHead(IMediator mediator)
        {
            return await mediator.Send(new ListHeadReport());
        }
    }
}
