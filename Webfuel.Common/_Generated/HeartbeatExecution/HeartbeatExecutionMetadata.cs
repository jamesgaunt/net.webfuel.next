using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class HeartbeatExecutionMetadata: IRepositoryMetadata<HeartbeatExecution>
    {
        // Data Access
        
        public static string DatabaseTable => "HeartbeatExecution";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
        public static HeartbeatExecution DataReader(SqlDataReader dr) => new HeartbeatExecution(dr);
        
        public static List<SqlParameter> ExtractParameters(HeartbeatExecution entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(HeartbeatExecution.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(HeartbeatExecution.Id):
                        break;
                    case nameof(HeartbeatExecution.ExecutedAt):
                        result.Add(new SqlParameter(nameof(HeartbeatExecution.ExecutedAt), entity.ExecutedAt));
                        break;
                    case nameof(HeartbeatExecution.Message):
                        result.Add(new SqlParameter(nameof(HeartbeatExecution.Message), entity.Message));
                        break;
                    case nameof(HeartbeatExecution.Success):
                        result.Add(new SqlParameter(nameof(HeartbeatExecution.Success), entity.Success));
                        break;
                    case nameof(HeartbeatExecution.Microseconds):
                        result.Add(new SqlParameter(nameof(HeartbeatExecution.Microseconds), entity.Microseconds));
                        break;
                    case nameof(HeartbeatExecution.MetadataJson):
                        result.Add(new SqlParameter(nameof(HeartbeatExecution.MetadataJson), entity.MetadataJson));
                        break;
                    case nameof(HeartbeatExecution.HeartbeatId):
                        result.Add(new SqlParameter(nameof(HeartbeatExecution.HeartbeatId), entity.HeartbeatId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<HeartbeatExecution, HeartbeatExecutionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<HeartbeatExecution, HeartbeatExecutionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<HeartbeatExecution, HeartbeatExecutionMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "ExecutedAt";
                yield return "Message";
                yield return "Success";
                yield return "Microseconds";
                yield return "MetadataJson";
                yield return "HeartbeatId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "ExecutedAt";
                yield return "Message";
                yield return "Success";
                yield return "Microseconds";
                yield return "MetadataJson";
                yield return "HeartbeatId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "ExecutedAt";
                yield return "Message";
                yield return "Success";
                yield return "Microseconds";
                yield return "MetadataJson";
                yield return "HeartbeatId";
            }
        }
        
        // Validation
        
        public static void Validate(HeartbeatExecution entity)
        {
            entity.Message = entity.Message ?? String.Empty;
            entity.Message = entity.Message.Trim();
            entity.MetadataJson = entity.MetadataJson ?? String.Empty;
            entity.MetadataJson = entity.MetadataJson.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static HeartbeatExecutionRepositoryValidator Validator { get; } = new HeartbeatExecutionRepositoryValidator();
        
        
        public static void Message_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void MetadataJson_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class HeartbeatExecutionRepositoryValidator: AbstractValidator<HeartbeatExecution>
    {
        public HeartbeatExecutionRepositoryValidator()
        {
            RuleFor(x => x.Message).Use(HeartbeatExecutionMetadata.Message_ValidationRules);
            RuleFor(x => x.MetadataJson).Use(HeartbeatExecutionMetadata.MetadataJson_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

