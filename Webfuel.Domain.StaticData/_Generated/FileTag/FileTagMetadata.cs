using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FileTagMetadata: IRepositoryMetadata<FileTag>
    {
        // Data Access
        
        public static string DatabaseTable => "FileTag";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static FileTag DataReader(SqlDataReader dr) => new FileTag(dr);
        
        public static List<SqlParameter> ExtractParameters(FileTag entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FileTag.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FileTag.Id):
                        break;
                    case nameof(FileTag.Name):
                        result.Add(new SqlParameter(nameof(FileTag.Name), entity.Name));
                        break;
                    case nameof(FileTag.SortOrder):
                        result.Add(new SqlParameter(nameof(FileTag.SortOrder), entity.SortOrder));
                        break;
                    case nameof(FileTag.Default):
                        result.Add(new SqlParameter(nameof(FileTag.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FileTag, FileTagMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FileTag, FileTagMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FileTag, FileTagMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
            }
        }
        
        // Validation
        
        public static void Validate(FileTag entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FileTagRepositoryValidator Validator { get; } = new FileTagRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class FileTagRepositoryValidator: AbstractValidator<FileTag>
    {
        public FileTagRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(FileTagMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

