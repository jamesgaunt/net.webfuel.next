using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsYesNoMetadata: IRepositoryMetadata<IsYesNo>
    {
        // Data Access
        
        public static string DatabaseTable => "IsYesNo";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsYesNo DataReader(SqlDataReader dr) => new IsYesNo(dr);
        
        public static List<SqlParameter> ExtractParameters(IsYesNo entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsYesNo.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsYesNo.Id):
                        break;
                    case nameof(IsYesNo.Name):
                        result.Add(new SqlParameter(nameof(IsYesNo.Name), entity.Name));
                        break;
                    case nameof(IsYesNo.SortOrder):
                        result.Add(new SqlParameter(nameof(IsYesNo.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsYesNo.Default):
                        result.Add(new SqlParameter(nameof(IsYesNo.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsYesNo, IsYesNoMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsYesNo, IsYesNoMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsYesNo, IsYesNoMetadata>();
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
        
        public static void Validate(IsYesNo entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsYesNoRepositoryValidator Validator { get; } = new IsYesNoRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsYesNoRepositoryValidator: AbstractValidator<IsYesNo>
    {
        public IsYesNoRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsYesNoMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

