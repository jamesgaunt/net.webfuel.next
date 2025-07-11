﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Reporting;

namespace Webfuel.App;

[ApiService]
[ApiDataSource]
public static class ProjectApi
{
    public static void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        // Commands

        app.MapPut("api/project", Update)
            .RequireIdentity();

        app.MapPut("api/project/request", UpdateRequest)
            .RequireIdentity();

        app.MapPut("api/project/researcher", UpdateResearcher)
            .RequireIdentity();

        app.MapPut("api/project/support-settings", UpdateSupportSettings)
            .RequireIdentity();

        app.MapPut("api/project/unlock", Unlock)
             .RequireClaim(p => p.CanUnlockProjects);

        app.MapDelete("api/project/{id:guid}", Delete)
            .RequireIdentity();

        app.MapPost("api/project:create-test", CreateTest)
            .RequireClaim(p => p.Developer);

        app.MapPost("api/project:enrich", Enrich)
            .RequireClaim(p => p.Developer);

        // Querys

        app.MapPost("api/project/query", Query)
            .RequireIdentity();

        app.MapGet("api/project/{id:guid}", Get)
            .RequireIdentity();

        app.MapPut("api/project/export", Export)
            .RequireIdentity();
    }

    public static Task<Project> Update([FromBody] UpdateProject command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task<Project> UpdateRequest([FromBody] UpdateProjectRequest command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task<Project> UpdateResearcher([FromBody] UpdateProjectResearcher command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task<Project> UpdateSupportSettings([FromBody] UpdateProjectSupportSettings command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task<Project> Unlock([FromBody] UnlockProject command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task Delete(Guid id, IMediator mediator)
    {
        return mediator.Send(new DeleteProject { Id = id });
    }

    public static Task<Project> CreateTest([FromBody] CreateTestProject command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task Enrich([FromBody] EnrichProject command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static Task<QueryResult<Project>> Query([FromBody] QueryProject command, IMediator mediator)
    {
        return mediator.Send(command);
    }

    public static async Task<Project?> Get(Guid id, IMediator mediator)
    {
        return await mediator.Send(new GetProject { Id = id });
    }

    public static Task<ReportStep> Export([FromBody] QueryProject request, IExportProjectService service)
    {
        return service.InitialiseReport(request);
    }
}
