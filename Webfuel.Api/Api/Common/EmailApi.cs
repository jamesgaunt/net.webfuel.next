using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Common;

namespace Webfuel.App
{
    [ApiService]
    public static class EmailApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Commands
            
            app.MapPost("api/email/send", Send)
                .RequireIdentity();
        }
        
        public static Task Send([FromBody] SendEmailRequest command, IMediator mediator)
        {
            return mediator.Send(command);
        }
        
    }
}

