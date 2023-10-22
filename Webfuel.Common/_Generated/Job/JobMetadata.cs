using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class JobMetadata: IRepositoryMetadata<Job>
    {
        // Data Access
        
        public static string DatabaseTable => "Job";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Job DataReader(SqlDataReader dr) => new Job(dr);
        
        public static List<SqlParameter> ExtractParameters(Job entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Job.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Job.Id):
                        break;
                    case nameof(Job.Name):
                        result.Add(new SqlParameter(nameof(Job.Name), entity.Name));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Job, JobMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Job, JobMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Job, JobMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
            }
        }
        
        // Validation
        
        public static void Validate(Job entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static JobRepositoryValidator Validator { get; } = new JobRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class JobRepositoryValidator: AbstractValidator<Job>
        {
            public JobRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

