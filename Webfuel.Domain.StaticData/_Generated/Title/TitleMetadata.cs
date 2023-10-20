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
                    case nameof(Title.SortOrder):
                        result.Add(new SqlParameter(nameof(Title.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Title.Default):
                        result.Add(new SqlParameter(nameof(Title.Default), entity.Default));
                        break;
                    case nameof(Title.Hidden):
                        result.Add(new SqlParameter(nameof(Title.Hidden), entity.Hidden));
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
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
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
            }
        }
        
        // Validation
        
        public static void Validate(Title entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static TitleRepositoryValidator Validator { get; } = new TitleRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class TitleRepositoryValidator: AbstractValidator<Title>
        {
            public TitleRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

