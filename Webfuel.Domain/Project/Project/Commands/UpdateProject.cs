using MediatR;
using Webfuel.Domai;
using Webfuel.Domain.Dashboard;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class UpdateProject : IRequest<Project>
    {
        public required Guid Id { get; set; }
        public required Guid StatusId { get; set; }
        public DateOnly? ClosureDate { get; set; } = null;

        // Ownership

        public Guid? LeadAdviserUserId { get; set; }

        // Annual Reporting

        public Guid? WillStudyUseCTUId { get; set; }
        public Guid? IsPaidRSSAdviserLeadId { get; set; }
        public Guid? IsPaidRSSAdviserCoapplicantId { get; set; }
        public List<Guid> RSSHubProvidingAdviceIds { get; set; } = new List<Guid>();
        public Decimal? MonetaryValueOfFundingApplication { get; set; }

        // Funding Stream

        public Guid? SubmittedFundingStreamId { get; set; }
        public string SubmittedFundingStreamFreeText { get; set; } = String.Empty;
        public string SubmittedFundingStreamName { get; set; } = String.Empty;

        // Clinical Trial Submissions

        public DateOnly? ProjectStartDate { get; set; } = null;
        public int? RecruitmentTarget { get; set; } = null;
        public int? NumberOfProjectSites { get; set; } = null;
        public Guid? IsInternationalMultiSiteStudyId { get; set; }

        // Project Advisers

        public List<Guid> ProjectAdviserUserIds { get; set; } = new List<Guid>();
    }

    internal class UpdateProjectHandler : IRequestHandler<UpdateProject, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IProjectChangeLogService _projectChangeLogService;

        public UpdateProjectHandler(
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IProjectEnrichmentService projectEnrichmentService,
            IProjectChangeLogService projectChangeLogService)
        {
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _projectEnrichmentService = projectEnrichmentService;
            _projectChangeLogService = projectChangeLogService;
        }

        public async Task<Project> Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            if (original.Locked)
                throw new InvalidOperationException("Unable to edit a locked project");

            var updated = ProjectMapper.Apply(request, original);

            if (updated.StatusId == request.StatusId)
            {
                await _projectEnrichmentService.EnrichProject(updated);
                updated = await _projectRepository.UpdateProject(original: original, updated: updated);
                await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
                return updated; // No change to status  
            }

            var newStatus = await _staticDataService.RequireProjectStatus(request.StatusId);
            
            if (newStatus.Id == ProjectStatusEnum.Closed)
                updated.ClosureAttempted = true;

            // Enrich the project with the new status, to detect diagnostic errors
            await _projectEnrichmentService.EnrichProject(updated);

            updated.StatusId = newStatus.Id;
            updated.Locked = newStatus.Locked;
            updated.Discarded = newStatus.Discarded;

            if (newStatus.Id == ProjectStatusEnum.Closed)
            {
                if (updated.DiagnosticCount > 0)
                {
                    // We can't close the project if there are errors or warnings
                    updated.StatusId = original.StatusId;
                    updated.Locked = original.Locked;
                    updated.Discarded = original.Discarded;
                }
                else
                {
                    updated.ClosureDate = request.ClosureDate ?? updated.ClosureDate ?? DateOnly.FromDateTime(DateTime.Today);
                }
            }

            updated = await _projectRepository.UpdateProject(original: original, updated: updated);
            DashboardService.FlushProjectMetrics();

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }
    }
}
