using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class WorkActivityMetadata: IRepositoryMetadata<WorkActivity>
    {
        // Data Access
        
        public static string DatabaseTable => "WorkActivity";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static WorkActivity DataReader(SqlDataReader dr) => new WorkActivity(dr);
        
        public static List<SqlParameter> ExtractParameters(WorkActivity entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(WorkActivity.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(WorkActivity.Id):
                        break;
                    case nameof(WorkActivity.Name):
                        result.Add(new SqlParameter(nameof(WorkActivity.Name), entity.Name));
                        break;
                    case nameof(WorkActivity.SortOrder):
                        result.Add(new SqlParameter(nameof(WorkActivity.SortOrder), entity.SortOrder));
                        break;
                    case nameof(WorkActivity.Default):
                        result.Add(new SqlParameter(nameof(WorkActivity.Default), entity.Default));
                        break;
                    case nameof(WorkActivity.Hidden):
                        result.Add(new SqlParameter(nameof(WorkActivity.Hidden), entity.Hidden));
                        break;
                    case nameof(WorkActivity.FreeText):
                        result.Add(new SqlParameter(nameof(WorkActivity.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<WorkActivity, WorkActivityMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<WorkActivity, WorkActivityMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<WorkActivity, WorkActivityMetadata>();
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
        
        public static void Validate(WorkActivity entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static WorkActivityRepositoryValidator Validator { get; } = new WorkActivityRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class WorkActivityRepositoryValidator: AbstractValidator<WorkActivity>
    {
        public WorkActivityRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(WorkActivityMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

