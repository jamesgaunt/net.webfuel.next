using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class OutlineSubmissionStatusMetadata: IRepositoryMetadata<OutlineSubmissionStatus>
    {
        // Data Access
        
        public static string DatabaseTable => "OutlineSubmissionStatus";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static OutlineSubmissionStatus DataReader(SqlDataReader dr) => new OutlineSubmissionStatus(dr);
        
        public static List<SqlParameter> ExtractParameters(OutlineSubmissionStatus entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(OutlineSubmissionStatus.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(OutlineSubmissionStatus.Id):
                        break;
                    case nameof(OutlineSubmissionStatus.Name):
                        result.Add(new SqlParameter(nameof(OutlineSubmissionStatus.Name), entity.Name));
                        break;
                    case nameof(OutlineSubmissionStatus.SortOrder):
                        result.Add(new SqlParameter(nameof(OutlineSubmissionStatus.SortOrder), entity.SortOrder));
                        break;
                    case nameof(OutlineSubmissionStatus.Default):
                        result.Add(new SqlParameter(nameof(OutlineSubmissionStatus.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<OutlineSubmissionStatus, OutlineSubmissionStatusMetadata>();
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
        
        public static void Validate(OutlineSubmissionStatus entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static OutlineSubmissionStatusRepositoryValidator Validator { get; } = new OutlineSubmissionStatusRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class OutlineSubmissionStatusRepositoryValidator: AbstractValidator<OutlineSubmissionStatus>
    {
        public OutlineSubmissionStatusRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(OutlineSubmissionStatusMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

