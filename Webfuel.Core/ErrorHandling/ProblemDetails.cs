using Microsoft.AspNetCore.Http;
using System.Net;

namespace Webfuel
{
    [ApiType]
    public class ProblemDetails
    {
        public string Type { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public string Detail { get; set; } = String.Empty;

        public int Status { get; set; } = (int)HttpStatusCode.InternalServerError;

        public async Task ApplyTo(HttpContext context)
        {
            context.Response.StatusCode = Status;
            await context.Response.WriteAsJsonAsync(this);
        }
    }

    [ApiType]
    public class ValidationProblemDetails: ProblemDetails
    {
        public List<ValidationError> ValidationErrors { get; } = new List<ValidationError>();

        public class ValidationError
        {
            public string Property { get; set; } = String.Empty;

            public string Message { get; set; } = String.Empty;
        }

        public void AddValidationError(string property, string message)
        {
            ValidationErrors.Add(new ValidationError {  Property = property, Message = message });
        }
    }
}
