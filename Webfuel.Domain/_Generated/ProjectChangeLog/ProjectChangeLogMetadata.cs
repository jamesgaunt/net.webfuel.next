using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectChangeLogMetadata: IRepositoryMetadata<ProjectChangeLog>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectChangeLog";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
        public static ProjectChangeLog DataReader(SqlDataReader dr) => new ProjectChangeLog(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectChangeLog entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectChangeLog.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectChangeLog.Id):
                        break;
                    case nameof(ProjectChangeLog.Message):
                        result.Add(new SqlParameter(nameof(ProjectChangeLog.Message), entity.Message));
                        break;
                    case nameof(ProjectChangeLog.CreatedAt):
                        result.Add(new SqlParameter(nameof(ProjectChangeLog.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(ProjectChangeLog.ProjectId):
                        result.Add(new SqlParameter(nameof(ProjectChangeLog.ProjectId), entity.ProjectId));
                        break;
                    case nameof(ProjectChangeLog.CreatedByUserId):
                        result.Add(new SqlParameter(nameof(ProjectChangeLog.CreatedByUserId), entity.CreatedByUserId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectChangeLog, ProjectChangeLogMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectChangeLog, ProjectChangeLogMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectChangeLog, ProjectChangeLogMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Message";
                yield return "CreatedAt";
                yield return "ProjectId";
                yield return "CreatedByUserId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Message";
                yield return "CreatedAt";
                yield return "ProjectId";
                yield return "CreatedByUserId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Message";
                yield return "CreatedAt";
                yield return "ProjectId";
                yield return "CreatedByUserId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectChangeLog entity)
        {
            entity.Message = entity.Message ?? String.Empty;
            entity.Message = entity.Message.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectChangeLogRepositoryValidator Validator { get; } = new ProjectChangeLogRepositoryValidator();
        
        
        public static void Message_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class ProjectChangeLogRepositoryValidator: AbstractValidator<ProjectChangeLog>
    {
        public ProjectChangeLogRepositoryValidator()
        {
            RuleFor(x => x.Message).Use(ProjectChangeLogMetadata.Message_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

