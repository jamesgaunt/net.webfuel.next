using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ResearcherMetadata: IRepositoryMetadata<Researcher>
    {
        // Data Access
        
        public static string DatabaseTable => "Researcher";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Researcher DataReader(SqlDataReader dr) => new Researcher(dr);
        
        public static List<SqlParameter> ExtractParameters(Researcher entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Researcher.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Researcher.Id):
                        break;
                    case nameof(Researcher.Email):
                        result.Add(new SqlParameter(nameof(Researcher.Email), entity.Email));
                        break;
                    case nameof(Researcher.CreatedAt):
                        result.Add(new SqlParameter(nameof(Researcher.CreatedAt), entity.CreatedAt));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Researcher, ResearcherMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Researcher, ResearcherMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Researcher, ResearcherMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "CreatedAt";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "CreatedAt";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Email";
                yield return "CreatedAt";
            }
        }
        
        // Validation
        
        public static void Validate(Researcher entity)
        {
            entity.Email = entity.Email ?? String.Empty;
            entity.Email = entity.Email.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearcherRepositoryValidator Validator { get; } = new ResearcherRepositoryValidator();
        
        public const int Email_MaxLength = 64;
        
        public static void Email_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Email_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearcherRepositoryValidator: AbstractValidator<Researcher>
    {
        public ResearcherRepositoryValidator()
        {
            RuleFor(x => x.Email).Use(ResearcherMetadata.Email_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

