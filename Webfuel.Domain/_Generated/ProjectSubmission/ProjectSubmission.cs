using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSubmission
    {
        public ProjectSubmission() { }
        
        public ProjectSubmission(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectSubmission.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectSubmission.NIHRReference):
                        NIHRReference = (string)value!;
                        break;
                    case nameof(ProjectSubmission.SubmissionDate):
                        SubmissionDate = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(ProjectSubmission.FundingAmountOnSubmission):
                        FundingAmountOnSubmission = value == DBNull.Value ? (int?)null : (int?)value;
                        break;
                    case nameof(ProjectSubmission.OutcomeExpectedDate):
                        OutcomeExpectedDate = value == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(ProjectSubmission.ProjectId):
                        ProjectId = (Guid)value!;
                        break;
                    case nameof(ProjectSubmission.FundingStreamId):
                        FundingStreamId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(ProjectSubmission.SubmissionStatusId):
                        SubmissionStatusId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(ProjectSubmission.SubmissionStageId):
                        SubmissionStageId = (Guid)value!;
                        break;
                    case nameof(ProjectSubmission.SubmissionOutcomeId):
                        SubmissionOutcomeId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string NIHRReference  { get; set; } = String.Empty;
        public DateOnly SubmissionDate  { get; set; } = new DateOnly(1900, 1, 1);
        public int? FundingAmountOnSubmission  { get; set; } = null;
        public DateOnly? OutcomeExpectedDate  { get; set; } = null;
        public Guid ProjectId { get; set; }
        public Guid? FundingStreamId { get; set; }
        public Guid? SubmissionStatusId { get; set; }
        public Guid SubmissionStageId { get; set; }
        public Guid? SubmissionOutcomeId { get; set; }
        public ProjectSubmission Copy()
        {
            var entity = new ProjectSubmission();
            entity.Id = Id;
            entity.NIHRReference = NIHRReference;
            entity.SubmissionDate = SubmissionDate;
            entity.FundingAmountOnSubmission = FundingAmountOnSubmission;
            entity.OutcomeExpectedDate = OutcomeExpectedDate;
            entity.ProjectId = ProjectId;
            entity.FundingStreamId = FundingStreamId;
            entity.SubmissionStatusId = SubmissionStatusId;
            entity.SubmissionStageId = SubmissionStageId;
            entity.SubmissionOutcomeId = SubmissionOutcomeId;
            return entity;
        }
    }
}

