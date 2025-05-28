using MediatR;

namespace Webfuel.Domain;

public class UpdateProjectResearcher : IRequest<Project>
{
    public required Guid Id { get; set; }

    // Team Contact Details

    public string TeamContactTitle { get; set; } = String.Empty;
    public string TeamContactFirstName { get; set; } = String.Empty;
    public string TeamContactLastName { get; set; } = String.Empty;
    public string TeamContactEmail { get; set; } = String.Empty;
    public string TeamContactAltEmail { get; set; } = String.Empty;
    public Guid? TeamContactRoleId { get; set; }
    public string TeamContactRoleFreeText { get; set; } = String.Empty;
    public bool TeamContactMailingPermission { get; set; }
    public bool TeamContactPrivacyStatementRead { get; set; }
    public bool TeamContactServiceAgreementRead { get; set; }

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

}

internal class UpdateProjectResearcherHandler : IRequestHandler<UpdateProjectResearcher, Project>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectEnrichmentService _projectEnrichmentService;
    private readonly IProjectChangeLogService _projectChangeLogService;

    public UpdateProjectResearcherHandler(
        IProjectRepository projectRepository,
        IProjectEnrichmentService projectEnrichmentService,
        IProjectChangeLogService projectChangeLogService)
    {
        _projectRepository = projectRepository;
        _projectEnrichmentService = projectEnrichmentService;
        _projectChangeLogService = projectChangeLogService;
    }

    public async Task<Project> Handle(UpdateProjectResearcher request, CancellationToken cancellationToken)
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