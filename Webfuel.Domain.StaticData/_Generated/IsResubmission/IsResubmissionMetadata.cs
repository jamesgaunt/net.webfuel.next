using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsResubmissionMetadata: IRepositoryMetadata<IsResubmission>
    {
        // Data Access
        
        public static string DatabaseTable => "IsResubmission";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsResubmission DataReader(SqlDataReader dr) => new IsResubmission(dr);
        
        public static List<SqlParameter> ExtractParameters(IsResubmission entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsResubmission.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsResubmission.Id):
                        break;
                    case nameof(IsResubmission.Name):
                        result.Add(new SqlParameter(nameof(IsResubmission.Name), entity.Name));
                        break;
                    case nameof(IsResubmission.SortOrder):
                        result.Add(new SqlParameter(nameof(IsResubmission.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsResubmission.Default):
                        result.Add(new SqlParameter(nameof(IsResubmission.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsResubmission, IsResubmissionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsResubmission, IsResubmissionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsResubmission, IsResubmissionMetadata>();
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
        
        public static void Validate(IsResubmission entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsResubmissionRepositoryValidator Validator { get; } = new IsResubmissionRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsResubmissionRepositoryValidator: AbstractValidator<IsResubmission>
    {
        public IsResubmissionRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsResubmissionMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

