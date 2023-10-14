using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IUpdateProjectService
    {
        Task<Project> UpdateProject(UpdateProject request);
    }

    [Service(typeof(IUpdateProjectService))]
    internal class UpdateProjectService : IUpdateProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> UpdateProject(UpdateProject request)
        {
            var original = await _projectRepository.RequireProject(request.Id);

            var updated = original.Copy();

            updated.Title = request.Title;
            updated.FundingBodyId = request.FundingBodyId;
            updated.ResearchMethodologyId = request.ResearchMethodologyId;

            return await _projectRepository.UpdateProject(original: original, updated: updated);
        }
    }
}
