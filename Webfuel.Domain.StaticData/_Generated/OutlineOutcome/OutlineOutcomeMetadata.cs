using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class OutlineOutcomeMetadata: IRepositoryMetadata<OutlineOutcome>
    {
        // Data Access
        
        public static string DatabaseTable => "OutlineOutcome";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static OutlineOutcome DataReader(SqlDataReader dr) => new OutlineOutcome(dr);
        
        public static List<SqlParameter> ExtractParameters(OutlineOutcome entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(OutlineOutcome.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(OutlineOutcome.Id):
                        break;
                    case nameof(OutlineOutcome.Name):
                        result.Add(new SqlParameter(nameof(OutlineOutcome.Name), entity.Name));
                        break;
                    case nameof(OutlineOutcome.SortOrder):
                        result.Add(new SqlParameter(nameof(OutlineOutcome.SortOrder), entity.SortOrder));
                        break;
                    case nameof(OutlineOutcome.Default):
                        result.Add(new SqlParameter(nameof(OutlineOutcome.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<OutlineOutcome, OutlineOutcomeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<OutlineOutcome, OutlineOutcomeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<OutlineOutcome, OutlineOutcomeMetadata>();
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
        
        public static void Validate(OutlineOutcome entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static OutlineOutcomeRepositoryValidator Validator { get; } = new OutlineOutcomeRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class OutlineOutcomeRepositoryValidator: AbstractValidator<OutlineOutcome>
    {
        public OutlineOutcomeRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(OutlineOutcomeMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

