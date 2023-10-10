using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class TitleMetadata: IRepositoryMetadata<Title>
    {
        // Data Access
        
        public static string DatabaseTable => "Title";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static Title DataReader(SqlDataReader dr) => new Title(dr);
        
        public static List<SqlParameter> ExtractParameters(Title entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Title.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Title.Id):
                        break;
                    case nameof(Title.Name):
                        result.Add(new SqlParameter(nameof(Title.Name), entity.Name));
                        break;
                    case nameof(Title.Code):
                        result.Add(new SqlParameter(nameof(Title.Code), entity.Code));
                        break;
                    case nameof(Title.SortOrder):
                        result.Add(new SqlParameter(nameof(Title.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Title.Default):
                        result.Add(new SqlParameter(nameof(Title.Default), entity.Default));
                        break;
                    case nameof(Title.Hidden):
                        result.Add(new SqlParameter(nameof(Title.Hidden), entity.Hidden));
                        break;
                    case nameof(Title.Hint):
                        result.Add(new SqlParameter(nameof(Title.Hint), entity.Hint));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Title, TitleMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Title, TitleMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Title, TitleMetadata>();
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
        
        public static void Validate(Title entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Code = entity.Code ?? String.Empty;
            entity.Code = entity.Code.Trim();
            entity.Hint = entity.Hint ?? String.Empty;
            entity.Hint = entity.Hint.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static TitleRepositoryValidator Validator { get; } = new TitleRepositoryValidator();
        
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
        
        public class TitleRepositoryValidator: AbstractValidator<Title>
        {
            public TitleRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
                RuleFor(x => x.Code).Use(Code_ValidationRules);
                RuleFor(x => x.Hint).Use(Hint_ValidationRules);
            }
        }
    }
}

