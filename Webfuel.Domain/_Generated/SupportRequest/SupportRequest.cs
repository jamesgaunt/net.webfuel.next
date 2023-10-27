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
                    case nameof(SupportRequest.Title):
                        Title = (string)value!;
                        break;
                    case nameof(SupportRequest.DateOfRequest):
                        DateOfRequest = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(SupportRequest.FundingStreamName):
                        FundingStreamName = (string)value!;
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
                    case nameof(SupportRequest.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(SupportRequest.IsFellowshipId):
                        IsFellowshipId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.ApplicationStageId):
                        ApplicationStageId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.FundingCallTypeId):
                        FundingCallTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.FundingStreamId):
                        FundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsTeamMembersConsultedId):
                        IsTeamMembersConsultedId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsResubmissionId):
                        IsResubmissionId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.IsLeadApplicantNHSId):
                        IsLeadApplicantNHSId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.HowDidYouFindUsId):
                        HowDidYouFindUsId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.StatusId):
                        StatusId = (Guid)value!;
                        break;
                    case nameof(SupportRequest.ProjectId):
                        ProjectId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Title  { get; set; } = String.Empty;
        public DateOnly DateOfRequest  { get; set; } = new DateOnly(1900, 1, 1);
        public string FundingStreamName  { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate  { get; set; } = null;
        public string ExperienceOfResearchAwards  { get; set; } = String.Empty;
        public string BriefDescription  { get; set; } = String.Empty;
        public string SupportRequested  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid? IsFellowshipId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? FundingCallTypeId { get; set; }
        public Guid? FundingStreamId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? IsLeadApplicantNHSId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? ProjectId { get; set; }
        public SupportRequest Copy()
        {
            var entity = new SupportRequest();
            entity.Id = Id;
            entity.Title = Title;
            entity.DateOfRequest = DateOfRequest;
            entity.FundingStreamName = FundingStreamName;
            entity.TargetSubmissionDate = TargetSubmissionDate;
            entity.ExperienceOfResearchAwards = ExperienceOfResearchAwards;
            entity.BriefDescription = BriefDescription;
            entity.SupportRequested = SupportRequested;
            entity.CreatedAt = CreatedAt;
            entity.IsFellowshipId = IsFellowshipId;
            entity.ApplicationStageId = ApplicationStageId;
            entity.FundingCallTypeId = FundingCallTypeId;
            entity.FundingStreamId = FundingStreamId;
            entity.IsTeamMembersConsultedId = IsTeamMembersConsultedId;
            entity.IsResubmissionId = IsResubmissionId;
            entity.IsLeadApplicantNHSId = IsLeadApplicantNHSId;
            entity.HowDidYouFindUsId = HowDidYouFindUsId;
            entity.StatusId = StatusId;
            entity.ProjectId = ProjectId;
            return entity;
        }
    }
}

