using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ReportMetadata: IRepositoryMetadata<Report>
    {
        // Data Access
        
        public static string DatabaseTable => "Report";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Report DataReader(SqlDataReader dr) => new Report(dr);
        
        public static List<SqlParameter> ExtractParameters(Report entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Report.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Report.Id):
                        break;
                    case nameof(Report.Name):
                        result.Add(new SqlParameter(nameof(Report.Name), entity.Name));
                        break;
                    case nameof(Report.SortOrder):
                        result.Add(new SqlParameter(nameof(Report.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Report.ReportingContextId):
                        result.Add(new SqlParameter(nameof(Report.ReportingContextId), entity.ReportingContextId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Report, ReportMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Report, ReportMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Report, ReportMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "ReportingContextId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "ReportingContextId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SortOrder";
                yield return "ReportingContextId";
            }
        }
        
        // Validation
        
        public static void Validate(Report entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ReportRepositoryValidator Validator { get; } = new ReportRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ReportRepositoryValidator: AbstractValidator<Report>
        {
            public ReportRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}
