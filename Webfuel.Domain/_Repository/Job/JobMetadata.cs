using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class JobMetadata: IRepositoryMetadata<Job>
    {
        // Data Access
        
        public static string DatabaseTable => "Job";
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Job DataReader(SqlDataReader dr) => new Job(dr);
        
        public static  IEnumerable<SqlParameter> DataWriter(Job entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter>();
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Job.Id):
                        result.Add(new SqlParameter(nameof(Job.Id), entity.Id));
                        break;
                    case nameof(Job.Name):
                        result.Add(new SqlParameter(nameof(Job.Name), entity.Name));
                        break;
                }
            }
            return result;
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
        
        public static void Name<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class JobRepositoryValidator: AbstractValidator<Job>
        {
            public JobRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(JobRepositoryValidationRules.Name);
            }
        }
    }
}

