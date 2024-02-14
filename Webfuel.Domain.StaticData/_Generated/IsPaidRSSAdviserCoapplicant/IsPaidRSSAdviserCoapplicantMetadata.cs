using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsPaidRSSAdviserCoapplicantMetadata: IRepositoryMetadata<IsPaidRSSAdviserCoapplicant>
    {
        // Data Access
        
        public static string DatabaseTable => "IsPaidRSSAdviserCoapplicant";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsPaidRSSAdviserCoapplicant DataReader(SqlDataReader dr) => new IsPaidRSSAdviserCoapplicant(dr);
        
        public static List<SqlParameter> ExtractParameters(IsPaidRSSAdviserCoapplicant entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsPaidRSSAdviserCoapplicant.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsPaidRSSAdviserCoapplicant.Id):
                        break;
                    case nameof(IsPaidRSSAdviserCoapplicant.Name):
                        result.Add(new SqlParameter(nameof(IsPaidRSSAdviserCoapplicant.Name), entity.Name));
                        break;
                    case nameof(IsPaidRSSAdviserCoapplicant.SortOrder):
                        result.Add(new SqlParameter(nameof(IsPaidRSSAdviserCoapplicant.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsPaidRSSAdviserCoapplicant.Default):
                        result.Add(new SqlParameter(nameof(IsPaidRSSAdviserCoapplicant.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserCoapplicantMetadata>();
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
        
        public static void Validate(IsPaidRSSAdviserCoapplicant entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsPaidRSSAdviserCoapplicantRepositoryValidator Validator { get; } = new IsPaidRSSAdviserCoapplicantRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsPaidRSSAdviserCoapplicantRepositoryValidator: AbstractValidator<IsPaidRSSAdviserCoapplicant>
    {
        public IsPaidRSSAdviserCoapplicantRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsPaidRSSAdviserCoapplicantMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

