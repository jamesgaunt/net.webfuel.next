using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class ConfigurationMetadata: IRepositoryMetadata<Configuration>
    {
        // Data Access
        
        public static string DatabaseTable => "Configuration";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Configuration DataReader(SqlDataReader dr) => new Configuration(dr);
        
        public static List<SqlParameter> ExtractParameters(Configuration entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Configuration.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Configuration.Id):
                        break;
                    case nameof(Configuration.Prefix):
                        result.Add(new SqlParameter(nameof(Configuration.Prefix), entity.Prefix));
                        break;
                    case nameof(Configuration.NextProjectNumber):
                        result.Add(new SqlParameter(nameof(Configuration.NextProjectNumber), entity.NextProjectNumber));
                        break;
                    case nameof(Configuration.NextSupportRequestNumber):
                        result.Add(new SqlParameter(nameof(Configuration.NextSupportRequestNumber), entity.NextSupportRequestNumber));
                        break;
                    case nameof(Configuration.DomainName):
                        result.Add(new SqlParameter(nameof(Configuration.DomainName), entity.DomainName));
                        break;
                    case nameof(Configuration.ReplyTo):
                        result.Add(new SqlParameter(nameof(Configuration.ReplyTo), entity.ReplyTo));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Configuration, ConfigurationMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Configuration, ConfigurationMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Configuration, ConfigurationMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Prefix";
                yield return "NextProjectNumber";
                yield return "NextSupportRequestNumber";
                yield return "DomainName";
                yield return "ReplyTo";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Prefix";
                yield return "NextProjectNumber";
                yield return "NextSupportRequestNumber";
                yield return "DomainName";
                yield return "ReplyTo";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Prefix";
                yield return "NextProjectNumber";
                yield return "NextSupportRequestNumber";
                yield return "DomainName";
                yield return "ReplyTo";
            }
        }
        
        // Validation
        
        public static void Validate(Configuration entity)
        {
            entity.Prefix = entity.Prefix ?? String.Empty;
            entity.Prefix = entity.Prefix.Trim();
            entity.DomainName = entity.DomainName ?? String.Empty;
            entity.DomainName = entity.DomainName.Trim();
            entity.ReplyTo = entity.ReplyTo ?? String.Empty;
            entity.ReplyTo = entity.ReplyTo.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ConfigurationRepositoryValidator Validator { get; } = new ConfigurationRepositoryValidator();
        
        public const int Prefix_MaxLength = 64;
        public const int DomainName_MaxLength = 64;
        public const int ReplyTo_MaxLength = 64;
        
        public static void Prefix_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Prefix_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void DomainName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(DomainName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ReplyTo_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ReplyTo_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ConfigurationRepositoryValidator: AbstractValidator<Configuration>
    {
        public ConfigurationRepositoryValidator()
        {
            RuleFor(x => x.Prefix).Use(ConfigurationMetadata.Prefix_ValidationRules);
            RuleFor(x => x.DomainName).Use(ConfigurationMetadata.DomainName_ValidationRules);
            RuleFor(x => x.ReplyTo).Use(ConfigurationMetadata.ReplyTo_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

