using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SupportProvidedMetadata: IRepositoryMetadata<SupportProvided>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportProvided";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SupportProvided DataReader(SqlDataReader dr) => new SupportProvided(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportProvided entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportProvided.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportProvided.Id):
                        break;
                    case nameof(SupportProvided.Name):
                        result.Add(new SqlParameter(nameof(SupportProvided.Name), entity.Name));
                        break;
                    case nameof(SupportProvided.SortOrder):
                        result.Add(new SqlParameter(nameof(SupportProvided.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SupportProvided.Default):
                        result.Add(new SqlParameter(nameof(SupportProvided.Default), entity.Default));
                        break;
                    case nameof(SupportProvided.Hidden):
                        result.Add(new SqlParameter(nameof(SupportProvided.Hidden), entity.Hidden));
                        break;
                    case nameof(SupportProvided.FreeText):
                        result.Add(new SqlParameter(nameof(SupportProvided.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportProvided, SupportProvidedMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportProvided, SupportProvidedMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportProvided, SupportProvidedMetadata>();
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
        
        public static void Validate(SupportProvided entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportProvidedRepositoryValidator Validator { get; } = new SupportProvidedRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class SupportProvidedRepositoryValidator: AbstractValidator<SupportProvided>
    {
        public SupportProvidedRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(SupportProvidedMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

