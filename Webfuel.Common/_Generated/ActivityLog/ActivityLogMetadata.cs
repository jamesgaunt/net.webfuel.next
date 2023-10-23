using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class ActivityLogMetadata: IRepositoryMetadata<ActivityLog>
    {
        // Data Access
        
        public static string DatabaseTable => "ActivityLog";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static ActivityLog DataReader(SqlDataReader dr) => new ActivityLog(dr);
        
        public static List<SqlParameter> ExtractParameters(ActivityLog entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ActivityLog.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ActivityLog.Id):
                        break;
                    case nameof(ActivityLog.EntityId):
                        result.Add(new SqlParameter(nameof(ActivityLog.EntityId), entity.EntityId));
                        break;
                    case nameof(ActivityLog.EntityName):
                        result.Add(new SqlParameter(nameof(ActivityLog.EntityName), entity.EntityName));
                        break;
                    case nameof(ActivityLog.Summary):
                        result.Add(new SqlParameter(nameof(ActivityLog.Summary), entity.Summary));
                        break;
                    case nameof(ActivityLog.Message):
                        result.Add(new SqlParameter(nameof(ActivityLog.Message), entity.Message));
                        break;
                    case nameof(ActivityLog.MessageEnriched):
                        result.Add(new SqlParameter(nameof(ActivityLog.MessageEnriched), entity.MessageEnriched));
                        break;
                    case nameof(ActivityLog.CreatedAt):
                        result.Add(new SqlParameter(nameof(ActivityLog.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(ActivityLog.CreatedBy):
                        result.Add(new SqlParameter(nameof(ActivityLog.CreatedBy), entity.CreatedBy));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ActivityLog, ActivityLogMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ActivityLog, ActivityLogMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ActivityLog, ActivityLogMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "EntityId";
                yield return "EntityName";
                yield return "Summary";
                yield return "Message";
                yield return "MessageEnriched";
                yield return "CreatedAt";
                yield return "CreatedBy";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "EntityId";
                yield return "EntityName";
                yield return "Summary";
                yield return "Message";
                yield return "MessageEnriched";
                yield return "CreatedAt";
                yield return "CreatedBy";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "EntityId";
                yield return "EntityName";
                yield return "Summary";
                yield return "Message";
                yield return "MessageEnriched";
                yield return "CreatedAt";
                yield return "CreatedBy";
            }
        }
        
        // Validation
        
        public static void Validate(ActivityLog entity)
        {
            entity.EntityName = entity.EntityName ?? String.Empty;
            entity.EntityName = entity.EntityName.Trim();
            entity.Summary = entity.Summary ?? String.Empty;
            entity.Summary = entity.Summary.Trim();
            entity.Message = entity.Message ?? String.Empty;
            entity.Message = entity.Message.Trim();
            entity.CreatedBy = entity.CreatedBy ?? String.Empty;
            entity.CreatedBy = entity.CreatedBy.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ActivityLogRepositoryValidator Validator { get; } = new ActivityLogRepositoryValidator();
        
        public const int EntityName_MaxLength = 64;
        public const int Summary_MaxLength = 64;
        public const int CreatedBy_MaxLength = 64;
        
        public static void EntityName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(EntityName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Summary_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Summary_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Message_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void CreatedBy_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(CreatedBy_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ActivityLogRepositoryValidator: AbstractValidator<ActivityLog>
        {
            public ActivityLogRepositoryValidator()
            {
                RuleFor(x => x.EntityName).Use(EntityName_ValidationRules);
                RuleFor(x => x.Summary).Use(Summary_ValidationRules);
                RuleFor(x => x.Message).Use(Message_ValidationRules);
                RuleFor(x => x.CreatedBy).Use(CreatedBy_ValidationRules);
            }
        }
    }
}

