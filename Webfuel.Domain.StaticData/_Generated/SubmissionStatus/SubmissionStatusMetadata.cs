using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SubmissionStatusMetadata: IRepositoryMetadata<SubmissionStatus>
    {
        // Data Access
        
        public static string DatabaseTable => "SubmissionStatus";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SubmissionStatus DataReader(SqlDataReader dr) => new SubmissionStatus(dr);
        
        public static List<SqlParameter> ExtractParameters(SubmissionStatus entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SubmissionStatus.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SubmissionStatus.Id):
                        break;
                    case nameof(SubmissionStatus.Name):
                        result.Add(new SqlParameter(nameof(SubmissionStatus.Name), entity.Name));
                        break;
                    case nameof(SubmissionStatus.SortOrder):
                        result.Add(new SqlParameter(nameof(SubmissionStatus.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SubmissionStatus.Default):
                        result.Add(new SqlParameter(nameof(SubmissionStatus.Default), entity.Default));
                        break;
                    case nameof(SubmissionStatus.Hidden):
                        result.Add(new SqlParameter(nameof(SubmissionStatus.Hidden), entity.Hidden));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SubmissionStatus, SubmissionStatusMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SubmissionStatus, SubmissionStatusMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SubmissionStatus, SubmissionStatusMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
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
                yield return "Hidden";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
            }
        }
        
        // Validation
        
        public static void Validate(SubmissionStatus entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SubmissionStatusRepositoryValidator Validator { get; } = new SubmissionStatusRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class SubmissionStatusRepositoryValidator: AbstractValidator<SubmissionStatus>
    {
        public SubmissionStatusRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(SubmissionStatusMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

