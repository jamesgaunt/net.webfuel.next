using MediatR;
using Webfuel.Domai;

namespace Webfuel.Domain
{
    public class UpdateProjectRequest : IRequest<Project>
    {
        public required Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string ProposedFundingStreamName { get; set; } = String.Empty;
        public string NIHRApplicationId { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate { get; set; } = null;
        public string ExperienceOfResearchAwards { get; set; } = String.Empty;
        public string BriefDescription { get; set; } = String.Empty;
        public string SupportRequested { get; set; } = String.Empty;
        public Guid? IsFellowshipId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public string ApplicationStageFreeText { get; set; } = String.Empty;
        public Guid? ProposedFundingStreamId { get; set; }
        public Guid? ProposedFundingCallTypeId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public string HowDidYouFindUsFreeText { get; set; } = String.Empty;
        public string WhoElseIsOnTheStudyTeam { get; set; } = String.Empty;
        public Guid? IsCTUAlreadyInvolvedId { get; set; }
        public string IsCTUAlreadyInvolvedFreeText { get; set; } = String.Empty;
        public List<Guid> ProfessionalBackgroundIds { get; set; } = new List<Guid>(); // 1.2 Development
        public string ProfessionalBackgroundFreeText { get; set; } = String.Empty; // 1.2 Development   
    }

    internal class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IProjectChangeLogService _projectChangeLogService;

        public UpdateProjectRequestHandler(
            IProjectRepository projectRepository,
            IProjectEnrichmentService projectEnrichmentService,
            IProjectChangeLogService projectChangeLogService)
        {
            _projectRepository = projectRepository;
            _projectEnrichmentService = projectEnrichmentService;
            _projectChangeLogService = projectChangeLogService;
        }

        public async Task<Project> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            if (original.Locked)
                throw new InvalidOperationException("Unable to edit a locked project");

            var updated = ProjectMapper.Apply(request, original);

            await _projectEnrichmentService.EnrichProject(updated);
            updated = await _projectRepository.UpdateProject(updated: updated, original: original);

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }
    }
}