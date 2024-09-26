using MediatR;

namespace Webfuel.Domain
{
    public class UpdateSupportRequest : IRequest<SupportRequest>
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

        // Grantsmanship Questions (1.4) Not yet added to project

        public Guid? WouldYouLikeToReceiveAGrantsmanshipReviewId { get; set; }
        public Guid? IsYourSupportRequestOnlyForAGrantsmanshipReviewId { get; set; }
    }

    internal class UpdateSupportRequestHandler : IRequestHandler<UpdateSupportRequest, SupportRequest>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly ISupportRequestChangeLogService _supportRequestChangeLogService;

        public UpdateSupportRequestHandler(
            ISupportRequestRepository supportRequestRepository,
            ISupportRequestChangeLogService supportRequestChangeLogService)
        {
            _supportRequestRepository = supportRequestRepository;
            _supportRequestChangeLogService = supportRequestChangeLogService;
        }

        public async Task<SupportRequest> Handle(UpdateSupportRequest request, CancellationToken cancellationToken)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);

            var updated = SupportRequestMapper.Apply(request, original);
            updated = await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);

            updated.TeamContactFullName = $"{updated.TeamContactTitle} {updated.TeamContactFirstName} {updated.TeamContactLastName}";
            updated.LeadApplicantFullName = $"{updated.LeadApplicantTitle} {updated.LeadApplicantFirstName} {updated.LeadApplicantLastName}";

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }
    }
}