using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectFileMetadata: IRepositoryMetadata<ProjectFile>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectFile";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static ProjectFile DataReader(SqlDataReader dr) => new ProjectFile(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectFile entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectFile.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectFile.Id):
                        break;
                    case nameof(ProjectFile.FileGroupId):
                        result.Add(new SqlParameter(nameof(ProjectFile.FileGroupId), entity.FileGroupId));
                        break;
                    case nameof(ProjectFile.FileName):
                        result.Add(new SqlParameter(nameof(ProjectFile.FileName), entity.FileName));
                        break;
                    case nameof(ProjectFile.SizeBytes):
                        result.Add(new SqlParameter(nameof(ProjectFile.SizeBytes), entity.SizeBytes));
                        break;
                    case nameof(ProjectFile.UploadedAt):
                        result.Add(new SqlParameter(nameof(ProjectFile.UploadedAt), entity.UploadedAt));
                        break;
                    case nameof(ProjectFile.UploadedBy):
                        result.Add(new SqlParameter(nameof(ProjectFile.UploadedBy), entity.UploadedBy));
                        break;
                    case nameof(ProjectFile.Description):
                        result.Add(new SqlParameter(nameof(ProjectFile.Description), entity.Description));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectFile, ProjectFileMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectFile, ProjectFileMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectFile, ProjectFileMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "FileGroupId";
                yield return "FileName";
                yield return "SizeBytes";
                yield return "UploadedAt";
                yield return "UploadedBy";
                yield return "Description";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "FileGroupId";
                yield return "FileName";
                yield return "SizeBytes";
                yield return "UploadedAt";
                yield return "UploadedBy";
                yield return "Description";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "FileGroupId";
                yield return "FileName";
                yield return "SizeBytes";
                yield return "UploadedAt";
                yield return "UploadedBy";
                yield return "Description";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectFile entity)
        {
            entity.FileName = entity.FileName ?? String.Empty;
            entity.FileName = entity.FileName.Trim();
            entity.UploadedBy = entity.UploadedBy ?? String.Empty;
            entity.UploadedBy = entity.UploadedBy.Trim();
            entity.Description = entity.Description ?? String.Empty;
            entity.Description = entity.Description.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectFileRepositoryValidator Validator { get; } = new ProjectFileRepositoryValidator();
        
        public const int FileName_MaxLength = 64;
        public const int UploadedBy_MaxLength = 64;
        public const int Description_MaxLength = 64;
        
        public static void FileName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FileName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void UploadedBy_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(UploadedBy_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Description_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Description_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ProjectFileRepositoryValidator: AbstractValidator<ProjectFile>
        {
            public ProjectFileRepositoryValidator()
            {
                RuleFor(x => x.FileName).Use(FileName_ValidationRules);
                RuleFor(x => x.UploadedBy).Use(UploadedBy_ValidationRules);
                RuleFor(x => x.Description).Use(Description_ValidationRules);
            }
        }
    }
}

