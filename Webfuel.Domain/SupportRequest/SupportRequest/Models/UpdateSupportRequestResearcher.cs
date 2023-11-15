using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateSupportRequestResearcher: IRequest<SupportRequest>
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

        public string LeadApplicantJobRole { get; set; } = String.Empty;
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
}
