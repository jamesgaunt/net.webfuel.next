using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearchMethodologyMetadata: IRepositoryMetadata<ResearchMethodology>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearchMethodology";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearchMethodology DataReader(SqlDataReader dr) => new ResearchMethodology(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearchMethodology entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearchMethodology.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearchMethodology.Id):
                        break;
                    case nameof(ResearchMethodology.Name):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Name), entity.Name));
                        break;
                    case nameof(ResearchMethodology.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearchMethodology.Default):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Default), entity.Default));
                        break;
                    case nameof(ResearchMethodology.Hidden):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Hidden), entity.Hidden));
                        break;
                    case nameof(ResearchMethodology.FreeText):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearchMethodology, ResearchMethodologyMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearchMethodology, ResearchMethodologyMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearchMethodology, ResearchMethodologyMetadata>();
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
        
        public static void Validate(ResearchMethodology entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearchMethodologyRepositoryValidator Validator { get; } = new ResearchMethodologyRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearchMethodologyRepositoryValidator: AbstractValidator<ResearchMethodology>
    {
        public ResearchMethodologyRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ResearchMethodologyMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

