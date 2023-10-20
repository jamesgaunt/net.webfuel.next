using FluentValidation;
using Microsoft.Data.SqlClient;
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
                    case nameof(SupportRequest.LinkId):
                        LinkId = (Guid)value!;
                        break;
                    case nameof(SupportRequest.Title):
                        Title = (string)value!;
                        break;
                    case nameof(SupportRequest.Fellowship):
                        Fellowship = (bool)value!;
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
                    case nameof(SupportRequest.TeamMembersConsulted):
                        TeamMembersConsulted = (bool)value!;
                        break;
                    case nameof(SupportRequest.Resubmission):
                        Resubmission = (bool)value!;
                        break;
                    case nameof(SupportRequest.BriefDescription):
                        BriefDescription = (string)value!;
                        break;
                    case nameof(SupportRequest.SupportRequested):
                        SupportRequested = (string)value!;
                        break;
                    case nameof(SupportRequest.LeadApplicantNHS):
                        LeadApplicantNHS = (bool)value!;
                        break;
                    case nameof(SupportRequest.ApplicationStageId):
                        ApplicationStageId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.FundingStreamId):
                        FundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.FundingCallTypeId):
                        FundingCallTypeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(SupportRequest.StatusId):
                        StatusId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid LinkId  { get; set; } = Guid.Empty;
        public string Title  { get; set; } = String.Empty;
        public bool Fellowship  { get; set; } = false;
        public DateOnly DateOfRequest  { get; set; } = new DateOnly(1900, 1, 1);
        public string FundingStreamName  { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate  { get; set; } = null;
        public string ExperienceOfResearchAwards  { get; set; } = String.Empty;
        public bool TeamMembersConsulted  { get; set; } = false;
        public bool Resubmission  { get; set; } = false;
        public string BriefDescription  { get; set; } = String.Empty;
        public string SupportRequested  { get; set; } = String.Empty;
        public bool LeadApplicantNHS  { get; set; } = false;
        public Guid? ApplicationStageId { get; set; }
        public Guid? FundingStreamId { get; set; }
        public Guid? FundingCallTypeId { get; set; }
        public Guid StatusId { get; set; }
        public SupportRequest Copy()
        {
            var entity = new SupportRequest();
            entity.Id = Id;
            entity.LinkId = LinkId;
            entity.Title = Title;
            entity.Fellowship = Fellowship;
            entity.DateOfRequest = DateOfRequest;
            entity.FundingStreamName = FundingStreamName;
            entity.TargetSubmissionDate = TargetSubmissionDate;
            entity.ExperienceOfResearchAwards = ExperienceOfResearchAwards;
            entity.TeamMembersConsulted = TeamMembersConsulted;
            entity.Resubmission = Resubmission;
            entity.BriefDescription = BriefDescription;
            entity.SupportRequested = SupportRequested;
            entity.LeadApplicantNHS = LeadApplicantNHS;
            entity.ApplicationStageId = ApplicationStageId;
            entity.FundingStreamId = FundingStreamId;
            entity.FundingCallTypeId = FundingCallTypeId;
            entity.StatusId = StatusId;
            return entity;
        }
    }
}

