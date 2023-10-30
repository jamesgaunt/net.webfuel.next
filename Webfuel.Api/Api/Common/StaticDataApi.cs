using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.App
{
    [ApiService]
    public static class StaticDataApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            // Queries

            app.MapGet("api/static-data", GetStaticData)
                .RequireIdentity();
        }

        public static Task<IStaticDataModel> GetStaticData(IStaticDataService staticDataService)
        {
            return staticDataService.GetStaticData();
        }
    }
}
