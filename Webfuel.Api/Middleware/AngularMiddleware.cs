using System.Linq;

namespace Webfuel.Api
{
    public class AngularMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly IWebHostEnvironment _env;

        public AngularMiddleware(RequestDelegate request, IWebHostEnvironment env)
        {
            _request = request;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString() ?? String.Empty;
            if (path == "/")
                path = "/index.html";

            if (await WriteAngularResponse(context, path))
                return;

            await _request(context);

            // If we have a missing response, try returning the index page
            if(context.Response.StatusCode == 404)
            {
                if (await WriteAngularResponse(context, "/index.html"))
                    return;
            }
        }

        async Task<bool> WriteAngularResponse(HttpContext context, string path)
        {

            if (path.Count(p => p == '/') == 1)
            {


                if (path.EndsWith(".html") || path.EndsWith(".js") || path.EndsWith(".svg") || path.EndsWith(".eot") || path.EndsWith(".ttf") || path.EndsWith(".woff") || path.EndsWith(".woff2") || path.EndsWith(".css"))
                {
                    var fileInfo = _env.WebRootFileProvider.GetFileInfo("dist" + path);
                    if (fileInfo.Exists)
                    {
                        context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
                        context.Response.Headers.Append("Expires", "-1");

                        context.Response.StatusCode = 200;
                        context.Response.ContentType = MimeTypes.GetMimeType(fileInfo.Name);
                        context.Response.ContentLength = fileInfo.Length;

                        using (var stream = fileInfo.CreateReadStream())
                        {
                            await stream.CopyToAsync(context.Response.Body);
                        }
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
