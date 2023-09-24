namespace Webfuel.App
{
    public class CachingApi
    {
        public static void RegisterCachingProvider(IEndpointRouteBuilder app)
        {
            app.MapPost("api/Caching/Store", (HttpContext context) =>
            {
                var file = context.Request.Form.Files[0];
                var key = context.Request.Form["key"];
                var timestamp = context.Request.Form["timestamp"];

                var ms = new MemoryStream();
                file.CopyTo(ms);
                ms.Position = 0;

            });

            app.MapGet("api/Caching/Retrieve", (HttpContext context) =>
            {
                
            });
        }
    }
}
