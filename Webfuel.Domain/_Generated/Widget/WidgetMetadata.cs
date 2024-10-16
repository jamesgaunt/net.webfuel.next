using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class WidgetMetadata: IRepositoryMetadata<Widget>
    {
        // Data Access
        
        public static string DatabaseTable => "Widget";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static Widget DataReader(SqlDataReader dr) => new Widget(dr);
        
        public static List<SqlParameter> ExtractParameters(Widget entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Widget.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Widget.Id):
                        break;
                    case nameof(Widget.SortOrder):
                        result.Add(new SqlParameter(nameof(Widget.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Widget.ConfigJson):
                        result.Add(new SqlParameter(nameof(Widget.ConfigJson), entity.ConfigJson));
                        break;
                    case nameof(Widget.HeaderText):
                        result.Add(new SqlParameter(nameof(Widget.HeaderText), entity.HeaderText));
                        break;
                    case nameof(Widget.DataJson):
                        result.Add(new SqlParameter(nameof(Widget.DataJson), entity.DataJson));
                        break;
                    case nameof(Widget.DataVersion):
                        result.Add(new SqlParameter(nameof(Widget.DataVersion), entity.DataVersion));
                        break;
                    case nameof(Widget.DataCurrent):
                        result.Add(new SqlParameter(nameof(Widget.DataCurrent), entity.DataCurrent));
                        break;
                    case nameof(Widget.DataTimestamp):
                        result.Add(new SqlParameter(nameof(Widget.DataTimestamp), entity.DataTimestamp));
                        break;
                    case nameof(Widget.UserId):
                        result.Add(new SqlParameter(nameof(Widget.UserId), entity.UserId));
                        break;
                    case nameof(Widget.WidgetTypeId):
                        result.Add(new SqlParameter(nameof(Widget.WidgetTypeId), entity.WidgetTypeId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Widget, WidgetMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Widget, WidgetMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Widget, WidgetMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "SortOrder";
                yield return "ConfigJson";
                yield return "HeaderText";
                yield return "DataJson";
                yield return "DataVersion";
                yield return "DataCurrent";
                yield return "DataTimestamp";
                yield return "UserId";
                yield return "WidgetTypeId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "SortOrder";
                yield return "ConfigJson";
                yield return "HeaderText";
                yield return "DataJson";
                yield return "DataVersion";
                yield return "DataCurrent";
                yield return "DataTimestamp";
                yield return "UserId";
                yield return "WidgetTypeId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "SortOrder";
                yield return "ConfigJson";
                yield return "HeaderText";
                yield return "DataJson";
                yield return "DataVersion";
                yield return "DataCurrent";
                yield return "DataTimestamp";
                yield return "UserId";
                yield return "WidgetTypeId";
            }
        }
        
        // Validation
        
        public static void Validate(Widget entity)
        {
            entity.ConfigJson = entity.ConfigJson ?? String.Empty;
            entity.ConfigJson = entity.ConfigJson.Trim();
            entity.HeaderText = entity.HeaderText ?? String.Empty;
            entity.HeaderText = entity.HeaderText.Trim();
            entity.DataJson = entity.DataJson ?? String.Empty;
            entity.DataJson = entity.DataJson.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static WidgetRepositoryValidator Validator { get; } = new WidgetRepositoryValidator();
        
        public const int HeaderText_MaxLength = 128;
        
        public static void ConfigJson_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void HeaderText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(HeaderText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void DataJson_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class WidgetRepositoryValidator: AbstractValidator<Widget>
    {
        public WidgetRepositoryValidator()
        {
            RuleFor(x => x.ConfigJson).Use(WidgetMetadata.ConfigJson_ValidationRules);
            RuleFor(x => x.HeaderText).Use(WidgetMetadata.HeaderText_ValidationRules);
            RuleFor(x => x.DataJson).Use(WidgetMetadata.DataJson_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

