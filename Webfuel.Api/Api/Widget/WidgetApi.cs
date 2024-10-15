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

            // Querys

            app.MapPost("api/widget/select", Select)
                .RequireIdentity();

            app.MapGet("api/widget/select-available-type", SelectAvailableType)
                .RequireIdentity();

            app.MapGet("api/widget/{id:guid}", Get)
                .RequireIdentity();

            app.MapPost("api/widget/generate/{widgetId:guid}", Generate)
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

        public static Task<List<Widget>> Select([FromBody] SelectWidget command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<List<WidgetType>> SelectAvailableType(IMediator mediator)
        {
            return mediator.Send(new SelectAvailableWidgetType());
        }

        public static Task<WidgetDataResponse> Generate(Guid widgetId, IWidgetDataService service)
        {
            return service.GenerateData(widgetId);
        }

        public static async Task<Widget?> Get(Guid id, IMediator mediator)
        {
            return await mediator.Send(new GetWidget { Id = id }) ;
        }
    }
}
