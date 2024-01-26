using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.Dashboard;

namespace Webfuel.Domain
{
    public interface IDeleteProjectService
    {
        Task DeleteProject(DeleteProject request);
    }

    [Service(typeof(IDeleteProjectService))]
    internal class DeleteProjectService : IDeleteProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task DeleteProject(DeleteProject request)
        {
            await _projectRepository.DeleteProject(request.Id);

            DashboardService.FlushProjectMetrics();
        }
    }
}
