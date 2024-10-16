using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class TriageTemplateMetadata: IRepositoryMetadata<TriageTemplate>
    {
        // Data Access
        
        public static string DatabaseTable => "TriageTemplate";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static TriageTemplate DataReader(SqlDataReader dr) => new TriageTemplate(dr);
        
        public static List<SqlParameter> ExtractParameters(TriageTemplate entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(TriageTemplate.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(TriageTemplate.Id):
                        break;
                    case nameof(TriageTemplate.Name):
                        result.Add(new SqlParameter(nameof(TriageTemplate.Name), entity.Name));
                        break;
                    case nameof(TriageTemplate.SortOrder):
                        result.Add(new SqlParameter(nameof(TriageTemplate.SortOrder), entity.SortOrder));
                        break;
                    case nameof(TriageTemplate.Subject):
                        result.Add(new SqlParameter(nameof(TriageTemplate.Subject), entity.Subject));
                        break;
                    case nameof(TriageTemplate.HtmlTemplate):
                        result.Add(new SqlParameter(nameof(TriageTemplate.HtmlTemplate), entity.HtmlTemplate));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<TriageTemplate, TriageTemplateMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<TriageTemplate, TriageTemplateMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<TriageTemplate, TriageTemplateMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Subject";
                yield return "HtmlTemplate";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Subject";
                yield return "HtmlTemplate";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SortOrder";
                yield return "Subject";
                yield return "HtmlTemplate";
            }
        }
        
        // Validation
        
        public static void Validate(TriageTemplate entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Subject = entity.Subject ?? String.Empty;
            entity.Subject = entity.Subject.Trim();
            entity.HtmlTemplate = entity.HtmlTemplate ?? String.Empty;
            entity.HtmlTemplate = entity.HtmlTemplate.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static TriageTemplateRepositoryValidator Validator { get; } = new TriageTemplateRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Subject_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void HtmlTemplate_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class TriageTemplateRepositoryValidator: AbstractValidator<TriageTemplate>
    {
        public TriageTemplateRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(TriageTemplateMetadata.Name_ValidationRules);
            RuleFor(x => x.Subject).Use(TriageTemplateMetadata.Subject_ValidationRules);
            RuleFor(x => x.HtmlTemplate).Use(TriageTemplateMetadata.HtmlTemplate_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

