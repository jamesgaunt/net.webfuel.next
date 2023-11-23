using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class ErrorLogMetadata: IRepositoryMetadata<ErrorLog>
    {
        // Data Access
        
        public static string DatabaseTable => "ErrorLog";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
        public static ErrorLog DataReader(SqlDataReader dr) => new ErrorLog(dr);
        
        public static List<SqlParameter> ExtractParameters(ErrorLog entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ErrorLog.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ErrorLog.Id):
                        break;
                    case nameof(ErrorLog.EntityId):
                        result.Add(new SqlParameter(nameof(ErrorLog.EntityId), entity.EntityId));
                        break;
                    case nameof(ErrorLog.Summary):
                        result.Add(new SqlParameter(nameof(ErrorLog.Summary), entity.Summary));
                        break;
                    case nameof(ErrorLog.Message):
                        result.Add(new SqlParameter(nameof(ErrorLog.Message), entity.Message));
                        break;
                    case nameof(ErrorLog.CreatedAt):
                        result.Add(new SqlParameter(nameof(ErrorLog.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(ErrorLog.CreatedBy):
                        result.Add(new SqlParameter(nameof(ErrorLog.CreatedBy), entity.CreatedBy));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ErrorLog, ErrorLogMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ErrorLog, ErrorLogMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ErrorLog, ErrorLogMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "EntityId";
                yield return "Summary";
                yield return "Message";
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
                yield return "Summary";
                yield return "Message";
                yield return "CreatedAt";
                yield return "CreatedBy";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "EntityId";
                yield return "Summary";
                yield return "Message";
                yield return "CreatedAt";
                yield return "CreatedBy";
            }
        }
        
        // Validation
        
        public static void Validate(ErrorLog entity)
        {
            entity.Summary = entity.Summary ?? String.Empty;
            entity.Summary = entity.Summary.Trim();
            entity.Message = entity.Message ?? String.Empty;
            entity.Message = entity.Message.Trim();
            entity.CreatedBy = entity.CreatedBy ?? String.Empty;
            entity.CreatedBy = entity.CreatedBy.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ErrorLogRepositoryValidator Validator { get; } = new ErrorLogRepositoryValidator();
        
        public const int Summary_MaxLength = 64;
        public const int CreatedBy_MaxLength = 64;
        
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
    }
    
    public partial class ErrorLogRepositoryValidator: AbstractValidator<ErrorLog>
    {
        public ErrorLogRepositoryValidator()
        {
            RuleFor(x => x.Summary).Use(ErrorLogMetadata.Summary_ValidationRules);
            RuleFor(x => x.Message).Use(ErrorLogMetadata.Message_ValidationRules);
            RuleFor(x => x.CreatedBy).Use(ErrorLogMetadata.CreatedBy_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

