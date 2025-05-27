using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FullOutcomeMetadata: IRepositoryMetadata<FullOutcome>
    {
        // Data Access
        
        public static string DatabaseTable => "FullOutcome";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static FullOutcome DataReader(SqlDataReader dr) => new FullOutcome(dr);
        
        public static List<SqlParameter> ExtractParameters(FullOutcome entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FullOutcome.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FullOutcome.Id):
                        break;
                    case nameof(FullOutcome.Name):
                        result.Add(new SqlParameter(nameof(FullOutcome.Name), entity.Name));
                        break;
                    case nameof(FullOutcome.SortOrder):
                        result.Add(new SqlParameter(nameof(FullOutcome.SortOrder), entity.SortOrder));
                        break;
                    case nameof(FullOutcome.Default):
                        result.Add(new SqlParameter(nameof(FullOutcome.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FullOutcome, FullOutcomeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FullOutcome, FullOutcomeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FullOutcome, FullOutcomeMetadata>();
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
        
        public static void Validate(FullOutcome entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FullOutcomeRepositoryValidator Validator { get; } = new FullOutcomeRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class FullOutcomeRepositoryValidator: AbstractValidator<FullOutcome>
    {
        public FullOutcomeRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(FullOutcomeMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

