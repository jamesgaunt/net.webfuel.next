using MediatR;
using Webfuel.Domain;
using Webfuel.Domain.Dashboard;

namespace Webfuel.App
{
    [ApiService]
    public static class DashboardApi
    {
        public static void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("api/dashboard-model", GetDashboardModel)
                .RequireIdentity();
        }

        public static Task<DashboardModel> GetDashboardModel(IDashboardService dashboardService)
        {
            return dashboardService.GetDashboardModel();
        }
    }
}
