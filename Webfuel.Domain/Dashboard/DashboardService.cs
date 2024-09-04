using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardModel> GetDashboardModel();
    }

    [Service(typeof(IDashboardService))]
    internal class DashboardService : IDashboardService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProjectRepository _projectRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public DashboardService(
            IStaticDataService staticDataService,
            IProjectRepository projectRepository,
            IIdentityAccessor identityAccessor)
        {
            _staticDataService = staticDataService;
            _projectRepository = projectRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<DashboardModel> GetDashboardModel()
        {
            return new DashboardModel
            {
                SupportMetrics = await GenerateSupportMetrics(),
                ProjectMetrics = await GenerateProjectMetrics(),
            };
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Project Metrics

        async Task<List<DashboardMetric>> GenerateProjectMetrics()
        {
            var result = _projectMetrics;
            if (result != null)
                return result;

            result = new List<DashboardMetric>();

            { 
                var query = new Query();
                query.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
                var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

                result.Add(new DashboardMetric
                {
                    Name = "Active Projects",
                    Count = queryResult.TotalCount,
                    Icon = "fas fa-books",
                    RouterLink = "/project/project-list",
                    RouterParams = $"{{ \"show\": \"active\" }}",
                    BackgroundColor = "#d6bdcc"
                });
            }

            {
                var query = new Query();
                query.Equal(nameof(Project.StatusId), ProjectStatusEnum.OnHold);
                var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

                result.Add(new DashboardMetric
                {
                    Name = "On Hold Projects",
                    Count = queryResult.TotalCount,
                    Icon = "fas fa-books",
                    RouterLink = "/project/project-list",
                    RouterParams = $"{{ \"show\": \"on-hold\" }}",
                    BackgroundColor = "#d6bdcc"
                });
            }

            // All Projects
            {
                result.Add(new DashboardMetric
                {
                    Name = "All Projects",
                    Count = await _projectRepository.CountProject(),
                    Icon = "fas fa-books",
                    RouterLink = "/project/project-list",
                    RouterParams = $"{{ \"show\": \"all\" }}",
                    BackgroundColor = "#d6bdcc"

                });
            }

            return _projectMetrics = result;
        }

        public static void FlushProjectMetrics()
        {
            _projectMetrics = null;
        }

        static List<DashboardMetric>? _projectMetrics = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Support Team Metrics

        async Task<List<DashboardMetric>> GenerateSupportMetrics()
        {
            var result = _supportMetrics;
            if (result != null)
                return result;

            result = new List<DashboardMetric>();
            var staticData = await _staticDataService.GetStaticData();
            foreach (var supportTeam in staticData.SupportTeam)
            {
                result.Add(await GenerateSupportTeamMetric(supportTeam));
            }

            return _supportMetrics = result;
        }

        async Task<DashboardMetric> GenerateSupportTeamMetric(SupportTeam supportTeam)
        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
            query.SQL($"EXISTS (SELECT Id FROM [ProjectSupport] AS ps WHERE ps.[ProjectId] = e.Id AND ps.[SupportRequestedTeamId] = '{supportTeam.Id}' AND ps.[SupportRequestedCompletedAt] IS NULL)");
            var queryResult = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            return new DashboardMetric
            {
                Name = supportTeam.Name,
                Count = queryResult.TotalCount,
                Icon = "fas fa-user-headset",
                RouterLink = "/project/project-list",
                RouterParams = $"{{ \"supportTeam\": \"{supportTeam.Id}\" }}",
                BackgroundColor = "#d6bdcc"
            };
        }

        public static void FlushSupportMetrics()
        {
            _supportMetrics = null;
        }

        static List<DashboardMetric>? _supportMetrics = null;
    }
}
