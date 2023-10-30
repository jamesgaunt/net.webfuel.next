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

        public static string Ping()
        {
            return DateTime.Now.ToString();
        }
    }
}
