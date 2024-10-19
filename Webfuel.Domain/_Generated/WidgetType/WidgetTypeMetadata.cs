using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class WidgetTypeMetadata: IRepositoryMetadata<WidgetType>
    {
        // Data Access
        
        public static string DatabaseTable => "WidgetType";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static WidgetType DataReader(SqlDataReader dr) => new WidgetType(dr);
        
        public static List<SqlParameter> ExtractParameters(WidgetType entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(WidgetType.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(WidgetType.Id):
                        break;
                    case nameof(WidgetType.Name):
                        result.Add(new SqlParameter(nameof(WidgetType.Name), entity.Name));
                        break;
                    case nameof(WidgetType.SortOrder):
                        result.Add(new SqlParameter(nameof(WidgetType.SortOrder), entity.SortOrder));
                        break;
                    case nameof(WidgetType.Description):
                        result.Add(new SqlParameter(nameof(WidgetType.Description), entity.Description));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<WidgetType, WidgetTypeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<WidgetType, WidgetTypeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<WidgetType, WidgetTypeMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Description";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Description";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SortOrder";
                yield return "Description";
            }
        }
        
        // Validation
        
        public static void Validate(WidgetType entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description ?? String.Empty;
            entity.Description = entity.Description.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static WidgetTypeRepositoryValidator Validator { get; } = new WidgetTypeRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        public const int Description_MaxLength = 1024;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Description_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Description_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class WidgetTypeRepositoryValidator: AbstractValidator<WidgetType>
    {
        public WidgetTypeRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(WidgetTypeMetadata.Name_ValidationRules);
            RuleFor(x => x.Description).Use(WidgetTypeMetadata.Description_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

