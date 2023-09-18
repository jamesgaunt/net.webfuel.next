using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading;

namespace Webfuel.App
{
    [ApiService]
    public static class WidgetApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("api/create-widget", CreateWidget);

            app.MapPut("api/update-widget", UpdateWidget);

            app.MapDelete("api/delete-widget/{id:guid}", DeleteWidget);

            app.MapPost("api/query-widget", QueryWidget);
        }

        public static Task<Widget> CreateWidget([FromBody] CreateWidgetCommand command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task<Widget> UpdateWidget([FromBody] UpdateWidgetCommand command, IMediator mediator)
        {
            return mediator.Send(command);
        }

        public static Task DeleteWidget(Guid id, IMediator mediator)
        {
            return mediator.Send(new DeleteWidgetCommand { Id = id });
        }

        public static Task<QueryResult<Widget>> QueryWidget([FromBody] QueryWidgetCommand command, IMediator mediator)
        {
            return mediator.Send(command);
        }
    }
}
