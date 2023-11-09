using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class FileStorageEntryMetadata: IRepositoryMetadata<FileStorageEntry>
    {
        // Data Access
        
        public static string DatabaseTable => "FileStorageEntry";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static FileStorageEntry DataReader(SqlDataReader dr) => new FileStorageEntry(dr);
        
        public static List<SqlParameter> ExtractParameters(FileStorageEntry entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FileStorageEntry.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FileStorageEntry.Id):
                        break;
                    case nameof(FileStorageEntry.FileName):
                        result.Add(new SqlParameter(nameof(FileStorageEntry.FileName), entity.FileName));
                        break;
                    case nameof(FileStorageEntry.SizeBytes):
                        result.Add(new SqlParameter(nameof(FileStorageEntry.SizeBytes), entity.SizeBytes));
                        break;
                    case nameof(FileStorageEntry.UploadedAt):
                        result.Add(new SqlParameter(nameof(FileStorageEntry.UploadedAt), entity.UploadedAt ?? (object?)DBNull.Value));
                        break;
                    case nameof(FileStorageEntry.UploadedBy):
                        result.Add(new SqlParameter(nameof(FileStorageEntry.UploadedBy), entity.UploadedBy));
                        break;
                    case nameof(FileStorageEntry.Description):
                        result.Add(new SqlParameter(nameof(FileStorageEntry.Description), entity.Description));
                        break;
                    case nameof(FileStorageEntry.FileStorageGroupId):
                        result.Add(new SqlParameter(nameof(FileStorageEntry.FileStorageGroupId), entity.FileStorageGroupId));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FileStorageEntry, FileStorageEntryMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FileStorageEntry, FileStorageEntryMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FileStorageEntry, FileStorageEntryMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "FileName";
                yield return "SizeBytes";
                yield return "UploadedAt";
                yield return "UploadedBy";
                yield return "Description";
                yield return "FileStorageGroupId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "FileName";
                yield return "SizeBytes";
                yield return "UploadedAt";
                yield return "UploadedBy";
                yield return "Description";
                yield return "FileStorageGroupId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "FileName";
                yield return "SizeBytes";
                yield return "UploadedAt";
                yield return "UploadedBy";
                yield return "Description";
                yield return "FileStorageGroupId";
            }
        }
        
        // Validation
        
        public static void Validate(FileStorageEntry entity)
        {
            entity.FileName = entity.FileName ?? String.Empty;
            entity.FileName = entity.FileName.Trim();
            entity.UploadedBy = entity.UploadedBy ?? String.Empty;
            entity.UploadedBy = entity.UploadedBy.Trim();
            entity.Description = entity.Description ?? String.Empty;
            entity.Description = entity.Description.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FileStorageEntryRepositoryValidator Validator { get; } = new FileStorageEntryRepositoryValidator();
        
        public const int FileName_MaxLength = 64;
        public const int UploadedBy_MaxLength = 64;
        public const int Description_MaxLength = 64;
        
        public static void FileName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FileName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void UploadedBy_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(UploadedBy_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Description_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Description_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class FileStorageEntryRepositoryValidator: AbstractValidator<FileStorageEntry>
        {
            public FileStorageEntryRepositoryValidator()
            {
                RuleFor(x => x.FileName).Use(FileName_ValidationRules);
                RuleFor(x => x.UploadedBy).Use(UploadedBy_ValidationRules);
                RuleFor(x => x.Description).Use(Description_ValidationRules);
            }
        }
    }
}

