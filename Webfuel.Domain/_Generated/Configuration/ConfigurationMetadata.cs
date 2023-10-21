using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
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
                    case nameof(Configuration.NextProjectNumber):
                        result.Add(new SqlParameter(nameof(Configuration.NextProjectNumber), entity.NextProjectNumber));
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
                yield return "NextProjectNumber";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "NextProjectNumber";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "NextProjectNumber";
            }
        }
        
        // Validation
        
        public static void Validate(Configuration entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static ConfigurationRepositoryValidator Validator { get; } = new ConfigurationRepositoryValidator();
        
        
        public class ConfigurationRepositoryValidator: AbstractValidator<Configuration>
        {
            public ConfigurationRepositoryValidator()
            {
            }
        }
    }
}

