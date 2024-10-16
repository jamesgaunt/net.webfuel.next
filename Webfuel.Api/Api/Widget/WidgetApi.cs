using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class WidgetApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands

            app.MapPost("api/widget", Create)
                .RequireIdentity();

            app.MapPut("api/widget", Update)
                  .RequireIdentity();

            app.MapPut("api/widget/sort", Sort)
                .RequireIdentity();

            app.MapDelete("api/widget/{id:guid}", Delete)
                .RequireIdentity();

            app.MapPost("api/widget/refresh/{id:guid}", Refresh)
                  .RequireIdentity();

            // Querys

            app.MapGet("api/widget/select-active", SelectActive)
                .RequireIdentity();

            app.MapGet("api/widget/select-available-type", SelectAvailableType)
                .RequireIdentity();
        }

        public static Task<Widget> Create([FromBody] CreateWidget command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Widget> Update([FromBody] UpdateWidget command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Sort([FromBody] SortWidget command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task Delete(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteWidget { Id = id });
        }

        public static Task<RefreshWidgetResult> Refresh(Guid id, IMediator mediator)
        {
            return mediator.Send(new RefreshWidget { Id = id });
        }

        public static Task<List<Widget>> SelectActive(IMediator mediator)
        {
            return mediator.Send(new SelectActiveWidget());
        }

        public static Task<List<WidgetType>> SelectAvailableType(IMediator mediator)
        {
            return mediator.Send(new SelectAvailableWidgetType());
        }
    }
}
