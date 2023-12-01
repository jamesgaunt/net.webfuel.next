using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearcherOrganisationTypeMetadata: IRepositoryMetadata<ResearcherOrganisationType>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearcherOrganisationType";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearcherOrganisationType DataReader(SqlDataReader dr) => new ResearcherOrganisationType(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearcherOrganisationType entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearcherOrganisationType.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearcherOrganisationType.Id):
                        break;
                    case nameof(ResearcherOrganisationType.Name):
                        result.Add(new SqlParameter(nameof(ResearcherOrganisationType.Name), entity.Name));
                        break;
                    case nameof(ResearcherOrganisationType.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearcherOrganisationType.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearcherOrganisationType.Default):
                        result.Add(new SqlParameter(nameof(ResearcherOrganisationType.Default), entity.Default));
                        break;
                    case nameof(ResearcherOrganisationType.Hidden):
                        result.Add(new SqlParameter(nameof(ResearcherOrganisationType.Hidden), entity.Hidden));
                        break;
                    case nameof(ResearcherOrganisationType.FreeText):
                        result.Add(new SqlParameter(nameof(ResearcherOrganisationType.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearcherOrganisationType, ResearcherOrganisationTypeMetadata>();
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
        
        public static void Validate(ResearcherOrganisationType entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearcherOrganisationTypeRepositoryValidator Validator { get; } = new ResearcherOrganisationTypeRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearcherOrganisationTypeRepositoryValidator: AbstractValidator<ResearcherOrganisationType>
    {
        public ResearcherOrganisationTypeRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ResearcherOrganisationTypeMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

