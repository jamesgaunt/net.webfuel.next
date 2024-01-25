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
    internal class DashboardService: IDashboardService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProjectRepository _projectRepository;

        public DashboardService(
            IStaticDataService staticDataService,
            IProjectRepository projectRepository)
        {
            _staticDataService = staticDataService;
            _projectRepository = projectRepository;
        }

        public async Task<DashboardModel> GetDashboardModel()
        {
            var model = _dashboardModel;
            if (model != null)
                return model;

            return _dashboardModel = await GenerateModel();
        }

        async Task<DashboardModel> GenerateModel()
        {
            var model = new DashboardModel();
            var staticData = await _staticDataService.GetStaticData();
            foreach(var supportTeam in staticData.SupportTeam)
            {
                model.SupportTeams.Add(await GenerateSupportTeam(supportTeam));
            }
            return model;
        }

        async Task<DashboardSupportTeam> GenerateSupportTeam(SupportTeam supportTeam)
        {
            var query = new Query();
            query.Equal(nameof(Project.StatusId), ProjectStatusEnum.Active);
            query.SQL($"EXISTS (SELECT Id FROM [ProjectTeamSupport] AS pts WHERE pts.[ProjectId] = e.Id AND pts.[SupportTeamId] = '{supportTeam.Id}' AND pts.[CompletedAt] IS NULL)");

            var result = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);

            return new DashboardSupportTeam
            {
                Id = supportTeam.Id,
                Name = supportTeam.Name,
                OpenProjects = result.TotalCount
            };
        }

        // Cached Model

        public static void FlushSupportTeams()
        {
            _dashboardModel = null;
        }

        public static DashboardModel? _dashboardModel = null;
    }
}
