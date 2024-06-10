using MediatR;

namespace Webfuel.Domain
{
    public class UpdateSupportRequestResearcher : IRequest<SupportRequest>
    {
        public required Guid Id { get; set; }

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
        public string LeadApplicantCareerStage { get; set; } = String.Empty;
        public Guid? LeadApplicantOrganisationTypeId { get; set; }
        public string LeadApplicantOrganisation { get; set; } = String.Empty;
        public string LeadApplicantDepartment { get; set; } = String.Empty;

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

    }

    internal class UpdateSupportRequestResearcherHandler : IRequestHandler<UpdateSupportRequestResearcher, SupportRequest>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly ISupportRequestChangeLogService _supportRequestChangeLogService;

        public UpdateSupportRequestResearcherHandler(
            ISupportRequestRepository supportRequestRepository,
            ISupportRequestChangeLogService supportRequestChangeLogService)
        {
            _supportRequestRepository = supportRequestRepository;
            _supportRequestChangeLogService = supportRequestChangeLogService;
        }

        public async Task<SupportRequest> Handle(UpdateSupportRequestResearcher request, CancellationToken cancellationToken)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);

            var updated = SupportRequestMapper.Apply(request, original);

            updated.TeamContactFullName = $"{updated.TeamContactTitle} {updated.TeamContactFirstName} {updated.TeamContactLastName}";
            updated.LeadApplicantFullName = $"{updated.LeadApplicantTitle} {updated.LeadApplicantFirstName} {updated.LeadApplicantLastName}";

            updated = await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }
    }
}