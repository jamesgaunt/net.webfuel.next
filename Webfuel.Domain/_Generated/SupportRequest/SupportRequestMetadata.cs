using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportRequestMetadata: IRepositoryMetadata<SupportRequest>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportRequest";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static SupportRequest DataReader(SqlDataReader dr) => new SupportRequest(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportRequest entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportRequest.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportRequest.Id):
                        break;
                    case nameof(SupportRequest.Title):
                        result.Add(new SqlParameter(nameof(SupportRequest.Title), entity.Title));
                        break;
                    case nameof(SupportRequest.DateOfRequest):
                        result.Add(new SqlParameter(nameof(SupportRequest.DateOfRequest), entity.DateOfRequest));
                        break;
                    case nameof(SupportRequest.FundingStreamName):
                        result.Add(new SqlParameter(nameof(SupportRequest.FundingStreamName), entity.FundingStreamName));
                        break;
                    case nameof(SupportRequest.TargetSubmissionDate):
                        result.Add(new SqlParameter(nameof(SupportRequest.TargetSubmissionDate), entity.TargetSubmissionDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.ExperienceOfResearchAwards):
                        result.Add(new SqlParameter(nameof(SupportRequest.ExperienceOfResearchAwards), entity.ExperienceOfResearchAwards));
                        break;
                    case nameof(SupportRequest.BriefDescription):
                        result.Add(new SqlParameter(nameof(SupportRequest.BriefDescription), entity.BriefDescription));
                        break;
                    case nameof(SupportRequest.SupportRequested):
                        result.Add(new SqlParameter(nameof(SupportRequest.SupportRequested), entity.SupportRequested));
                        break;
                    case nameof(SupportRequest.CreatedAt):
                        result.Add(new SqlParameter(nameof(SupportRequest.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(SupportRequest.IsFellowshipId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsFellowshipId), entity.IsFellowshipId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.ApplicationStageId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ApplicationStageId), entity.ApplicationStageId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.FundingCallTypeId):
                        result.Add(new SqlParameter(nameof(SupportRequest.FundingCallTypeId), entity.FundingCallTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.FundingStreamId):
                        result.Add(new SqlParameter(nameof(SupportRequest.FundingStreamId), entity.FundingStreamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsTeamMembersConsultedId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsTeamMembersConsultedId), entity.IsTeamMembersConsultedId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsResubmissionId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsResubmissionId), entity.IsResubmissionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsLeadApplicantNHSId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsLeadApplicantNHSId), entity.IsLeadApplicantNHSId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.HowDidYouFindUsId):
                        result.Add(new SqlParameter(nameof(SupportRequest.HowDidYouFindUsId), entity.HowDidYouFindUsId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.StatusId):
                        result.Add(new SqlParameter(nameof(SupportRequest.StatusId), entity.StatusId));
                        break;
                    case nameof(SupportRequest.ProjectId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProjectId), entity.ProjectId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportRequest, SupportRequestMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportRequest, SupportRequestMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportRequest, SupportRequestMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Title";
                yield return "DateOfRequest";
                yield return "FundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "CreatedAt";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "FundingCallTypeId";
                yield return "FundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "IsLeadApplicantNHSId";
                yield return "HowDidYouFindUsId";
                yield return "StatusId";
                yield return "ProjectId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Title";
                yield return "DateOfRequest";
                yield return "FundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "CreatedAt";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "FundingCallTypeId";
                yield return "FundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "IsLeadApplicantNHSId";
                yield return "HowDidYouFindUsId";
                yield return "StatusId";
                yield return "ProjectId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Title";
                yield return "DateOfRequest";
                yield return "FundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "CreatedAt";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "FundingCallTypeId";
                yield return "FundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "IsLeadApplicantNHSId";
                yield return "HowDidYouFindUsId";
                yield return "StatusId";
                yield return "ProjectId";
            }
        }
        
        // Validation
        
        public static void Validate(SupportRequest entity)
        {
            entity.Title = entity.Title ?? String.Empty;
            entity.Title = entity.Title.Trim();
            entity.FundingStreamName = entity.FundingStreamName ?? String.Empty;
            entity.FundingStreamName = entity.FundingStreamName.Trim();
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards ?? String.Empty;
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards.Trim();
            entity.BriefDescription = entity.BriefDescription ?? String.Empty;
            entity.BriefDescription = entity.BriefDescription.Trim();
            entity.SupportRequested = entity.SupportRequested ?? String.Empty;
            entity.SupportRequested = entity.SupportRequested.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportRequestRepositoryValidator Validator { get; } = new SupportRequestRepositoryValidator();
        
        public const int Title_MaxLength = 1000;
        public const int FundingStreamName_MaxLength = 64;
        public const int ExperienceOfResearchAwards_MaxLength = 1000;
        public const int BriefDescription_MaxLength = 5000;
        public const int SupportRequested_MaxLength = 2000;
        
        public static void Title_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Title_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void FundingStreamName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FundingStreamName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ExperienceOfResearchAwards_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ExperienceOfResearchAwards_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void BriefDescription_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(BriefDescription_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SupportRequested_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SupportRequested_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class SupportRequestRepositoryValidator: AbstractValidator<SupportRequest>
        {
            public SupportRequestRepositoryValidator()
            {
                RuleFor(x => x.Title).Use(Title_ValidationRules);
                RuleFor(x => x.FundingStreamName).Use(FundingStreamName_ValidationRules);
                RuleFor(x => x.ExperienceOfResearchAwards).Use(ExperienceOfResearchAwards_ValidationRules);
                RuleFor(x => x.BriefDescription).Use(BriefDescription_ValidationRules);
                RuleFor(x => x.SupportRequested).Use(SupportRequested_ValidationRules);
            }
        }
    }
}

