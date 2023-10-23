using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectSubmissionMetadata: IRepositoryMetadata<ProjectSubmission>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectSubmission";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static ProjectSubmission DataReader(SqlDataReader dr) => new ProjectSubmission(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectSubmission entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectSubmission.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectSubmission.Id):
                        break;
                    case nameof(ProjectSubmission.ProjectId):
                        result.Add(new SqlParameter(nameof(ProjectSubmission.ProjectId), entity.ProjectId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectSubmission, ProjectSubmissionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectSubmission, ProjectSubmissionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectSubmission, ProjectSubmissionMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "ProjectId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "ProjectId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "ProjectId";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectSubmission entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectSubmissionRepositoryValidator Validator { get; } = new ProjectSubmissionRepositoryValidator();
        
        
        public class ProjectSubmissionRepositoryValidator: AbstractValidator<ProjectSubmission>
        {
            public ProjectSubmissionRepositoryValidator()
            {
            }
        }
    }
}

