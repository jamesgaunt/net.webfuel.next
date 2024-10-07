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
                yield return "UserId";
                yield return "WidgetTypeId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "SortOrder";
                yield return "UserId";
                yield return "WidgetTypeId";
            }
        }
        
        // Validation
        
        public static void Validate(Widget entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static WidgetRepositoryValidator Validator { get; } = new WidgetRepositoryValidator();
        
    }
    
    public partial class WidgetRepositoryValidator: AbstractValidator<Widget>
    {
        public WidgetRepositoryValidator()
        {
            Validation();
        }
        
        partial void Validation();
    }
}

