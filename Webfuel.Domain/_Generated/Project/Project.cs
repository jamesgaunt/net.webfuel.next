using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Project
    {
        public Project() { }
        
        public Project(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Project.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Project.Number):
                        Number = (int)value!;
                        break;
                    case nameof(Project.PrefixedNumber):
                        PrefixedNumber = (string)value!;
                        break;
                    case nameof(Project.SupportRequestId):
                        SupportRequestId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.ClosureDate):
                        ClosureDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.NIHRRSSMemberCollaboratorIds):
                        NIHRRSSMemberCollaboratorIdsJson = (string)value!;
                        break;
                    case nameof(Project.SubmittedFundingStreamFreeText):
                        SubmittedFundingStreamFreeText = (string)value!;
                        break;
                    case nameof(Project.SubmittedFundingStreamName):
                        SubmittedFundingStreamName = (string)value!;
                        break;
                    case nameof(Project.ProjectStartDate):
                        ProjectStartDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.RecruitmentTarget):
                        RecruitmentTarget = value == DBNull.Value ? (int?)null : (int?)value;
                        break;
                    case nameof(Project.NumberOfProjectSites):
                        NumberOfProjectSites = value == DBNull.Value ? (int?)null : (int?)value;
                        break;
                    case nameof(Project.DateOfRequest):
                        DateOfRequest = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.Title):
                        Title = (string)value!;
                        break;
                    case nameof(Project.ProposedFundingStreamName):
                        ProposedFundingStreamName = (string)value!;
                        break;
                    case nameof(Project.TargetSubmissionDate):
                        TargetSubmissionDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.ExperienceOfResearchAwards):
                        ExperienceOfResearchAwards = (string)value!;
                        break;
                    case nameof(Project.BriefDescription):
                        BriefDescription = (string)value!;
                        break;
                    case nameof(Project.SupportRequested):
                        SupportRequested = (string)value!;
                        break;
                    case nameof(Project.TeamContactTitle):
                        TeamContactTitle = (string)value!;
                        break;
                    case nameof(Project.TeamContactFirstName):
                        TeamContactFirstName = (string)value!;
                        break;
                    case nameof(Project.TeamContactLastName):
                        TeamContactLastName = (string)value!;
                        break;
                    case nameof(Project.TeamContactEmail):
                        TeamContactEmail = (string)value!;
                        break;
                    case nameof(Project.TeamContactMailingPermission):
                        TeamContactMailingPermission = (bool)value!;
                        break;
                    case nameof(Project.TeamContactPrivacyStatementRead):
                        TeamContactPrivacyStatementRead = (bool)value!;
                        break;
                    case nameof(Project.LeadApplicantTitle):
                        LeadApplicantTitle = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantFirstName):
                        LeadApplicantFirstName = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantLastName):
                        LeadApplicantLastName = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantJobRole):
                        LeadApplicantJobRole = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantOrganisation):
                        LeadApplicantOrganisation = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantDepartment):
                        LeadApplicantDepartment = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantAddressLine1):
                        LeadApplicantAddressLine1 = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantAddressLine2):
                        LeadApplicantAddressLine2 = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantAddressTown):
                        LeadApplicantAddressTown = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantAddressCounty):
                        LeadApplicantAddressCounty = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantAddressCountry):
                        LeadApplicantAddressCountry = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantAddressPostcode):
                        LeadApplicantAddressPostcode = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantORCID):
                        LeadApplicantORCID = (string)value!;
                        break;
                    case nameof(Project.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(Project.StatusId):
                        StatusId = (Guid)value!;
                        break;
                    case nameof(Project.IsQuantativeTeamContributionId):
                        IsQuantativeTeamContributionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsCTUTeamContributionId):
                        IsCTUTeamContributionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsPPIEAndEDIContributionId):
                        IsPPIEAndEDIContributionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.SubmittedFundingStreamId):
                        SubmittedFundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsInternationalMultiSiteStudyId):
                        IsInternationalMultiSiteStudyId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsFellowshipId):
                        IsFellowshipId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.ApplicationStageId):
                        ApplicationStageId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.ProposedFundingCallTypeId):
                        ProposedFundingCallTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.ProposedFundingStreamId):
                        ProposedFundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsTeamMembersConsultedId):
                        IsTeamMembersConsultedId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsResubmissionId):
                        IsResubmissionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.HowDidYouFindUsId):
                        HowDidYouFindUsId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.TeamContactRoleId):
                        TeamContactRoleId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantOrganisationTypeId):
                        LeadApplicantOrganisationTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsLeadApplicantNHSId):
                        IsLeadApplicantNHSId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantAgeRangeId):
                        LeadApplicantAgeRangeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantDisabilityId):
                        LeadApplicantDisabilityId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantGenderId):
                        LeadApplicantGenderId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantEthnicityId):
                        LeadApplicantEthnicityId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.FileStorageGroupId):
                        FileStorageGroupId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public int Number  { get; set; } = 0;
        public string PrefixedNumber  { get; set; } = String.Empty;
        public Guid? SupportRequestId  { get; set; } = null;
        public DateOnly? ClosureDate  { get; set; } = null;
        public List<Guid> NIHRRSSMemberCollaboratorIds
        {
            get { return _NIHRRSSMemberCollaboratorIds ?? (_NIHRRSSMemberCollaboratorIds = SafeJsonSerializer.Deserialize<List<Guid>>(_NIHRRSSMemberCollaboratorIdsJson)); }
            set { _NIHRRSSMemberCollaboratorIds = value; }
        }
        List<Guid>? _NIHRRSSMemberCollaboratorIds = null;
        internal string NIHRRSSMemberCollaboratorIdsJson
        {
            get { var result = _NIHRRSSMemberCollaboratorIds == null ? _NIHRRSSMemberCollaboratorIdsJson : (_NIHRRSSMemberCollaboratorIdsJson = SafeJsonSerializer.Serialize(_NIHRRSSMemberCollaboratorIds)); _NIHRRSSMemberCollaboratorIds = null; return result; }
            set { _NIHRRSSMemberCollaboratorIdsJson = value; _NIHRRSSMemberCollaboratorIds = null; }
        }
        string _NIHRRSSMemberCollaboratorIdsJson = String.Empty;
        public string SubmittedFundingStreamFreeText  { get; set; } = String.Empty;
        public string SubmittedFundingStreamName  { get; set; } = String.Empty;
        public DateOnly? ProjectStartDate  { get; set; } = null;
        public int? RecruitmentTarget  { get; set; } = null;
        public int? NumberOfProjectSites  { get; set; } = null;
        public DateOnly DateOfRequest  { get; set; } = new DateOnly(1900, 1, 1);
        public string Title  { get; set; } = String.Empty;
        public string ProposedFundingStreamName  { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate  { get; set; } = null;
        public string ExperienceOfResearchAwards  { get; set; } = String.Empty;
        public string BriefDescription  { get; set; } = String.Empty;
        public string SupportRequested  { get; set; } = String.Empty;
        public string TeamContactTitle  { get; set; } = String.Empty;
        public string TeamContactFirstName  { get; set; } = String.Empty;
        public string TeamContactLastName  { get; set; } = String.Empty;
        public string TeamContactEmail  { get; set; } = String.Empty;
        public bool TeamContactMailingPermission  { get; set; } = false;
        public bool TeamContactPrivacyStatementRead  { get; set; } = false;
        public string LeadApplicantTitle  { get; set; } = String.Empty;
        public string LeadApplicantFirstName  { get; set; } = String.Empty;
        public string LeadApplicantLastName  { get; set; } = String.Empty;
        public string LeadApplicantJobRole  { get; set; } = String.Empty;
        public string LeadApplicantOrganisation  { get; set; } = String.Empty;
        public string LeadApplicantDepartment  { get; set; } = String.Empty;
        public string LeadApplicantAddressLine1  { get; set; } = String.Empty;
        public string LeadApplicantAddressLine2  { get; set; } = String.Empty;
        public string LeadApplicantAddressTown  { get; set; } = String.Empty;
        public string LeadApplicantAddressCounty  { get; set; } = String.Empty;
        public string LeadApplicantAddressCountry  { get; set; } = String.Empty;
        public string LeadApplicantAddressPostcode  { get; set; } = String.Empty;
        public string LeadApplicantORCID  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid StatusId { get; set; }
        public Guid? IsQuantativeTeamContributionId { get; set; }
        public Guid? IsCTUTeamContributionId { get; set; }
        public Guid? IsPPIEAndEDIContributionId { get; set; }
        public Guid? SubmittedFundingStreamId { get; set; }
        public Guid? IsInternationalMultiSiteStudyId { get; set; }
        public Guid? IsFellowshipId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? ProposedFundingCallTypeId { get; set; }
        public Guid? ProposedFundingStreamId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public Guid? TeamContactRoleId { get; set; }
        public Guid? LeadApplicantOrganisationTypeId { get; set; }
        public Guid? IsLeadApplicantNHSId { get; set; }
        public Guid? LeadApplicantAgeRangeId { get; set; }
        public Guid? LeadApplicantDisabilityId { get; set; }
        public Guid? LeadApplicantGenderId { get; set; }
        public Guid? LeadApplicantEthnicityId { get; set; }
        public Guid FileStorageGroupId { get; set; }
        public Project Copy()
        {
            var entity = new Project();
            entity.Id = Id;
            entity.Number = Number;
            entity.PrefixedNumber = PrefixedNumber;
            entity.SupportRequestId = SupportRequestId;
            entity.ClosureDate = ClosureDate;
            entity.NIHRRSSMemberCollaboratorIdsJson = NIHRRSSMemberCollaboratorIdsJson;
            entity.SubmittedFundingStreamFreeText = SubmittedFundingStreamFreeText;
            entity.SubmittedFundingStreamName = SubmittedFundingStreamName;
            entity.ProjectStartDate = ProjectStartDate;
            entity.RecruitmentTarget = RecruitmentTarget;
            entity.NumberOfProjectSites = NumberOfProjectSites;
            entity.DateOfRequest = DateOfRequest;
            entity.Title = Title;
            entity.ProposedFundingStreamName = ProposedFundingStreamName;
            entity.TargetSubmissionDate = TargetSubmissionDate;
            entity.ExperienceOfResearchAwards = ExperienceOfResearchAwards;
            entity.BriefDescription = BriefDescription;
            entity.SupportRequested = SupportRequested;
            entity.TeamContactTitle = TeamContactTitle;
            entity.TeamContactFirstName = TeamContactFirstName;
            entity.TeamContactLastName = TeamContactLastName;
            entity.TeamContactEmail = TeamContactEmail;
            entity.TeamContactMailingPermission = TeamContactMailingPermission;
            entity.TeamContactPrivacyStatementRead = TeamContactPrivacyStatementRead;
            entity.LeadApplicantTitle = LeadApplicantTitle;
            entity.LeadApplicantFirstName = LeadApplicantFirstName;
            entity.LeadApplicantLastName = LeadApplicantLastName;
            entity.LeadApplicantJobRole = LeadApplicantJobRole;
            entity.LeadApplicantOrganisation = LeadApplicantOrganisation;
            entity.LeadApplicantDepartment = LeadApplicantDepartment;
            entity.LeadApplicantAddressLine1 = LeadApplicantAddressLine1;
            entity.LeadApplicantAddressLine2 = LeadApplicantAddressLine2;
            entity.LeadApplicantAddressTown = LeadApplicantAddressTown;
            entity.LeadApplicantAddressCounty = LeadApplicantAddressCounty;
            entity.LeadApplicantAddressCountry = LeadApplicantAddressCountry;
            entity.LeadApplicantAddressPostcode = LeadApplicantAddressPostcode;
            entity.LeadApplicantORCID = LeadApplicantORCID;
            entity.CreatedAt = CreatedAt;
            entity.StatusId = StatusId;
            entity.IsQuantativeTeamContributionId = IsQuantativeTeamContributionId;
            entity.IsCTUTeamContributionId = IsCTUTeamContributionId;
            entity.IsPPIEAndEDIContributionId = IsPPIEAndEDIContributionId;
            entity.SubmittedFundingStreamId = SubmittedFundingStreamId;
            entity.IsInternationalMultiSiteStudyId = IsInternationalMultiSiteStudyId;
            entity.IsFellowshipId = IsFellowshipId;
            entity.ApplicationStageId = ApplicationStageId;
            entity.ProposedFundingCallTypeId = ProposedFundingCallTypeId;
            entity.ProposedFundingStreamId = ProposedFundingStreamId;
            entity.IsTeamMembersConsultedId = IsTeamMembersConsultedId;
            entity.IsResubmissionId = IsResubmissionId;
            entity.HowDidYouFindUsId = HowDidYouFindUsId;
            entity.TeamContactRoleId = TeamContactRoleId;
            entity.LeadApplicantOrganisationTypeId = LeadApplicantOrganisationTypeId;
            entity.IsLeadApplicantNHSId = IsLeadApplicantNHSId;
            entity.LeadApplicantAgeRangeId = LeadApplicantAgeRangeId;
            entity.LeadApplicantDisabilityId = LeadApplicantDisabilityId;
            entity.LeadApplicantGenderId = LeadApplicantGenderId;
            entity.LeadApplicantEthnicityId = LeadApplicantEthnicityId;
            entity.FileStorageGroupId = FileStorageGroupId;
            return entity;
        }
    }
}

