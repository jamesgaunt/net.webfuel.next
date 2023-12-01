using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class AgeRangeMetadata: IRepositoryMetadata<AgeRange>
    {
        // Data Access
        
        public static string DatabaseTable => "AgeRange";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static AgeRange DataReader(SqlDataReader dr) => new AgeRange(dr);
        
        public static List<SqlParameter> ExtractParameters(AgeRange entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(AgeRange.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(AgeRange.Id):
                        break;
                    case nameof(AgeRange.Name):
                        result.Add(new SqlParameter(nameof(AgeRange.Name), entity.Name));
                        break;
                    case nameof(AgeRange.SortOrder):
                        result.Add(new SqlParameter(nameof(AgeRange.SortOrder), entity.SortOrder));
                        break;
                    case nameof(AgeRange.Default):
                        result.Add(new SqlParameter(nameof(AgeRange.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<AgeRange, AgeRangeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<AgeRange, AgeRangeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<AgeRange, AgeRangeMetadata>();
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
        
        public static void Validate(AgeRange entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static AgeRangeRepositoryValidator Validator { get; } = new AgeRangeRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class AgeRangeRepositoryValidator: AbstractValidator<AgeRange>
    {
        public AgeRangeRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(AgeRangeMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

