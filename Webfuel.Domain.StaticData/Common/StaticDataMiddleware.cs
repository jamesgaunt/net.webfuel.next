using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.StaticData
{
    public class StaticDataMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly IStaticDataService _staticDataService;

        public StaticDataMiddleware(RequestDelegate request, IStaticDataService staticDataService)
        {
            _request = request;
            _staticDataService = staticDataService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // We push the timestamp of the currently loaded static data onto each response so the client knows when it needs to reload the data
            var staticData = await _staticDataService.GetStaticData();
            context.Response.Headers.Add("Static-Data-Timestamp", staticData.LoadedAt.ToString());

            await _request(context);
        }
    }
}
