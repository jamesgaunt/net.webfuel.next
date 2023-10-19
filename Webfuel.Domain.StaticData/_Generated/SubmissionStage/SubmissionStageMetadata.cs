using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SubmissionStageMetadata: IRepositoryMetadata<SubmissionStage>
    {
        // Data Access
        
        public static string DatabaseTable => "SubmissionStage";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SubmissionStage DataReader(SqlDataReader dr) => new SubmissionStage(dr);
        
        public static List<SqlParameter> ExtractParameters(SubmissionStage entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SubmissionStage.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SubmissionStage.Id):
                        break;
                    case nameof(SubmissionStage.Name):
                        result.Add(new SqlParameter(nameof(SubmissionStage.Name), entity.Name));
                        break;
                    case nameof(SubmissionStage.SortOrder):
                        result.Add(new SqlParameter(nameof(SubmissionStage.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SubmissionStage.Default):
                        result.Add(new SqlParameter(nameof(SubmissionStage.Default), entity.Default));
                        break;
                    case nameof(SubmissionStage.Hidden):
                        result.Add(new SqlParameter(nameof(SubmissionStage.Hidden), entity.Hidden));
                        break;
                    case nameof(SubmissionStage.FreeText):
                        result.Add(new SqlParameter(nameof(SubmissionStage.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SubmissionStage, SubmissionStageMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SubmissionStage, SubmissionStageMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SubmissionStage, SubmissionStageMetadata>();
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
        
        public static void Validate(SubmissionStage entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SubmissionStageRepositoryValidator Validator { get; } = new SubmissionStageRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class SubmissionStageRepositoryValidator: AbstractValidator<SubmissionStage>
        {
            public SubmissionStageRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

