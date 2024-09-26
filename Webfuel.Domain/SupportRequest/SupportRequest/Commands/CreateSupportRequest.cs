using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class CreateSupportRequest : IRequest<SupportRequest>
    {
        // File Storage (this may be null if no files were submitted)
        public Guid? FileStorageGroupId { get; set; }

        // Meta (not converted to project data)
        public bool IsThisRequestLinkedToAnExistingProject { get; set; } // 1.2 Development

        // Project Details

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

        // Team Contact Details

        public string TeamContactTitle { get; set; } = String.Empty;
        public string TeamContactFirstName { get; set; } = String.Empty;
        public string TeamContactLastName { get; set; } = String.Empty;
        public string TeamContactEmail { get; set; } = String.Empty;
        public Guid? TeamContactRoleId { get; set; }
        public string TeamContactRoleFreeText { get; set; } = String.Empty;
        public bool TeamContactMailingPermission { get; set; }
        public bool TeamContactPrivacyStatementRead { get; set; }

        // Lead Applicant Details

        public string LeadApplicantTitle { get; set; } = String.Empty;
        public string LeadApplicantFirstName { get; set; } = String.Empty;
        public string LeadApplicantLastName { get; set; } = String.Empty;
        public string LeadApplicantEmail { get; set; } = String.Empty;

        public string LeadApplicantJobRole { get; set; } = String.Empty;
        public string LeadApplicantCareerStage { get; set; } = String.Empty; // DEPRICATED
        public Guid? LeadApplicantCareerStageId { get; set; } // 1.3
        public Guid? LeadApplicantOrganisationTypeId { get; set; }
        public string LeadApplicantOrganisation { get; set; } = String.Empty;
        public string LeadApplicantDepartment { get; set; } = String.Empty;
        public Guid? LeadApplicantLocationId { get; set; } // 1.3

        public string LeadApplicantAddressLine1 { get; set; } = String.Empty;
        public string LeadApplicantAddressLine2 { get; set; } = String.Empty;
        public string LeadApplicantAddressTown { get; set; } = String.Empty;
        public string LeadApplicantAddressCounty { get; set; } = String.Empty;
        public string LeadApplicantAddressCountry { get; set; } = String.Empty;
        public string LeadApplicantAddressPostcode { get; set; } = String.Empty;

        public string LeadApplicantORCID { get; set; } = String.Empty;
        public Guid? IsLeadApplicantNHSId { get; set; }

        public Guid? LeadApplicantAgeRangeId { get; set; }
        public Guid? LeadApplicantGenderId { get; set; }
        public Guid? LeadApplicantEthnicityId { get; set; }

        // Grantsmanship Questions (1.4) Not yet added to project

        public Guid? WouldYouLikeToReceiveAGrantsmanshipReviewId { get; set; }
        public Guid? IsYourSupportRequestOnlyForAGrantsmanshipReviewId { get; set; }
    }

    internal class CreateSupportRequestHandler : IRequestHandler<CreateSupportRequest, SupportRequest>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMediator _mediator;

        public CreateSupportRequestHandler(
            ISupportRequestRepository supportRequestRepository,
            IFileStorageService fileStorageService,
            IMediator mediator)
        {
            _supportRequestRepository = supportRequestRepository;
            _fileStorageService = fileStorageService;
            _mediator = mediator;
        }

        public async Task<SupportRequest> Handle(CreateSupportRequest request, CancellationToken cancellationToken)
        {
            var supportRequest = SupportRequestMapper.Map(request);

            supportRequest.StatusId = SupportRequestStatusEnum.ToBeTriaged;
            supportRequest.DateOfRequest = DateOnly.FromDateTime(DateTime.Now);
            supportRequest.CreatedAt = DateTimeOffset.UtcNow;

            supportRequest.TeamContactFullName = $"{supportRequest.TeamContactTitle} {supportRequest.TeamContactFirstName} {supportRequest.TeamContactLastName}";
            supportRequest.LeadApplicantFullName = $"{supportRequest.LeadApplicantTitle} {supportRequest.LeadApplicantFirstName} {supportRequest.LeadApplicantLastName}";

            // File Storage Setup

            if (request.FileStorageGroupId.HasValue)
                supportRequest.FileStorageGroupId = request.FileStorageGroupId.Value;
            else
                supportRequest.FileStorageGroupId = (await _fileStorageService.CreateGroup()).Id;

            return await _supportRequestRepository.InsertSupportRequest(supportRequest);
        }
    }
}