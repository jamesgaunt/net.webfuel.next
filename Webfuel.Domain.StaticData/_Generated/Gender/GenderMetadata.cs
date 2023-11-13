using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class GenderMetadata: IRepositoryMetadata<Gender>
    {
        // Data Access
        
        public static string DatabaseTable => "Gender";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static Gender DataReader(SqlDataReader dr) => new Gender(dr);
        
        public static List<SqlParameter> ExtractParameters(Gender entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Gender.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Gender.Id):
                        break;
                    case nameof(Gender.Name):
                        result.Add(new SqlParameter(nameof(Gender.Name), entity.Name));
                        break;
                    case nameof(Gender.SortOrder):
                        result.Add(new SqlParameter(nameof(Gender.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Gender.Default):
                        result.Add(new SqlParameter(nameof(Gender.Default), entity.Default));
                        break;
                    case nameof(Gender.Hidden):
                        result.Add(new SqlParameter(nameof(Gender.Hidden), entity.Hidden));
                        break;
                    case nameof(Gender.FreeText):
                        result.Add(new SqlParameter(nameof(Gender.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Gender, GenderMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Gender, GenderMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Gender, GenderMetadata>();
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
        
        public static void Validate(Gender entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static GenderRepositoryValidator Validator { get; } = new GenderRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class GenderRepositoryValidator: AbstractValidator<Gender>
    {
        public GenderRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(GenderMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

