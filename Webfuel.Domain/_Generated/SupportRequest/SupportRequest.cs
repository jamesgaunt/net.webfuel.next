using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportRequest
    {
        public SupportRequest() { }
        
        public SupportRequest(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SupportRequest.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SupportRequest.TriageNote):
                        TriageNote = (string)value!;
                        break;
                    case nameof(SupportRequest.IsThisRequestLinkedToAnExistingProject):
                        IsThisRequestLinkedToAnExistingProject = (bool)value!;
                        break;
                    case nameof(SupportRequest.DateOfRequest):
                        DateOfRequest = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(SupportRequest.Title):
                        Title = (string)value!;
                        break;
                    case nameof(SupportRequest.ApplicationStageFreeText):
                        ApplicationStageFreeText = (string)value!;
                        break;
                    case nameof(SupportRequest.ProposedFundingStreamName):
                        ProposedFundingStreamName = (string)value!;
                        break;
                    case nameof(SupportRequest.NIHRApplicationId):
                        NIHRApplicationId = (string)value!;
                        break;
                    case nameof(SupportRequest.TargetSubmissionDate):
                        TargetSubmissionDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(SupportRequest.ExperienceOfResearchAwards):
                        ExperienceOfResearchAwards = (string)value!;
                        break;
                    case nameof(SupportRequest.BriefDescription):
                        BriefDescription = (string)value!;
                        break;
                    case nameof(SupportRequest.SupportRequested):
                        SupportRequested = (string)value!;
                        break;
                    case nameof(SupportRequest.HowDidYouFindUsFreeText):
                        HowDidYouFindUsFreeText = (string)value!;
                        break;
                    case nameof(SupportRequest.WhoElseIsOnTheStudyTeam):
                        WhoElseIsOnTheStudyTeam = (string)value!;
                        break;
                    case nameof(SupportRequest.IsCTUAlreadyInvolvedFreeText):
                        IsCTUAlreadyInvolvedFreeText = (string)value!;
                        break;
                    case nameof(SupportRequest.ProfessionalBackgroundIds):
                        ProfessionalBackgroundIdsJson = (string)value!;
                        break;
                    case nameof(SupportRequest.ProfessionalBackgroundFreeText):
                        ProfessionalBackgroundFreeText = (string)value!;
                        break;
                    case nameof(SupportRequest.TeamContactTitle):
                        TeamContactTitle = (string)value!;
                        break;
                    case nameof(SupportRequest.TeamContactFirstName):
                        TeamContactFirstName = (string)value!;
                        break;
                    case nameof(SupportRequest.TeamContactLastName):
                        TeamContactLastName = (string)value!;
                        break;
                    case nameof(SupportRequest.TeamContactEmail):
                        TeamContactEmail = (string)value!;
                        break;
                    case nameof(SupportRequest.TeamContactRoleFreeText):
                        TeamContactRoleFreeText = (string)value!;
                        break;
                    case nameof(SupportRequest.TeamContactMailingPermission):
                        TeamContactMailingPermission = (bool)value!;
                        break;
                    case nameof(SupportRequest.TeamContactPrivacyStatementRead):
                        TeamContactPrivacyStatementRead = (bool)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantTitle):
                        LeadApplicantTitle = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantFirstName):
                        LeadApplicantFirstName = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantLastName):
                        LeadApplicantLastName = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantEmail):
                        LeadApplicantEmail = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantJobRole):
                        LeadApplicantJobRole = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantCareerStage):
                        LeadApplicantCareerStage = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantOrganisation):
                        LeadApplicantOrganisation = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantDepartment):
                        LeadApplicantDepartment = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressLine1):
                        LeadApplicantAddressLine1 = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressLine2):
                        LeadApplicantAddressLine2 = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressTown):
                        LeadApplicantAddressTown = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressCounty):
                        LeadApplicantAddressCounty = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressCountry):
                        LeadApplicantAddressCountry = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressPostcode):
                        LeadApplicantAddressPostcode = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantORCID):
                        LeadApplicantORCID = (string)value!;
                        break;
                    case nameof(SupportRequest.ProjectId):
                        ProjectId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(SupportRequest.FileStorageGroupId):
                        FileStorageGroupId = (Guid)value!;
                        break;
                    case nameof(SupportRequest.StatusId):
                        StatusId = (Guid)value!;
                        break;
                    case nameof(SupportRequest.IsFellowshipId):
                        IsFellowshipId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.ApplicationStageId):
                        ApplicationStageId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.ProposedFundingCallTypeId):
                        ProposedFundingCallTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.ProposedFundingStreamId):
                        ProposedFundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsTeamMembersConsultedId):
                        IsTeamMembersConsultedId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsResubmissionId):
                        IsResubmissionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.HowDidYouFindUsId):
                        HowDidYouFindUsId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsCTUAlreadyInvolvedId):
                        IsCTUAlreadyInvolvedId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.TeamContactRoleId):
                        TeamContactRoleId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.LeadApplicantOrganisationTypeId):
                        LeadApplicantOrganisationTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsLeadApplicantNHSId):
                        IsLeadApplicantNHSId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.LeadApplicantAgeRangeId):
                        LeadApplicantAgeRangeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.LeadApplicantGenderId):
                        LeadApplicantGenderId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.LeadApplicantEthnicityId):
                        LeadApplicantEthnicityId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string TriageNote  { get; set; } = String.Empty;
        public bool IsThisRequestLinkedToAnExistingProject  { get; set; } = false;
        public DateOnly DateOfRequest  { get; set; } = new DateOnly(1900, 1, 1);
        public string Title  { get; set; } = String.Empty;
        public string ApplicationStageFreeText  { get; set; } = String.Empty;
        public string ProposedFundingStreamName  { get; set; } = String.Empty;
        public string NIHRApplicationId  { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate  { get; set; } = null;
        public string ExperienceOfResearchAwards  { get; set; } = String.Empty;
        public string BriefDescription  { get; set; } = String.Empty;
        public string SupportRequested  { get; set; } = String.Empty;
        public string HowDidYouFindUsFreeText  { get; set; } = String.Empty;
        public string WhoElseIsOnTheStudyTeam  { get; set; } = String.Empty;
        public string IsCTUAlreadyInvolvedFreeText  { get; set; } = String.Empty;
        public List<Guid> ProfessionalBackgroundIds
        {
            get { return _ProfessionalBackgroundIds ?? (_ProfessionalBackgroundIds = SafeJsonSerializer.Deserialize<List<Guid>>(_ProfessionalBackgroundIdsJson)); }
            set { _ProfessionalBackgroundIds = value; }
        }
        List<Guid>? _ProfessionalBackgroundIds = null;
        internal string ProfessionalBackgroundIdsJson
        {
            get { var result = _ProfessionalBackgroundIds == null ? _ProfessionalBackgroundIdsJson : (_ProfessionalBackgroundIdsJson = SafeJsonSerializer.Serialize(_ProfessionalBackgroundIds)); _ProfessionalBackgroundIds = null; return result; }
            set { _ProfessionalBackgroundIdsJson = value; _ProfessionalBackgroundIds = null; }
        }
        string _ProfessionalBackgroundIdsJson = String.Empty;
        public string ProfessionalBackgroundFreeText  { get; set; } = String.Empty;
        public string TeamContactTitle  { get; set; } = String.Empty;
        public string TeamContactFirstName  { get; set; } = String.Empty;
        public string TeamContactLastName  { get; set; } = String.Empty;
        public string TeamContactEmail  { get; set; } = String.Empty;
        public string TeamContactRoleFreeText  { get; set; } = String.Empty;
        public bool TeamContactMailingPermission  { get; set; } = false;
        public bool TeamContactPrivacyStatementRead  { get; set; } = false;
        public string LeadApplicantTitle  { get; set; } = String.Empty;
        public string LeadApplicantFirstName  { get; set; } = String.Empty;
        public string LeadApplicantLastName  { get; set; } = String.Empty;
        public string LeadApplicantEmail  { get; set; } = String.Empty;
        public string LeadApplicantJobRole  { get; set; } = String.Empty;
        public string LeadApplicantCareerStage  { get; set; } = String.Empty;
        public string LeadApplicantOrganisation  { get; set; } = String.Empty;
        public string LeadApplicantDepartment  { get; set; } = String.Empty;
        public string LeadApplicantAddressLine1  { get; set; } = String.Empty;
        public string LeadApplicantAddressLine2  { get; set; } = String.Empty;
        public string LeadApplicantAddressTown  { get; set; } = String.Empty;
        public string LeadApplicantAddressCounty  { get; set; } = String.Empty;
        public string LeadApplicantAddressCountry  { get; set; } = String.Empty;
        public string LeadApplicantAddressPostcode  { get; set; } = String.Empty;
        public string LeadApplicantORCID  { get; set; } = String.Empty;
        public Guid? ProjectId  { get; set; } = null;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid FileStorageGroupId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? IsFellowshipId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? ProposedFundingCallTypeId { get; set; }
        public Guid? ProposedFundingStreamId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public Guid? IsCTUAlreadyInvolvedId { get; set; }
        public Guid? TeamContactRoleId { get; set; }
        public Guid? LeadApplicantOrganisationTypeId { get; set; }
        public Guid? IsLeadApplicantNHSId { get; set; }
        public Guid? LeadApplicantAgeRangeId { get; set; }
        public Guid? LeadApplicantGenderId { get; set; }
        public Guid? LeadApplicantEthnicityId { get; set; }
        public SupportRequest Copy()
        {
            var entity = new SupportRequest();
            entity.Id = Id;
            entity.TriageNote = TriageNote;
            entity.IsThisRequestLinkedToAnExistingProject = IsThisRequestLinkedToAnExistingProject;
            entity.DateOfRequest = DateOfRequest;
            entity.Title = Title;
            entity.ApplicationStageFreeText = ApplicationStageFreeText;
            entity.ProposedFundingStreamName = ProposedFundingStreamName;
            entity.NIHRApplicationId = NIHRApplicationId;
            entity.TargetSubmissionDate = TargetSubmissionDate;
            entity.ExperienceOfResearchAwards = ExperienceOfResearchAwards;
            entity.BriefDescription = BriefDescription;
            entity.SupportRequested = SupportRequested;
            entity.HowDidYouFindUsFreeText = HowDidYouFindUsFreeText;
            entity.WhoElseIsOnTheStudyTeam = WhoElseIsOnTheStudyTeam;
            entity.IsCTUAlreadyInvolvedFreeText = IsCTUAlreadyInvolvedFreeText;
            entity.ProfessionalBackgroundIdsJson = ProfessionalBackgroundIdsJson;
            entity.ProfessionalBackgroundFreeText = ProfessionalBackgroundFreeText;
            entity.TeamContactTitle = TeamContactTitle;
            entity.TeamContactFirstName = TeamContactFirstName;
            entity.TeamContactLastName = TeamContactLastName;
            entity.TeamContactEmail = TeamContactEmail;
            entity.TeamContactRoleFreeText = TeamContactRoleFreeText;
            entity.TeamContactMailingPermission = TeamContactMailingPermission;
            entity.TeamContactPrivacyStatementRead = TeamContactPrivacyStatementRead;
            entity.LeadApplicantTitle = LeadApplicantTitle;
            entity.LeadApplicantFirstName = LeadApplicantFirstName;
            entity.LeadApplicantLastName = LeadApplicantLastName;
            entity.LeadApplicantEmail = LeadApplicantEmail;
            entity.LeadApplicantJobRole = LeadApplicantJobRole;
            entity.LeadApplicantCareerStage = LeadApplicantCareerStage;
            entity.LeadApplicantOrganisation = LeadApplicantOrganisation;
            entity.LeadApplicantDepartment = LeadApplicantDepartment;
            entity.LeadApplicantAddressLine1 = LeadApplicantAddressLine1;
            entity.LeadApplicantAddressLine2 = LeadApplicantAddressLine2;
            entity.LeadApplicantAddressTown = LeadApplicantAddressTown;
            entity.LeadApplicantAddressCounty = LeadApplicantAddressCounty;
            entity.LeadApplicantAddressCountry = LeadApplicantAddressCountry;
            entity.LeadApplicantAddressPostcode = LeadApplicantAddressPostcode;
            entity.LeadApplicantORCID = LeadApplicantORCID;
            entity.ProjectId = ProjectId;
            entity.CreatedAt = CreatedAt;
            entity.FileStorageGroupId = FileStorageGroupId;
            entity.StatusId = StatusId;
            entity.IsFellowshipId = IsFellowshipId;
            entity.ApplicationStageId = ApplicationStageId;
            entity.ProposedFundingCallTypeId = ProposedFundingCallTypeId;
            entity.ProposedFundingStreamId = ProposedFundingStreamId;
            entity.IsTeamMembersConsultedId = IsTeamMembersConsultedId;
            entity.IsResubmissionId = IsResubmissionId;
            entity.HowDidYouFindUsId = HowDidYouFindUsId;
            entity.IsCTUAlreadyInvolvedId = IsCTUAlreadyInvolvedId;
            entity.TeamContactRoleId = TeamContactRoleId;
            entity.LeadApplicantOrganisationTypeId = LeadApplicantOrganisationTypeId;
            entity.IsLeadApplicantNHSId = IsLeadApplicantNHSId;
            entity.LeadApplicantAgeRangeId = LeadApplicantAgeRangeId;
            entity.LeadApplicantGenderId = LeadApplicantGenderId;
            entity.LeadApplicantEthnicityId = LeadApplicantEthnicityId;
            return entity;
        }
    }
}

