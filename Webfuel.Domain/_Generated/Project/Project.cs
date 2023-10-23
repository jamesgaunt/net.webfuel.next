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
                    case nameof(Project.ClosureDate):
                        ClosureDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.NIHRRSSMemberCollaboratorIds):
                        NIHRRSSMemberCollaboratorIdsJson = (string)value!;
                        break;
                    case nameof(Project.SubmittedFundingStreamName):
                        SubmittedFundingStreamName = (string)value!;
                        break;
                    case nameof(Project.DateOfRequest):
                        DateOfRequest = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.Title):
                        Title = (string)value!;
                        break;
                    case nameof(Project.BriefDescription):
                        BriefDescription = (string)value!;
                        break;
                    case nameof(Project.FundingStreamName):
                        FundingStreamName = (string)value!;
                        break;
                    case nameof(Project.TargetSubmissionDate):
                        TargetSubmissionDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(Project.ExperienceOfResearchAwards):
                        ExperienceOfResearchAwards = (string)value!;
                        break;
                    case nameof(Project.SupportRequested):
                        SupportRequested = (string)value!;
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
                    case nameof(Project.SupportRequestId):
                        SupportRequestId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.ApplicationStageId):
                        ApplicationStageId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.FundingStreamId):
                        FundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.FundingCallTypeId):
                        FundingCallTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsFellowshipId):
                        IsFellowshipId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsTeamMembersConsultedId):
                        IsTeamMembersConsultedId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsResubmissionId):
                        IsResubmissionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsLeadApplicantNHSId):
                        IsLeadApplicantNHSId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.HowDidYouFindUsId):
                        HowDidYouFindUsId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.IsInternationalMultiSiteStudyId):
                        IsInternationalMultiSiteStudyId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public int Number  { get; set; } = 0;
        public string PrefixedNumber  { get; set; } = String.Empty;
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
        public string SubmittedFundingStreamName  { get; set; } = String.Empty;
        public DateOnly DateOfRequest  { get; set; } = new DateOnly(1900, 1, 1);
        public string Title  { get; set; } = String.Empty;
        public string BriefDescription  { get; set; } = String.Empty;
        public string FundingStreamName  { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate  { get; set; } = null;
        public string ExperienceOfResearchAwards  { get; set; } = String.Empty;
        public string SupportRequested  { get; set; } = String.Empty;
        public DateOnly? ProjectStartDate  { get; set; } = null;
        public int? RecruitmentTarget  { get; set; } = null;
        public int? NumberOfProjectSites  { get; set; } = null;
        public Guid StatusId { get; set; }
        public Guid? IsQuantativeTeamContributionId { get; set; }
        public Guid? IsCTUTeamContributionId { get; set; }
        public Guid? IsPPIEAndEDIContributionId { get; set; }
        public Guid? SubmittedFundingStreamId { get; set; }
        public Guid? SupportRequestId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? FundingStreamId { get; set; }
        public Guid? FundingCallTypeId { get; set; }
        public Guid? IsFellowshipId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? IsLeadApplicantNHSId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public Guid? IsInternationalMultiSiteStudyId { get; set; }
        public Project Copy()
        {
            var entity = new Project();
            entity.Id = Id;
            entity.Number = Number;
            entity.PrefixedNumber = PrefixedNumber;
            entity.ClosureDate = ClosureDate;
            entity.NIHRRSSMemberCollaboratorIdsJson = NIHRRSSMemberCollaboratorIdsJson;
            entity.SubmittedFundingStreamName = SubmittedFundingStreamName;
            entity.DateOfRequest = DateOfRequest;
            entity.Title = Title;
            entity.BriefDescription = BriefDescription;
            entity.FundingStreamName = FundingStreamName;
            entity.TargetSubmissionDate = TargetSubmissionDate;
            entity.ExperienceOfResearchAwards = ExperienceOfResearchAwards;
            entity.SupportRequested = SupportRequested;
            entity.ProjectStartDate = ProjectStartDate;
            entity.RecruitmentTarget = RecruitmentTarget;
            entity.NumberOfProjectSites = NumberOfProjectSites;
            entity.StatusId = StatusId;
            entity.IsQuantativeTeamContributionId = IsQuantativeTeamContributionId;
            entity.IsCTUTeamContributionId = IsCTUTeamContributionId;
            entity.IsPPIEAndEDIContributionId = IsPPIEAndEDIContributionId;
            entity.SubmittedFundingStreamId = SubmittedFundingStreamId;
            entity.SupportRequestId = SupportRequestId;
            entity.ApplicationStageId = ApplicationStageId;
            entity.FundingStreamId = FundingStreamId;
            entity.FundingCallTypeId = FundingCallTypeId;
            entity.IsFellowshipId = IsFellowshipId;
            entity.IsTeamMembersConsultedId = IsTeamMembersConsultedId;
            entity.IsResubmissionId = IsResubmissionId;
            entity.IsLeadApplicantNHSId = IsLeadApplicantNHSId;
            entity.HowDidYouFindUsId = HowDidYouFindUsId;
            entity.IsInternationalMultiSiteStudyId = IsInternationalMultiSiteStudyId;
            return entity;
        }
    }
}

