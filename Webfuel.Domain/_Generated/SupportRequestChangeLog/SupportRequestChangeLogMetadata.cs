using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportRequestChangeLogMetadata: IRepositoryMetadata<SupportRequestChangeLog>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportRequestChangeLog";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
        public static SupportRequestChangeLog DataReader(SqlDataReader dr) => new SupportRequestChangeLog(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportRequestChangeLog entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportRequestChangeLog.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportRequestChangeLog.Id):
                        break;
                    case nameof(SupportRequestChangeLog.Message):
                        result.Add(new SqlParameter(nameof(SupportRequestChangeLog.Message), entity.Message));
                        break;
                    case nameof(SupportRequestChangeLog.CreatedAt):
                        result.Add(new SqlParameter(nameof(SupportRequestChangeLog.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(SupportRequestChangeLog.SupportRequestId):
                        result.Add(new SqlParameter(nameof(SupportRequestChangeLog.SupportRequestId), entity.SupportRequestId));
                        break;
                    case nameof(SupportRequestChangeLog.CreatedByUserId):
                        result.Add(new SqlParameter(nameof(SupportRequestChangeLog.CreatedByUserId), entity.CreatedByUserId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportRequestChangeLog, SupportRequestChangeLogMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportRequestChangeLog, SupportRequestChangeLogMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Message";
                yield return "CreatedAt";
                yield return "SupportRequestId";
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
                yield return "SupportRequestId";
                yield return "CreatedByUserId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Message";
                yield return "CreatedAt";
                yield return "SupportRequestId";
                yield return "CreatedByUserId";
            }
        }
        
        // Validation
        
        public static void Validate(SupportRequestChangeLog entity)
        {
            entity.Message = entity.Message ?? String.Empty;
            entity.Message = entity.Message.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportRequestChangeLogRepositoryValidator Validator { get; } = new SupportRequestChangeLogRepositoryValidator();
        
        
        public static void Message_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class SupportRequestChangeLogRepositoryValidator: AbstractValidator<SupportRequestChangeLog>
    {
        public SupportRequestChangeLogRepositoryValidator()
        {
            RuleFor(x => x.Message).Use(SupportRequestChangeLogMetadata.Message_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

