using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearcherCareerStageMetadata: IRepositoryMetadata<ResearcherCareerStage>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearcherCareerStage";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearcherCareerStage DataReader(SqlDataReader dr) => new ResearcherCareerStage(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearcherCareerStage entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearcherCareerStage.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearcherCareerStage.Id):
                        break;
                    case nameof(ResearcherCareerStage.Name):
                        result.Add(new SqlParameter(nameof(ResearcherCareerStage.Name), entity.Name));
                        break;
                    case nameof(ResearcherCareerStage.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearcherCareerStage.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearcherCareerStage.Default):
                        result.Add(new SqlParameter(nameof(ResearcherCareerStage.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearcherCareerStage, ResearcherCareerStageMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearcherCareerStage, ResearcherCareerStageMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearcherCareerStage, ResearcherCareerStageMetadata>();
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
        
        public static void Validate(ResearcherCareerStage entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearcherCareerStageRepositoryValidator Validator { get; } = new ResearcherCareerStageRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearcherCareerStageRepositoryValidator: AbstractValidator<ResearcherCareerStage>
    {
        public ResearcherCareerStageRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ResearcherCareerStageMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

