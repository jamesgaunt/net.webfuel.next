using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSupportGroupMetadata: IRepositoryMetadata<ProjectSupportGroup>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectSupportGroup";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static ProjectSupportGroup DataReader(SqlDataReader dr) => new ProjectSupportGroup(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectSupportGroup entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectSupportGroup.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectSupportGroup.Id):
                        break;
                    case nameof(ProjectSupportGroup.CreatedAt):
                        result.Add(new SqlParameter(nameof(ProjectSupportGroup.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(ProjectSupportGroup.FileStorageGroupId):
                        result.Add(new SqlParameter(nameof(ProjectSupportGroup.FileStorageGroupId), entity.FileStorageGroupId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectSupportGroup, ProjectSupportGroupMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectSupportGroup, ProjectSupportGroupMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectSupportGroup, ProjectSupportGroupMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectSupportGroup entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectSupportGroupRepositoryValidator Validator { get; } = new ProjectSupportGroupRepositoryValidator();
        
    }
    
    public partial class ProjectSupportGroupRepositoryValidator: AbstractValidator<ProjectSupportGroup>
    {
        public ProjectSupportGroupRepositoryValidator()
        {
            Validation();
        }
        
        partial void Validation();
    }
}

