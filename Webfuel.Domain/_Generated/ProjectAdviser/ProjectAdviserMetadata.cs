using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectAdviserMetadata: IRepositoryMetadata<ProjectAdviser>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectAdviser";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ProjectAdviser DataReader(SqlDataReader dr) => new ProjectAdviser(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectAdviser entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectAdviser.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectAdviser.Id):
                        break;
                    case nameof(ProjectAdviser.SortOrder):
                        result.Add(new SqlParameter(nameof(ProjectAdviser.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ProjectAdviser.ProjectId):
                        result.Add(new SqlParameter(nameof(ProjectAdviser.ProjectId), entity.ProjectId));
                        break;
                    case nameof(ProjectAdviser.UserId):
                        result.Add(new SqlParameter(nameof(ProjectAdviser.UserId), entity.UserId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectAdviser, ProjectAdviserMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectAdviser, ProjectAdviserMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectAdviser, ProjectAdviserMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "SortOrder";
                yield return "ProjectId";
                yield return "UserId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "SortOrder";
                yield return "ProjectId";
                yield return "UserId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "SortOrder";
                yield return "ProjectId";
                yield return "UserId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectAdviser entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectAdviserRepositoryValidator Validator { get; } = new ProjectAdviserRepositoryValidator();
        
    }
    
    public partial class ProjectAdviserRepositoryValidator: AbstractValidator<ProjectAdviser>
    {
        public ProjectAdviserRepositoryValidator()
        {
            Validation();
        }
        
        partial void Validation();
    }
}

