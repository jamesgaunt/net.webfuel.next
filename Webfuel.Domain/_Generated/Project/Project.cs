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
                    case nameof(Project.ClosureAttempted):
                        ClosureAttempted = (bool)value!;
                        break;
                    case nameof(Project.AdministratorComments):
                        AdministratorComments = (string)value!;
                        break;
                    case nameof(Project.SubmittedFundingStreamFreeText):
                        SubmittedFundingStreamFreeText = (string)value!;
                        break;
                    case nameof(Project.SubmittedFundingStreamName):
                        SubmittedFundingStreamName = (string)value!;
                        break;
                    case nameof(Project.Locked):
                        Locked = (bool)value!;
                        break;
                    case nameof(Project.Discarded):
                        Discarded = (bool)value!;
                        break;
                    case nameof(Project.RSSHubProvidingAdviceIds):
                        RSSHubProvidingAdviceIdsJson = (string)value!;
                        break;
                    case nameof(Project.MonetaryValueOfFundingApplication):
                        MonetaryValueOfFundingApplication = value == DBNull.Value ? (Decimal?)null : (Decimal?)value;
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
                    case nameof(Project.SocialCare):
                        SocialCare = (bool)value!;
                        break;
                    case nameof(Project.PublicHealth):
                        PublicHealth = (bool)value!;
                        break;
                    case nameof(Project.OutlineSubmissionDeadline):
                        OutlineSubmissionDeadline = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.OutlineOutcomeExpectedDate):
                        OutlineOutcomeExpectedDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.FullSubmissionDeadline):
                        FullSubmissionDeadline = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.FullOutcomeExpectedDate):
                        FullOutcomeExpectedDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.MockInterviews):
                        MockInterviews = (bool)value!;
                        break;
                    case nameof(Project.GrantsmanshipReview):
                        GrantsmanshipReview = (bool)value!;
                        break;
                    case nameof(Project.DateOfRequest):
                        DateOfRequest = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.Title):
                        Title = (string)value!;
                        break;
                    case nameof(Project.ApplicationStageFreeText):
                        ApplicationStageFreeText = (string)value!;
                        break;
                    case nameof(Project.ProposedFundingStreamName):
                        ProposedFundingStreamName = (string)value!;
                        break;
                    case nameof(Project.NIHRApplicationId):
                        NIHRApplicationId = (string)value!;
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
                    case nameof(Project.HowDidYouFindUsFreeText):
                        HowDidYouFindUsFreeText = (string)value!;
                        break;
                    case nameof(Project.WhoElseIsOnTheStudyTeam):
                        WhoElseIsOnTheStudyTeam = (string)value!;
                        break;
                    case nameof(Project.IsCTUAlreadyInvolvedFreeText):
                        IsCTUAlreadyInvolvedFreeText = (string)value!;
                        break;
                    case nameof(Project.ProfessionalBackgroundIds):
                        ProfessionalBackgroundIdsJson = (string)value!;
                        break;
                    case nameof(Project.ProfessionalBackgroundFreeText):
                        ProfessionalBackgroundFreeText = (string)value!;
                        break;
                    case nameof(Project.IsRoundRobinEnquiry):
                        IsRoundRobinEnquiry = (bool)value!;
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
                    case nameof(Project.TeamContactAltEmail):
                        TeamContactAltEmail = (string)value!;
                        break;
                    case nameof(Project.TeamContactRoleFreeText):
                        TeamContactRoleFreeText = (string)value!;
                        break;
                    case nameof(Project.TeamContactMailingPermission):
                        TeamContactMailingPermission = (bool)value!;
                        break;
                    case nameof(Project.TeamContactPrivacyStatementRead):
                        TeamContactPrivacyStatementRead = (bool)value!;
                        break;
                    case nameof(Project.TeamContactServiceAgreementRead):
                        TeamContactServiceAgreementRead = (bool)value!;
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
                    case nameof(Project.LeadApplicantEmail):
                        LeadApplicantEmail = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantJobRole):
                        LeadApplicantJobRole = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantCareerStage):
                        LeadApplicantCareerStage = (string)value!;
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
                    case nameof(Project.HeartbeatExecutedAt):
                        HeartbeatExecutedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(Project.DiagnosticCount):
                        DiagnosticCount = (int)value!;
                        break;
                    case nameof(Project.DiagnosticList):
                        DiagnosticListJson = (string)value!;
                        break;
                    case nameof(Project.TeamContactFullName):
                        TeamContactFullName = (string)value!;
                        break;
                    case nameof(Project.LeadApplicantFullName):
                        LeadApplicantFullName = (string)value!;
                        break;
                    case nameof(Project.SupportTotalMinutes):
                        SupportTotalMinutes = (int)value!;
                        break;
                    case nameof(Project.OpenSupportRequestTeamIds):
                        OpenSupportRequestTeamIdsJson = (string)value!;
                        break;
                    case nameof(Project.OverdueSupportRequestTeamIds):
                        OverdueSupportRequestTeamIdsJson = (string)value!;
                        break;
                    case nameof(Project.LegacyProfessionalBackgroundIds):
                        LegacyProfessionalBackgroundIdsJson = (string)value!;
                        break;
                    case nameof(Project.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(Project.FileStorageGroupId):
                        FileStorageGroupId = (Guid)value!;
                        break;
                    case nameof(Project.LeadAdviserUserId):
                        LeadAdviserUserId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.SubmittedFundingStreamId):
                        SubmittedFundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.StatusId):
                        StatusId = (Guid)value!;
                        break;
                    case nameof(Project.WillStudyUseCTUId):
                        WillStudyUseCTUId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsPaidRSSAdviserLeadId):
                        IsPaidRSSAdviserLeadId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsPaidRSSAdviserCoapplicantId):
                        IsPaidRSSAdviserCoapplicantId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsInternationalMultiSiteStudyId):
                        IsInternationalMultiSiteStudyId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.OutlineSubmissionStatusId):
                        OutlineSubmissionStatusId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.OutlineOutcomeId):
                        OutlineOutcomeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.FullSubmissionStatusId):
                        FullSubmissionStatusId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.FullOutcomeId):
                        FullOutcomeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
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
                    case nameof(Project.IsCTUAlreadyInvolvedId):
                        IsCTUAlreadyInvolvedId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.TeamContactRoleId):
                        TeamContactRoleId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantCareerStageId):
                        LeadApplicantCareerStageId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantOrganisationTypeId):
                        LeadApplicantOrganisationTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantLocationId):
                        LeadApplicantLocationId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsLeadApplicantNHSId):
                        IsLeadApplicantNHSId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantAgeRangeId):
                        LeadApplicantAgeRangeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantGenderId):
                        LeadApplicantGenderId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.LeadApplicantEthnicityId):
                        LeadApplicantEthnicityId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public int Number  { get; set; } = 0;
        public string PrefixedNumber  { get; set; } = String.Empty;
        public Guid? SupportRequestId  { get; set; } = null;
        public DateOnly? ClosureDate  { get; set; } = null;
        public bool ClosureAttempted  { get; set; } = false;
        public string AdministratorComments  { get; set; } = String.Empty;
        public string SubmittedFundingStreamFreeText  { get; set; } = String.Empty;
        public string SubmittedFundingStreamName  { get; set; } = String.Empty;
        public bool Locked  { get; set; } = false;
        public bool Discarded  { get; set; } = false;
        public List<Guid> RSSHubProvidingAdviceIds
        {
            get { return _RSSHubProvidingAdviceIds ?? (_RSSHubProvidingAdviceIds = SafeJsonSerializer.Deserialize<List<Guid>>(_RSSHubProvidingAdviceIdsJson)); }
            set { _RSSHubProvidingAdviceIds = value; }
        }
        List<Guid>? _RSSHubProvidingAdviceIds = null;
        internal string RSSHubProvidingAdviceIdsJson
        {
            get { var result = _RSSHubProvidingAdviceIds == null ? _RSSHubProvidingAdviceIdsJson : (_RSSHubProvidingAdviceIdsJson = SafeJsonSerializer.Serialize(_RSSHubProvidingAdviceIds)); _RSSHubProvidingAdviceIds = null; return result; }
            set { _RSSHubProvidingAdviceIdsJson = value; _RSSHubProvidingAdviceIds = null; }
        }
        string _RSSHubProvidingAdviceIdsJson = String.Empty;
        public Decimal? MonetaryValueOfFundingApplication  { get; set; } = null;
        public DateOnly? ProjectStartDate  { get; set; } = null;
        public int? RecruitmentTarget  { get; set; } = null;
        public int? NumberOfProjectSites  { get; set; } = null;
        public bool SocialCare  { get; set; } = false;
        public bool PublicHealth  { get; set; } = false;
        public DateOnly? OutlineSubmissionDeadline  { get; set; } = null;
        public DateOnly? OutlineOutcomeExpectedDate  { get; set; } = null;
        public DateOnly? FullSubmissionDeadline  { get; set; } = null;
        public DateOnly? FullOutcomeExpectedDate  { get; set; } = null;
        public bool MockInterviews  { get; set; } = false;
        public bool GrantsmanshipReview  { get; set; } = false;
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
        public bool IsRoundRobinEnquiry  { get; set; } = false;
        public string TeamContactTitle  { get; set; } = String.Empty;
        public string TeamContactFirstName  { get; set; } = String.Empty;
        public string TeamContactLastName  { get; set; } = String.Empty;
        public string TeamContactEmail  { get; set; } = String.Empty;
        public string TeamContactAltEmail  { get; set; } = String.Empty;
        public string TeamContactRoleFreeText  { get; set; } = String.Empty;
        public bool TeamContactMailingPermission  { get; set; } = false;
        public bool TeamContactPrivacyStatementRead  { get; set; } = false;
        public bool TeamContactServiceAgreementRead  { get; set; } = false;
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
        public DateTimeOffset HeartbeatExecutedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public int DiagnosticCount  { get; set; } = 0;
        public List<ProjectDiagnostic> DiagnosticList
        {
            get { return _DiagnosticList ?? (_DiagnosticList = SafeJsonSerializer.Deserialize<List<ProjectDiagnostic>>(_DiagnosticListJson)); }
            set { _DiagnosticList = value; }
        }
        List<ProjectDiagnostic>? _DiagnosticList = null;
        internal string DiagnosticListJson
        {
            get { var result = _DiagnosticList == null ? _DiagnosticListJson : (_DiagnosticListJson = SafeJsonSerializer.Serialize(_DiagnosticList)); _DiagnosticList = null; return result; }
            set { _DiagnosticListJson = value; _DiagnosticList = null; }
        }
        string _DiagnosticListJson = String.Empty;
        public string TeamContactFullName  { get; set; } = String.Empty;
        public string LeadApplicantFullName  { get; set; } = String.Empty;
        public int SupportTotalMinutes  { get; set; } = 0;
        public List<Guid> OpenSupportRequestTeamIds
        {
            get { return _OpenSupportRequestTeamIds ?? (_OpenSupportRequestTeamIds = SafeJsonSerializer.Deserialize<List<Guid>>(_OpenSupportRequestTeamIdsJson)); }
            set { _OpenSupportRequestTeamIds = value; }
        }
        List<Guid>? _OpenSupportRequestTeamIds = null;
        internal string OpenSupportRequestTeamIdsJson
        {
            get { var result = _OpenSupportRequestTeamIds == null ? _OpenSupportRequestTeamIdsJson : (_OpenSupportRequestTeamIdsJson = SafeJsonSerializer.Serialize(_OpenSupportRequestTeamIds)); _OpenSupportRequestTeamIds = null; return result; }
            set { _OpenSupportRequestTeamIdsJson = value; _OpenSupportRequestTeamIds = null; }
        }
        string _OpenSupportRequestTeamIdsJson = String.Empty;
        public List<Guid> OverdueSupportRequestTeamIds
        {
            get { return _OverdueSupportRequestTeamIds ?? (_OverdueSupportRequestTeamIds = SafeJsonSerializer.Deserialize<List<Guid>>(_OverdueSupportRequestTeamIdsJson)); }
            set { _OverdueSupportRequestTeamIds = value; }
        }
        List<Guid>? _OverdueSupportRequestTeamIds = null;
        internal string OverdueSupportRequestTeamIdsJson
        {
            get { var result = _OverdueSupportRequestTeamIds == null ? _OverdueSupportRequestTeamIdsJson : (_OverdueSupportRequestTeamIdsJson = SafeJsonSerializer.Serialize(_OverdueSupportRequestTeamIds)); _OverdueSupportRequestTeamIds = null; return result; }
            set { _OverdueSupportRequestTeamIdsJson = value; _OverdueSupportRequestTeamIds = null; }
        }
        string _OverdueSupportRequestTeamIdsJson = String.Empty;
        public List<Guid> LegacyProfessionalBackgroundIds
        {
            get { return _LegacyProfessionalBackgroundIds ?? (_LegacyProfessionalBackgroundIds = SafeJsonSerializer.Deserialize<List<Guid>>(_LegacyProfessionalBackgroundIdsJson)); }
            set { _LegacyProfessionalBackgroundIds = value; }
        }
        List<Guid>? _LegacyProfessionalBackgroundIds = null;
        internal string LegacyProfessionalBackgroundIdsJson
        {
            get { var result = _LegacyProfessionalBackgroundIds == null ? _LegacyProfessionalBackgroundIdsJson : (_LegacyProfessionalBackgroundIdsJson = SafeJsonSerializer.Serialize(_LegacyProfessionalBackgroundIds)); _LegacyProfessionalBackgroundIds = null; return result; }
            set { _LegacyProfessionalBackgroundIdsJson = value; _LegacyProfessionalBackgroundIds = null; }
        }
        string _LegacyProfessionalBackgroundIdsJson = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid FileStorageGroupId { get; set; }
        public Guid? LeadAdviserUserId { get; set; }
        public Guid? SubmittedFundingStreamId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? WillStudyUseCTUId { get; set; }
        public Guid? IsPaidRSSAdviserLeadId { get; set; }
        public Guid? IsPaidRSSAdviserCoapplicantId { get; set; }
        public Guid? IsInternationalMultiSiteStudyId { get; set; }
        public Guid? OutlineSubmissionStatusId { get; set; }
        public Guid? OutlineOutcomeId { get; set; }
        public Guid? FullSubmissionStatusId { get; set; }
        public Guid? FullOutcomeId { get; set; }
        public Guid? IsFellowshipId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? ProposedFundingCallTypeId { get; set; }
        public Guid? ProposedFundingStreamId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public Guid? IsCTUAlreadyInvolvedId { get; set; }
        public Guid? TeamContactRoleId { get; set; }
        public Guid? LeadApplicantCareerStageId { get; set; }
        public Guid? LeadApplicantOrganisationTypeId { get; set; }
        public Guid? LeadApplicantLocationId { get; set; }
        public Guid? IsLeadApplicantNHSId { get; set; }
        public Guid? LeadApplicantAgeRangeId { get; set; }
        public Guid? LeadApplicantGenderId { get; set; }
        public Guid? LeadApplicantEthnicityId { get; set; }
        public Project Copy()
        {
            var entity = new Project();
            entity.Id = Id;
            entity.Number = Number;
            entity.PrefixedNumber = PrefixedNumber;
            entity.SupportRequestId = SupportRequestId;
            entity.ClosureDate = ClosureDate;
            entity.ClosureAttempted = ClosureAttempted;
            entity.AdministratorComments = AdministratorComments;
            entity.SubmittedFundingStreamFreeText = SubmittedFundingStreamFreeText;
            entity.SubmittedFundingStreamName = SubmittedFundingStreamName;
            entity.Locked = Locked;
            entity.Discarded = Discarded;
            entity.RSSHubProvidingAdviceIdsJson = RSSHubProvidingAdviceIdsJson;
            entity.MonetaryValueOfFundingApplication = MonetaryValueOfFundingApplication;
            entity.ProjectStartDate = ProjectStartDate;
            entity.RecruitmentTarget = RecruitmentTarget;
            entity.NumberOfProjectSites = NumberOfProjectSites;
            entity.SocialCare = SocialCare;
            entity.PublicHealth = PublicHealth;
            entity.OutlineSubmissionDeadline = OutlineSubmissionDeadline;
            entity.OutlineOutcomeExpectedDate = OutlineOutcomeExpectedDate;
            entity.FullSubmissionDeadline = FullSubmissionDeadline;
            entity.FullOutcomeExpectedDate = FullOutcomeExpectedDate;
            entity.MockInterviews = MockInterviews;
            entity.GrantsmanshipReview = GrantsmanshipReview;
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
            entity.IsRoundRobinEnquiry = IsRoundRobinEnquiry;
            entity.TeamContactTitle = TeamContactTitle;
            entity.TeamContactFirstName = TeamContactFirstName;
            entity.TeamContactLastName = TeamContactLastName;
            entity.TeamContactEmail = TeamContactEmail;
            entity.TeamContactAltEmail = TeamContactAltEmail;
            entity.TeamContactRoleFreeText = TeamContactRoleFreeText;
            entity.TeamContactMailingPermission = TeamContactMailingPermission;
            entity.TeamContactPrivacyStatementRead = TeamContactPrivacyStatementRead;
            entity.TeamContactServiceAgreementRead = TeamContactServiceAgreementRead;
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
            entity.HeartbeatExecutedAt = HeartbeatExecutedAt;
            entity.DiagnosticCount = DiagnosticCount;
            entity.DiagnosticListJson = DiagnosticListJson;
            entity.TeamContactFullName = TeamContactFullName;
            entity.LeadApplicantFullName = LeadApplicantFullName;
            entity.SupportTotalMinutes = SupportTotalMinutes;
            entity.OpenSupportRequestTeamIdsJson = OpenSupportRequestTeamIdsJson;
            entity.OverdueSupportRequestTeamIdsJson = OverdueSupportRequestTeamIdsJson;
            entity.LegacyProfessionalBackgroundIdsJson = LegacyProfessionalBackgroundIdsJson;
            entity.CreatedAt = CreatedAt;
            entity.FileStorageGroupId = FileStorageGroupId;
            entity.LeadAdviserUserId = LeadAdviserUserId;
            entity.SubmittedFundingStreamId = SubmittedFundingStreamId;
            entity.StatusId = StatusId;
            entity.WillStudyUseCTUId = WillStudyUseCTUId;
            entity.IsPaidRSSAdviserLeadId = IsPaidRSSAdviserLeadId;
            entity.IsPaidRSSAdviserCoapplicantId = IsPaidRSSAdviserCoapplicantId;
            entity.IsInternationalMultiSiteStudyId = IsInternationalMultiSiteStudyId;
            entity.OutlineSubmissionStatusId = OutlineSubmissionStatusId;
            entity.OutlineOutcomeId = OutlineOutcomeId;
            entity.FullSubmissionStatusId = FullSubmissionStatusId;
            entity.FullOutcomeId = FullOutcomeId;
            entity.IsFellowshipId = IsFellowshipId;
            entity.ApplicationStageId = ApplicationStageId;
            entity.ProposedFundingCallTypeId = ProposedFundingCallTypeId;
            entity.ProposedFundingStreamId = ProposedFundingStreamId;
            entity.IsTeamMembersConsultedId = IsTeamMembersConsultedId;
            entity.IsResubmissionId = IsResubmissionId;
            entity.HowDidYouFindUsId = HowDidYouFindUsId;
            entity.IsCTUAlreadyInvolvedId = IsCTUAlreadyInvolvedId;
            entity.TeamContactRoleId = TeamContactRoleId;
            entity.LeadApplicantCareerStageId = LeadApplicantCareerStageId;
            entity.LeadApplicantOrganisationTypeId = LeadApplicantOrganisationTypeId;
            entity.LeadApplicantLocationId = LeadApplicantLocationId;
            entity.IsLeadApplicantNHSId = IsLeadApplicantNHSId;
            entity.LeadApplicantAgeRangeId = LeadApplicantAgeRangeId;
            entity.LeadApplicantGenderId = LeadApplicantGenderId;
            entity.LeadApplicantEthnicityId = LeadApplicantEthnicityId;
            return entity;
        }
    }
}

