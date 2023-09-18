using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Webfuel
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _request;

        public ValidationMiddleware(RequestDelegate request)
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
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new ValidationError(exception));
            }
        }
    }
}
