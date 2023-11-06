using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSubmissionMetadata: IRepositoryMetadata<ProjectSubmission>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectSubmission";
        
        public static string DefaultOrderBy => "ORDER BY SubmissionDate DESC";
        
        public static ProjectSubmission DataReader(SqlDataReader dr) => new ProjectSubmission(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectSubmission entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectSubmission.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectSubmission.Id):
                        break;
                    case nameof(ProjectSubmission.NIHRReference):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.NIHRReference), entity.NIHRReference));
                        break;
                    case nameof(ProjectSubmission.SubmissionDate):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.SubmissionDate), entity.SubmissionDate));
                        break;
                    case nameof(ProjectSubmission.FundingAmountOnSubmission):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.FundingAmountOnSubmission), entity.FundingAmountOnSubmission));
                        break;
                    case nameof(ProjectSubmission.ProjectId):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.ProjectId), entity.ProjectId));
                        break;
                    case nameof(ProjectSubmission.SubmissionStageId):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.SubmissionStageId), entity.SubmissionStageId));
                        break;
                    case nameof(ProjectSubmission.SubmissionOutcomeId):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.SubmissionOutcomeId), entity.SubmissionOutcomeId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectSubmission, ProjectSubmissionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectSubmission, ProjectSubmissionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectSubmission, ProjectSubmissionMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "NIHRReference";
                yield return "SubmissionDate";
                yield return "FundingAmountOnSubmission";
                yield return "ProjectId";
                yield return "SubmissionStageId";
                yield return "SubmissionOutcomeId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "NIHRReference";
                yield return "SubmissionDate";
                yield return "FundingAmountOnSubmission";
                yield return "ProjectId";
                yield return "SubmissionStageId";
                yield return "SubmissionOutcomeId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "NIHRReference";
                yield return "SubmissionDate";
                yield return "FundingAmountOnSubmission";
                yield return "ProjectId";
                yield return "SubmissionStageId";
                yield return "SubmissionOutcomeId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectSubmission entity)
        {
            entity.NIHRReference = entity.NIHRReference ?? String.Empty;
            entity.NIHRReference = entity.NIHRReference.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectSubmissionRepositoryValidator Validator { get; } = new ProjectSubmissionRepositoryValidator();
        
        public const int NIHRReference_MaxLength = 64;
        
        public static void NIHRReference_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(NIHRReference_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ProjectSubmissionRepositoryValidator: AbstractValidator<ProjectSubmission>
        {
            public ProjectSubmissionRepositoryValidator()
            {
                RuleFor(x => x.NIHRReference).Use(NIHRReference_ValidationRules);
            }
        }
    }
}

