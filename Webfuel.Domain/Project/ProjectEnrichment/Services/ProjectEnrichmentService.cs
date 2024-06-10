using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domai
{
    public interface IProjectEnrichmentService
    {
        Task CalculateSupportTotalMinutesForProject(Project project);

        Task EnrichProject(Project project);
    }

    [Service(typeof(IProjectEnrichmentService))]
    internal class ProjectEnrichmentService: IProjectEnrichmentService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IStaticDataService _staticDataService;

        public ProjectEnrichmentService(
            IProjectRepository projectRepository,
            IProjectSupportRepository projectSupportRepository,
            IStaticDataService staticDataService)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _staticDataService = staticDataService;
        }

        public async Task CalculateSupportTotalMinutesForProject(Project project)
        {
            var total = await _projectSupportRepository.SumMinutesByProjectId(project.Id);
            project.SupportTotalMinutes = total ?? 0;
            await EnrichProject(project);
        }

        public async Task EnrichProject(Project project)
        {
            var staticData = await _staticDataService.GetStaticData();

            Coerce(project, staticData);
            CalculateDiagnostics(project, staticData);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Implementation

        void Coerce(Project project, IStaticDataModel staticData)
        {
            var status = staticData.ProjectStatus.First(p => p.Id == project.StatusId);
            project.Locked = status.Locked;
            project.Discarded = status.Discarded;

            // SearchTeamContactFullName
            {
                var searchTeamContactFullName = $"{project.TeamContactTitle} {project.TeamContactFirstName} {project.TeamContactLastName}";
                if (project.SearchTeamContactFullName != searchTeamContactFullName)
                {
                    project.SearchTeamContactFullName = searchTeamContactFullName;
                }
            }
        }

        void CalculateDiagnostics(Project project, IStaticDataModel staticData)
        {
            var result = new List<ProjectDiagnostic>();

            if (project.Locked)
            {
                project.DiagnosticCount = 0;
                project.DiagnosticList = result;
                return;
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

          
            project.DiagnosticList = result;
            project.DiagnosticCount = result.Count;
        }
    }
}
