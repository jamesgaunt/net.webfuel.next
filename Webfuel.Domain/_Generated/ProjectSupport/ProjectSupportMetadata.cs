using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSupportMetadata: IRepositoryMetadata<ProjectSupport>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectSupport";
        
        public static string DefaultOrderBy => "ORDER BY Date DESC, Id DESC";
        
        public static ProjectSupport DataReader(SqlDataReader dr) => new ProjectSupport(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectSupport entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectSupport.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectSupport.Id):
                        break;
                    case nameof(ProjectSupport.Date):
                        result.Add(new SqlParameter(nameof(ProjectSupport.Date), entity.Date));
                        break;
                    case nameof(ProjectSupport.Description):
                        result.Add(new SqlParameter(nameof(ProjectSupport.Description), entity.Description));
                        break;
                    case nameof(ProjectSupport.WorkTimeInHours):
                        result.Add(new SqlParameter(nameof(ProjectSupport.WorkTimeInHours), entity.WorkTimeInHours));
                        break;
                    case nameof(ProjectSupport.TeamIds):
                        result.Add(new SqlParameter(nameof(ProjectSupport.TeamIds), entity.TeamIdsJson));
                        break;
                    case nameof(ProjectSupport.AdviserIds):
                        result.Add(new SqlParameter(nameof(ProjectSupport.AdviserIds), entity.AdviserIdsJson));
                        break;
                    case nameof(ProjectSupport.SupportProvidedIds):
                        result.Add(new SqlParameter(nameof(ProjectSupport.SupportProvidedIds), entity.SupportProvidedIdsJson));
                        break;
                    case nameof(ProjectSupport.SupportRequestedAt):
                        result.Add(new SqlParameter(nameof(ProjectSupport.SupportRequestedAt), entity.SupportRequestedAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(ProjectSupport.SupportRequestedCompletedAt):
                        result.Add(new SqlParameter(nameof(ProjectSupport.SupportRequestedCompletedAt), entity.SupportRequestedCompletedAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(ProjectSupport.SupportRequestedCompletedNotes):
                        result.Add(new SqlParameter(nameof(ProjectSupport.SupportRequestedCompletedNotes), entity.SupportRequestedCompletedNotes));
                        break;
                    case nameof(ProjectSupport.Files):
                        result.Add(new SqlParameter(nameof(ProjectSupport.Files), entity.FilesJson));
                        break;
                    case nameof(ProjectSupport.CalculatedMinutes):
                        result.Add(new SqlParameter(nameof(ProjectSupport.CalculatedMinutes), entity.CalculatedMinutes));
                        break;
                    case nameof(ProjectSupport.ProjectId):
                        result.Add(new SqlParameter(nameof(ProjectSupport.ProjectId), entity.ProjectId));
                        break;
                    case nameof(ProjectSupport.IsPrePostAwardId):
                        result.Add(new SqlParameter(nameof(ProjectSupport.IsPrePostAwardId), entity.IsPrePostAwardId));
                        break;
                    case nameof(ProjectSupport.SupportRequestedTeamId):
                        result.Add(new SqlParameter(nameof(ProjectSupport.SupportRequestedTeamId), entity.SupportRequestedTeamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(ProjectSupport.SupportRequestedCompletedByUserId):
                        result.Add(new SqlParameter(nameof(ProjectSupport.SupportRequestedCompletedByUserId), entity.SupportRequestedCompletedByUserId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectSupport, ProjectSupportMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectSupport, ProjectSupportMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectSupport, ProjectSupportMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Date";
                yield return "Description";
                yield return "WorkTimeInHours";
                yield return "TeamIds";
                yield return "AdviserIds";
                yield return "SupportProvidedIds";
                yield return "SupportRequestedAt";
                yield return "SupportRequestedCompletedAt";
                yield return "SupportRequestedCompletedNotes";
                yield return "Files";
                yield return "CalculatedMinutes";
                yield return "ProjectId";
                yield return "IsPrePostAwardId";
                yield return "SupportRequestedTeamId";
                yield return "SupportRequestedCompletedByUserId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Date";
                yield return "Description";
                yield return "WorkTimeInHours";
                yield return "TeamIds";
                yield return "AdviserIds";
                yield return "SupportProvidedIds";
                yield return "SupportRequestedAt";
                yield return "SupportRequestedCompletedAt";
                yield return "SupportRequestedCompletedNotes";
                yield return "Files";
                yield return "CalculatedMinutes";
                yield return "ProjectId";
                yield return "IsPrePostAwardId";
                yield return "SupportRequestedTeamId";
                yield return "SupportRequestedCompletedByUserId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Date";
                yield return "Description";
                yield return "WorkTimeInHours";
                yield return "TeamIds";
                yield return "AdviserIds";
                yield return "SupportProvidedIds";
                yield return "SupportRequestedAt";
                yield return "SupportRequestedCompletedAt";
                yield return "SupportRequestedCompletedNotes";
                yield return "Files";
                yield return "CalculatedMinutes";
                yield return "ProjectId";
                yield return "IsPrePostAwardId";
                yield return "SupportRequestedTeamId";
                yield return "SupportRequestedCompletedByUserId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectSupport entity)
        {
            entity.Description = entity.Description ?? String.Empty;
            entity.Description = entity.Description.Trim();
            entity.SupportRequestedCompletedNotes = entity.SupportRequestedCompletedNotes ?? String.Empty;
            entity.SupportRequestedCompletedNotes = entity.SupportRequestedCompletedNotes.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectSupportRepositoryValidator Validator { get; } = new ProjectSupportRepositoryValidator();
        
        public const int Description_MaxLength = 5000;
        public const int SupportRequestedCompletedNotes_MaxLength = 1024;
        
        public static void Description_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Description_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SupportRequestedCompletedNotes_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SupportRequestedCompletedNotes_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ProjectSupportRepositoryValidator: AbstractValidator<ProjectSupport>
    {
        public ProjectSupportRepositoryValidator()
        {
            RuleFor(x => x.Description).Use(ProjectSupportMetadata.Description_ValidationRules);
            RuleFor(x => x.SupportRequestedCompletedNotes).Use(ProjectSupportMetadata.SupportRequestedCompletedNotes_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

