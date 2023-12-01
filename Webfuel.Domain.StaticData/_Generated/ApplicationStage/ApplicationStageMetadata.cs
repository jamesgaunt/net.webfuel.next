using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ApplicationStageMetadata: IRepositoryMetadata<ApplicationStage>
    {
        // Data Access
        
        public static string DatabaseTable => "ApplicationStage";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ApplicationStage DataReader(SqlDataReader dr) => new ApplicationStage(dr);
        
        public static List<SqlParameter> ExtractParameters(ApplicationStage entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ApplicationStage.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ApplicationStage.Id):
                        break;
                    case nameof(ApplicationStage.Name):
                        result.Add(new SqlParameter(nameof(ApplicationStage.Name), entity.Name));
                        break;
                    case nameof(ApplicationStage.SortOrder):
                        result.Add(new SqlParameter(nameof(ApplicationStage.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ApplicationStage.Default):
                        result.Add(new SqlParameter(nameof(ApplicationStage.Default), entity.Default));
                        break;
                    case nameof(ApplicationStage.Hidden):
                        result.Add(new SqlParameter(nameof(ApplicationStage.Hidden), entity.Hidden));
                        break;
                    case nameof(ApplicationStage.FreeText):
                        result.Add(new SqlParameter(nameof(ApplicationStage.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ApplicationStage, ApplicationStageMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ApplicationStage, ApplicationStageMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ApplicationStage, ApplicationStageMetadata>();
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
                yield return "FreeText";
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
                yield return "FreeText";
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
                yield return "FreeText";
            }
        }
        
        // Validation
        
        public static void Validate(ApplicationStage entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ApplicationStageRepositoryValidator Validator { get; } = new ApplicationStageRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ApplicationStageRepositoryValidator: AbstractValidator<ApplicationStage>
    {
        public ApplicationStageRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ApplicationStageMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

