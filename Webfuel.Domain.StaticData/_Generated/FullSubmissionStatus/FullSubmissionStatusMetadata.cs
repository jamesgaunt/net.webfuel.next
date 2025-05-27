using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FullSubmissionStatusMetadata: IRepositoryMetadata<FullSubmissionStatus>
    {
        // Data Access
        
        public static string DatabaseTable => "FullSubmissionStatus";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static FullSubmissionStatus DataReader(SqlDataReader dr) => new FullSubmissionStatus(dr);
        
        public static List<SqlParameter> ExtractParameters(FullSubmissionStatus entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FullSubmissionStatus.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FullSubmissionStatus.Id):
                        break;
                    case nameof(FullSubmissionStatus.Name):
                        result.Add(new SqlParameter(nameof(FullSubmissionStatus.Name), entity.Name));
                        break;
                    case nameof(FullSubmissionStatus.SortOrder):
                        result.Add(new SqlParameter(nameof(FullSubmissionStatus.SortOrder), entity.SortOrder));
                        break;
                    case nameof(FullSubmissionStatus.Default):
                        result.Add(new SqlParameter(nameof(FullSubmissionStatus.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FullSubmissionStatus, FullSubmissionStatusMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FullSubmissionStatus, FullSubmissionStatusMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FullSubmissionStatus, FullSubmissionStatusMetadata>();
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
        
        public static void Validate(FullSubmissionStatus entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FullSubmissionStatusRepositoryValidator Validator { get; } = new FullSubmissionStatusRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class FullSubmissionStatusRepositoryValidator: AbstractValidator<FullSubmissionStatus>
    {
        public FullSubmissionStatusRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(FullSubmissionStatusMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

