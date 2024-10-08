using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IProjectEnrichmentService
    {
        Task CalculateSupportMetricsForProject(Project project);

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

        public async Task CalculateSupportMetricsForProject(Project project)
        {
            project.SupportTotalMinutes = (await _projectSupportRepository.SumMinutesByProjectId(project.Id)) ?? 0;

            var openSupportRequestTeams = await _projectSupportRepository.SelectOpenSupportRequestTeamIdsByProjectId(project.Id);
            project.OpenSupportRequestTeamIds.Clear();
            foreach (var openTeam in openSupportRequestTeams.Where(p => p != null && p.SupportRequestedTeamId != null))
            { 
                project.OpenSupportRequestTeamIds.Add(openTeam.SupportRequestedTeamId!.Value);
            }

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
            project.TeamContactFullName = $"{project.TeamContactTitle} {project.TeamContactFirstName} {project.TeamContactLastName}";
            project.LeadApplicantFullName = $"{project.LeadApplicantTitle} {project.LeadApplicantFirstName} {project.LeadApplicantLastName}";
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
