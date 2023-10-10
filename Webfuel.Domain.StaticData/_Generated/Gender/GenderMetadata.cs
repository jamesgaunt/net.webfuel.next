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
                    case nameof(Gender.Code):
                        result.Add(new SqlParameter(nameof(Gender.Code), entity.Code));
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
                    case nameof(Gender.Hint):
                        result.Add(new SqlParameter(nameof(Gender.Hint), entity.Hint));
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
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "Hint";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "Hint";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "Hint";
            }
        }
        
        // Validation
        
        public static void Validate(Gender entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Code = entity.Code ?? String.Empty;
            entity.Code = entity.Code.Trim();
            entity.Hint = entity.Hint ?? String.Empty;
            entity.Hint = entity.Hint.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static GenderRepositoryValidator Validator { get; } = new GenderRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        public const int Code_MaxLength = 64;
        public const int Hint_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Code_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Code_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Hint_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Hint_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class GenderRepositoryValidator: AbstractValidator<Gender>
        {
            public GenderRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
                RuleFor(x => x.Code).Use(Code_ValidationRules);
                RuleFor(x => x.Hint).Use(Hint_ValidationRules);
            }
        }
    }
}

