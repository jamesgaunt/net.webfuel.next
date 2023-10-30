using MediatR;

namespace Webfuel.App
{
    [ApiService]
    public static class PingApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("api/ping", Ping);
        }

        public static PingResponse Ping()
        {
            return new PingResponse { Timestamp = DateTime.Now.ToString() };
        }

        public class PingResponse
        {
            public required string Timestamp { get; set; }
        }
    }
}
