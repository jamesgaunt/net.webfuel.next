using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class HeartbeatMetadata: IRepositoryMetadata<Heartbeat>
    {
        // Data Access
        
        public static string DatabaseTable => "Heartbeat";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static Heartbeat DataReader(SqlDataReader dr) => new Heartbeat(dr);
        
        public static List<SqlParameter> ExtractParameters(Heartbeat entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Heartbeat.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Heartbeat.Id):
                        break;
                    case nameof(Heartbeat.Name):
                        result.Add(new SqlParameter(nameof(Heartbeat.Name), entity.Name));
                        break;
                    case nameof(Heartbeat.Live):
                        result.Add(new SqlParameter(nameof(Heartbeat.Live), entity.Live));
                        break;
                    case nameof(Heartbeat.SortOrder):
                        result.Add(new SqlParameter(nameof(Heartbeat.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Heartbeat.ProviderName):
                        result.Add(new SqlParameter(nameof(Heartbeat.ProviderName), entity.ProviderName));
                        break;
                    case nameof(Heartbeat.ProviderParameter):
                        result.Add(new SqlParameter(nameof(Heartbeat.ProviderParameter), entity.ProviderParameter));
                        break;
                    case nameof(Heartbeat.MinTime):
                        result.Add(new SqlParameter(nameof(Heartbeat.MinTime), entity.MinTime));
                        break;
                    case nameof(Heartbeat.MaxTime):
                        result.Add(new SqlParameter(nameof(Heartbeat.MaxTime), entity.MaxTime));
                        break;
                    case nameof(Heartbeat.Schedule):
                        result.Add(new SqlParameter(nameof(Heartbeat.Schedule), entity.Schedule));
                        break;
                    case nameof(Heartbeat.NextExecutionScheduledAt):
                        result.Add(new SqlParameter(nameof(Heartbeat.NextExecutionScheduledAt), entity.NextExecutionScheduledAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(Heartbeat.SchedulerExceptionMessage):
                        result.Add(new SqlParameter(nameof(Heartbeat.SchedulerExceptionMessage), entity.SchedulerExceptionMessage));
                        break;
                    case nameof(Heartbeat.LogSuccessfulExecutions):
                        result.Add(new SqlParameter(nameof(Heartbeat.LogSuccessfulExecutions), entity.LogSuccessfulExecutions));
                        break;
                    case nameof(Heartbeat.LastExecutionAt):
                        result.Add(new SqlParameter(nameof(Heartbeat.LastExecutionAt), entity.LastExecutionAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(Heartbeat.LastExecutionMessage):
                        result.Add(new SqlParameter(nameof(Heartbeat.LastExecutionMessage), entity.LastExecutionMessage));
                        break;
                    case nameof(Heartbeat.LastExecutionSuccess):
                        result.Add(new SqlParameter(nameof(Heartbeat.LastExecutionSuccess), entity.LastExecutionSuccess));
                        break;
                    case nameof(Heartbeat.LastExecutionMicroseconds):
                        result.Add(new SqlParameter(nameof(Heartbeat.LastExecutionMicroseconds), entity.LastExecutionMicroseconds));
                        break;
                    case nameof(Heartbeat.LastExecutionMetadataJson):
                        result.Add(new SqlParameter(nameof(Heartbeat.LastExecutionMetadataJson), entity.LastExecutionMetadataJson));
                        break;
                    case nameof(Heartbeat.RecentExecutionSuccessCount):
                        result.Add(new SqlParameter(nameof(Heartbeat.RecentExecutionSuccessCount), entity.RecentExecutionSuccessCount));
                        break;
                    case nameof(Heartbeat.RecentExecutionFailureCount):
                        result.Add(new SqlParameter(nameof(Heartbeat.RecentExecutionFailureCount), entity.RecentExecutionFailureCount));
                        break;
                    case nameof(Heartbeat.RecentExecutionMicrosecondsAverage):
                        result.Add(new SqlParameter(nameof(Heartbeat.RecentExecutionMicrosecondsAverage), entity.RecentExecutionMicrosecondsAverage));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Heartbeat, HeartbeatMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Heartbeat, HeartbeatMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Heartbeat, HeartbeatMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Live";
                yield return "SortOrder";
                yield return "ProviderName";
                yield return "ProviderParameter";
                yield return "MinTime";
                yield return "MaxTime";
                yield return "Schedule";
                yield return "NextExecutionScheduledAt";
                yield return "SchedulerExceptionMessage";
                yield return "LogSuccessfulExecutions";
                yield return "LastExecutionAt";
                yield return "LastExecutionMessage";
                yield return "LastExecutionSuccess";
                yield return "LastExecutionMicroseconds";
                yield return "LastExecutionMetadataJson";
                yield return "RecentExecutionSuccessCount";
                yield return "RecentExecutionFailureCount";
                yield return "RecentExecutionMicrosecondsAverage";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Live";
                yield return "SortOrder";
                yield return "ProviderName";
                yield return "ProviderParameter";
                yield return "MinTime";
                yield return "MaxTime";
                yield return "Schedule";
                yield return "NextExecutionScheduledAt";
                yield return "SchedulerExceptionMessage";
                yield return "LogSuccessfulExecutions";
                yield return "LastExecutionAt";
                yield return "LastExecutionMessage";
                yield return "LastExecutionSuccess";
                yield return "LastExecutionMicroseconds";
                yield return "LastExecutionMetadataJson";
                yield return "RecentExecutionSuccessCount";
                yield return "RecentExecutionFailureCount";
                yield return "RecentExecutionMicrosecondsAverage";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Live";
                yield return "SortOrder";
                yield return "ProviderName";
                yield return "ProviderParameter";
                yield return "MinTime";
                yield return "MaxTime";
                yield return "Schedule";
                yield return "NextExecutionScheduledAt";
                yield return "SchedulerExceptionMessage";
                yield return "LogSuccessfulExecutions";
                yield return "LastExecutionAt";
                yield return "LastExecutionMessage";
                yield return "LastExecutionSuccess";
                yield return "LastExecutionMicroseconds";
                yield return "LastExecutionMetadataJson";
                yield return "RecentExecutionSuccessCount";
                yield return "RecentExecutionFailureCount";
                yield return "RecentExecutionMicrosecondsAverage";
            }
        }
        
        // Validation
        
        public static void Validate(Heartbeat entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.ProviderName = entity.ProviderName ?? String.Empty;
            entity.ProviderName = entity.ProviderName.Trim();
            entity.ProviderParameter = entity.ProviderParameter ?? String.Empty;
            entity.ProviderParameter = entity.ProviderParameter.Trim();
            entity.MinTime = entity.MinTime ?? String.Empty;
            entity.MinTime = entity.MinTime.Trim();
            entity.MaxTime = entity.MaxTime ?? String.Empty;
            entity.MaxTime = entity.MaxTime.Trim();
            entity.Schedule = entity.Schedule ?? String.Empty;
            entity.Schedule = entity.Schedule.Trim();
            entity.SchedulerExceptionMessage = entity.SchedulerExceptionMessage ?? String.Empty;
            entity.SchedulerExceptionMessage = entity.SchedulerExceptionMessage.Trim();
            entity.LastExecutionMessage = entity.LastExecutionMessage ?? String.Empty;
            entity.LastExecutionMessage = entity.LastExecutionMessage.Trim();
            entity.LastExecutionMetadataJson = entity.LastExecutionMetadataJson ?? String.Empty;
            entity.LastExecutionMetadataJson = entity.LastExecutionMetadataJson.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static HeartbeatRepositoryValidator Validator { get; } = new HeartbeatRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        public const int ProviderName_MaxLength = 128;
        public const int ProviderParameter_MaxLength = 128;
        public const int MinTime_MaxLength = 128;
        public const int MaxTime_MaxLength = 128;
        public const int Schedule_MaxLength = 128;
        public const int SchedulerExceptionMessage_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProviderName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProviderName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProviderParameter_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProviderParameter_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void MinTime_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(MinTime_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void MaxTime_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(MaxTime_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Schedule_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Schedule_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SchedulerExceptionMessage_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SchedulerExceptionMessage_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LastExecutionMessage_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void LastExecutionMetadataJson_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class HeartbeatRepositoryValidator: AbstractValidator<Heartbeat>
    {
        public HeartbeatRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(HeartbeatMetadata.Name_ValidationRules);
            RuleFor(x => x.ProviderName).Use(HeartbeatMetadata.ProviderName_ValidationRules);
            RuleFor(x => x.ProviderParameter).Use(HeartbeatMetadata.ProviderParameter_ValidationRules);
            RuleFor(x => x.MinTime).Use(HeartbeatMetadata.MinTime_ValidationRules);
            RuleFor(x => x.MaxTime).Use(HeartbeatMetadata.MaxTime_ValidationRules);
            RuleFor(x => x.Schedule).Use(HeartbeatMetadata.Schedule_ValidationRules);
            RuleFor(x => x.SchedulerExceptionMessage).Use(HeartbeatMetadata.SchedulerExceptionMessage_ValidationRules);
            RuleFor(x => x.LastExecutionMessage).Use(HeartbeatMetadata.LastExecutionMessage_ValidationRules);
            RuleFor(x => x.LastExecutionMetadataJson).Use(HeartbeatMetadata.LastExecutionMetadataJson_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

