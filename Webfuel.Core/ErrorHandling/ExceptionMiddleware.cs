using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Webfuel
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (ValidationException exception)
            {
                await exception.ToProblemDetails().ApplyTo(context);
            }
            catch (ApplicationException exception)
            {
                await exception.ToProblemDetails().ApplyTo(context);
            }
            catch (SqlException exception)
            {
                await exception.ToProblemDetails().ApplyTo(context);
            }
            catch (Exception exception) 
            {
                await new ProblemDetails
                {
                    Type = "/internal-server-error",
                    Title = "Interal Server Error: " + exception.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                }
                .ApplyTo(context);
            }
        }
    }
}
