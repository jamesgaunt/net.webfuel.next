using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectMetadata: IRepositoryMetadata<Project>
    {
        // Data Access
        
        public static string DatabaseTable => "Project";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Project DataReader(SqlDataReader dr) => new Project(dr);
        
        public static List<SqlParameter> ExtractParameters(Project entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Project.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Project.Id):
                        break;
                    case nameof(Project.LinkId):
                        result.Add(new SqlParameter(nameof(Project.LinkId), entity.LinkId));
                        break;
                    case nameof(Project.Number):
                        result.Add(new SqlParameter(nameof(Project.Number), entity.Number));
                        break;
                    case nameof(Project.Title):
                        result.Add(new SqlParameter(nameof(Project.Title), entity.Title));
                        break;
                    case nameof(Project.FundingBodyId):
                        result.Add(new SqlParameter(nameof(Project.FundingBodyId), entity.FundingBodyId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ResearchMethodologyId):
                        result.Add(new SqlParameter(nameof(Project.ResearchMethodologyId), entity.ResearchMethodologyId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Project, ProjectMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Project, ProjectMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Project, ProjectMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "LinkId";
                yield return "Number";
                yield return "Title";
                yield return "FundingBodyId";
                yield return "ResearchMethodologyId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "LinkId";
                yield return "Number";
                yield return "Title";
                yield return "FundingBodyId";
                yield return "ResearchMethodologyId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "LinkId";
                yield return "Number";
                yield return "Title";
                yield return "FundingBodyId";
                yield return "ResearchMethodologyId";
            }
        }
        
        // Validation
        
        public static void Validate(Project entity)
        {
            entity.Title = entity.Title ?? String.Empty;
            entity.Title = entity.Title.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectRepositoryValidator Validator { get; } = new ProjectRepositoryValidator();
        
        public const int Title_MaxLength = 1000;
        
        public static void Title_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Title_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ProjectRepositoryValidator: AbstractValidator<Project>
        {
            public ProjectRepositoryValidator()
            {
                RuleFor(x => x.Title).Use(Title_ValidationRules);
            }
        }
    }
}

