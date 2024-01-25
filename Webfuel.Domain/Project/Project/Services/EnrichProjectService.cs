using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IEnrichProjectService
    {
        Task<Project> EnrichProject(Project project);
    }

    [Service(typeof(IEnrichProjectService))]
    internal class EnrichProjectService : IEnrichProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;
        private readonly IProjectChangeLogService _projectChangeLogService;

        public EnrichProjectService(
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IProjectTeamSupportRepository projectTeamSupportRepository,
            IProjectChangeLogService projectChangeLogService)
        {
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _projectTeamSupportRepository = projectTeamSupportRepository;
            _projectChangeLogService = projectChangeLogService;
        }

        public async Task<Project> EnrichProject(Project project)
        {
            var updated = project.Copy();

            await GenerateSearchFields(updated);
  
            return await _projectRepository.UpdateProject(original: project, updated: updated);
        }

        // Implementation

        Task GenerateSearchFields(Project project)
        {

            // SearchTeamContactFullName
            {
                var searchTeamContactFullName = $"{project.TeamContactTitle} {project.TeamContactFirstName} {project.TeamContactLastName}";
                if (project.SearchTeamContactFullName != searchTeamContactFullName)
                {
                    project.SearchTeamContactFullName = searchTeamContactFullName;
                }
            }

            return Task.CompletedTask;
        }
    }
}
