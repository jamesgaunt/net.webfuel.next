using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectTeamSupportMetadata: IRepositoryMetadata<ProjectTeamSupport>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectTeamSupport";
        
        public static string DefaultOrderBy => "ORDER BY CreatedAt DESC";
        
        public static ProjectTeamSupport DataReader(SqlDataReader dr) => new ProjectTeamSupport(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectTeamSupport entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectTeamSupport.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectTeamSupport.Id):
                        break;
                    case nameof(ProjectTeamSupport.ProjectLabel):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.ProjectLabel), entity.ProjectLabel));
                        break;
                    case nameof(ProjectTeamSupport.CreatedNotes):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.CreatedNotes), entity.CreatedNotes));
                        break;
                    case nameof(ProjectTeamSupport.CreatedAt):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(ProjectTeamSupport.CompletedNotes):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.CompletedNotes), entity.CompletedNotes));
                        break;
                    case nameof(ProjectTeamSupport.CompletedAt):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.CompletedAt), entity.CompletedAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(ProjectTeamSupport.ProjectId):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.ProjectId), entity.ProjectId));
                        break;
                    case nameof(ProjectTeamSupport.SupportTeamId):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.SupportTeamId), entity.SupportTeamId));
                        break;
                    case nameof(ProjectTeamSupport.CreatedByUserId):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.CreatedByUserId), entity.CreatedByUserId));
                        break;
                    case nameof(ProjectTeamSupport.CompletedByUserId):
                        result.Add(new SqlParameter(nameof(ProjectTeamSupport.CompletedByUserId), entity.CompletedByUserId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectTeamSupport, ProjectTeamSupportMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectTeamSupport, ProjectTeamSupportMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectTeamSupport, ProjectTeamSupportMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "ProjectLabel";
                yield return "CreatedNotes";
                yield return "CreatedAt";
                yield return "CompletedNotes";
                yield return "CompletedAt";
                yield return "ProjectId";
                yield return "SupportTeamId";
                yield return "CreatedByUserId";
                yield return "CompletedByUserId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "ProjectLabel";
                yield return "CreatedNotes";
                yield return "CreatedAt";
                yield return "CompletedNotes";
                yield return "CompletedAt";
                yield return "ProjectId";
                yield return "SupportTeamId";
                yield return "CreatedByUserId";
                yield return "CompletedByUserId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "ProjectLabel";
                yield return "CreatedNotes";
                yield return "CreatedAt";
                yield return "CompletedNotes";
                yield return "CompletedAt";
                yield return "ProjectId";
                yield return "SupportTeamId";
                yield return "CreatedByUserId";
                yield return "CompletedByUserId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectTeamSupport entity)
        {
            entity.ProjectLabel = entity.ProjectLabel ?? String.Empty;
            entity.ProjectLabel = entity.ProjectLabel.Trim();
            entity.CreatedNotes = entity.CreatedNotes ?? String.Empty;
            entity.CreatedNotes = entity.CreatedNotes.Trim();
            entity.CompletedNotes = entity.CompletedNotes ?? String.Empty;
            entity.CompletedNotes = entity.CompletedNotes.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectTeamSupportRepositoryValidator Validator { get; } = new ProjectTeamSupportRepositoryValidator();
        
        public const int ProjectLabel_MaxLength = 128;
        public const int CreatedNotes_MaxLength = 1024;
        public const int CompletedNotes_MaxLength = 1024;
        
        public static void ProjectLabel_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProjectLabel_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void CreatedNotes_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(CreatedNotes_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void CompletedNotes_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(CompletedNotes_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ProjectTeamSupportRepositoryValidator: AbstractValidator<ProjectTeamSupport>
    {
        public ProjectTeamSupportRepositoryValidator()
        {
            RuleFor(x => x.ProjectLabel).Use(ProjectTeamSupportMetadata.ProjectLabel_ValidationRules);
            RuleFor(x => x.CreatedNotes).Use(ProjectTeamSupportMetadata.CreatedNotes_ValidationRules);
            RuleFor(x => x.CompletedNotes).Use(ProjectTeamSupportMetadata.CompletedNotes_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

