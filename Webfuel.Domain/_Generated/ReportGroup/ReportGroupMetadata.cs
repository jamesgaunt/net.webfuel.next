using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ReportGroupMetadata: IRepositoryMetadata<ReportGroup>
    {
        // Data Access
        
        public static string DatabaseTable => "ReportGroup";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ReportGroup DataReader(SqlDataReader dr) => new ReportGroup(dr);
        
        public static List<SqlParameter> ExtractParameters(ReportGroup entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ReportGroup.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ReportGroup.Id):
                        break;
                    case nameof(ReportGroup.Name):
                        result.Add(new SqlParameter(nameof(ReportGroup.Name), entity.Name));
                        break;
                    case nameof(ReportGroup.SortOrder):
                        result.Add(new SqlParameter(nameof(ReportGroup.SortOrder), entity.SortOrder));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ReportGroup, ReportGroupMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ReportGroup, ReportGroupMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ReportGroup, ReportGroupMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SortOrder";
            }
        }
        
        // Validation
        
        public static void Validate(ReportGroup entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ReportGroupRepositoryValidator Validator { get; } = new ReportGroupRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ReportGroupRepositoryValidator: AbstractValidator<ReportGroup>
    {
        public ReportGroupRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ReportGroupMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

