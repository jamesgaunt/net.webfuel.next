using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class CreateSupportRequest: IRequest<SupportRequest>
    {
        // File Storage
        [JsonIgnore] public Guid FileStorageGroupId { get; set; }

        // Project Details

        public string Title { get; set; } = String.Empty;
        public string ProposedFundingStreamName { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate { get; set; } = null;
        public string ExperienceOfResearchAwards { get; set; } = String.Empty;
        public string BriefDescription { get; set; } = String.Empty;
        public string SupportRequested { get; set; } = String.Empty;
        public Guid? IsFellowshipId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? ProposedFundingStreamId { get; set; }
        public Guid? ProposedFundingCallTypeId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }

        // Team Contact Details

        public string TeamContactTitle { get; set; } = String.Empty;
        public string TeamContactFirstName { get; set; } = String.Empty;
        public string TeamContactLastName { get; set;} = String.Empty;
        public string TeamContactEmail { get;set; } = String.Empty;
        public Guid? TeamContactRoleId { get; set;}
        public bool TeamContactMailingPermission { get; set; }
        public bool TeamContactPrivacyStatementRead { get; set; }

        // Lead Applicant Details

        public string LeadApplicantTitle { get; set; } = String.Empty;
        public string LeadApplicantFirstName { get; set; } = String.Empty;
        public string LeadApplicantLastName { get; set; } = String.Empty;
        
        public string LeadApplicantJobRole { get; set; } = String.Empty;
        public Guid? LeadApplicantOrganisationTypeId { get; set; }  
        public string LeadApplicantOrganisation { get; set;} = String.Empty;
        public string LeadApplicantDepartment { get; set; } = String.Empty;

        public string LeadApplicantAddressLine1 { get; set; } = String.Empty;
        public string LeadApplicantAddressLine2 { get; set; } = String.Empty;
        public string LeadApplicantAddressTown { get; set; } = String.Empty;
        public string LeadApplicantAddressCounty { get; set; } = String.Empty;
        public string LeadApplicantAddressCountry { get; set;} = String.Empty;
        public string LeadApplicantAddressPostcode { get; set; } = String.Empty;

        public string LeadApplicantORCID { get; set; } = String.Empty;
        public Guid? IsLeadApplicantNHSId { get; set; }

        public Guid? LeadApplicantAgeRangeId { get; set; }
        public Guid? LeadApplicantDisabilityId { get; set; }
        public Guid? LeadApplicantGenderId { get; set; }
        public Guid? LeadApplicantEthnicityId { get; set; }
    }
}
