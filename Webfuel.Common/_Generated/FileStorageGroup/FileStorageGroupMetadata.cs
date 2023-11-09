using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class FileStorageGroupMetadata: IRepositoryMetadata<FileStorageGroup>
    {
        // Data Access
        
        public static string DatabaseTable => "FileStorageGroup";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static FileStorageGroup DataReader(SqlDataReader dr) => new FileStorageGroup(dr);
        
        public static List<SqlParameter> ExtractParameters(FileStorageGroup entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FileStorageGroup.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FileStorageGroup.Id):
                        break;
                    case nameof(FileStorageGroup.CreatedAt):
                        result.Add(new SqlParameter(nameof(FileStorageGroup.CreatedAt), entity.CreatedAt));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FileStorageGroup, FileStorageGroupMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FileStorageGroup, FileStorageGroupMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FileStorageGroup, FileStorageGroupMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "CreatedAt";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "CreatedAt";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "CreatedAt";
            }
        }
        
        // Validation
        
        public static void Validate(FileStorageGroup entity)
        {
            Validator.ValidateAndThrow(entity);
        }
        
        public static FileStorageGroupRepositoryValidator Validator { get; } = new FileStorageGroupRepositoryValidator();
        
        
        public class FileStorageGroupRepositoryValidator: AbstractValidator<FileStorageGroup>
        {
            public FileStorageGroupRepositoryValidator()
            {
            }
        }
    }
}

