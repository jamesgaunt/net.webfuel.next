using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsPrePostAwardMetadata: IRepositoryMetadata<IsPrePostAward>
    {
        // Data Access
        
        public static string DatabaseTable => "IsPrePostAward";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsPrePostAward DataReader(SqlDataReader dr) => new IsPrePostAward(dr);
        
        public static List<SqlParameter> ExtractParameters(IsPrePostAward entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsPrePostAward.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsPrePostAward.Id):
                        break;
                    case nameof(IsPrePostAward.Name):
                        result.Add(new SqlParameter(nameof(IsPrePostAward.Name), entity.Name));
                        break;
                    case nameof(IsPrePostAward.SortOrder):
                        result.Add(new SqlParameter(nameof(IsPrePostAward.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsPrePostAward.Default):
                        result.Add(new SqlParameter(nameof(IsPrePostAward.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsPrePostAward, IsPrePostAwardMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsPrePostAward, IsPrePostAwardMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsPrePostAward, IsPrePostAwardMetadata>();
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
        
        public static void Validate(IsPrePostAward entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsPrePostAwardRepositoryValidator Validator { get; } = new IsPrePostAwardRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsPrePostAwardRepositoryValidator: AbstractValidator<IsPrePostAward>
    {
        public IsPrePostAwardRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsPrePostAwardMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

