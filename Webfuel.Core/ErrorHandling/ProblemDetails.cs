using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json.Serialization;

namespace Webfuel
{
    [ApiType]
    [JsonDerivedType(typeof(ValidationProblemDetails))]
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
}
