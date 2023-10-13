using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IProjectDeleteService
    {
        Task DeleteProject(DeleteProject request);
    }

    [Service(typeof(IProjectDeleteService))]
    internal class ProjectDeleteService : IProjectDeleteService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectDeleteService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task DeleteProject(DeleteProject request)
        {
            await _projectRepository.DeleteProject(request.Id);
        }
    }
}
