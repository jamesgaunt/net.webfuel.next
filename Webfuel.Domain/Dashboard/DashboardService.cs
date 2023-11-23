using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardModel> GetDashboardModel();
    }

    [Service(typeof(IDashboardService))]
    internal class DashboardService: IDashboardService
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;

        public DashboardService(IProjectTeamSupportRepository projectTeamSupportRepository)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
        }

        public async Task<DashboardModel> GetDashboardModel()
        {
            var model = new DashboardModel();

            model.OpenTeamSupport.AddRange(await _projectTeamSupportRepository.SelectProjectTeamSupportByCompletedAt(completedAt: null));

            return model;
        }
    }
}
