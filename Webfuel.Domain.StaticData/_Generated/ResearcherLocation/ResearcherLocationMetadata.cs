using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearcherLocationMetadata: IRepositoryMetadata<ResearcherLocation>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearcherLocation";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearcherLocation DataReader(SqlDataReader dr) => new ResearcherLocation(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearcherLocation entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearcherLocation.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearcherLocation.Id):
                        break;
                    case nameof(ResearcherLocation.Name):
                        result.Add(new SqlParameter(nameof(ResearcherLocation.Name), entity.Name));
                        break;
                    case nameof(ResearcherLocation.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearcherLocation.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearcherLocation.Default):
                        result.Add(new SqlParameter(nameof(ResearcherLocation.Default), entity.Default));
                        break;
                    case nameof(ResearcherLocation.Hidden):
                        result.Add(new SqlParameter(nameof(ResearcherLocation.Hidden), entity.Hidden));
                        break;
                    case nameof(ResearcherLocation.FreeText):
                        result.Add(new SqlParameter(nameof(ResearcherLocation.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearcherLocation, ResearcherLocationMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearcherLocation, ResearcherLocationMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearcherLocation, ResearcherLocationMetadata>();
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
        
        public static void Validate(ResearcherLocation entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearcherLocationRepositoryValidator Validator { get; } = new ResearcherLocationRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearcherLocationRepositoryValidator: AbstractValidator<ResearcherLocation>
    {
        public ResearcherLocationRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ResearcherLocationMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

