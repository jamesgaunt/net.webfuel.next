using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class EthnicityMetadata: IRepositoryMetadata<Ethnicity>
    {
        // Data Access
        
        public static string DatabaseTable => "Ethnicity";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static Ethnicity DataReader(SqlDataReader dr) => new Ethnicity(dr);
        
        public static List<SqlParameter> ExtractParameters(Ethnicity entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Ethnicity.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Ethnicity.Id):
                        break;
                    case nameof(Ethnicity.Name):
                        result.Add(new SqlParameter(nameof(Ethnicity.Name), entity.Name));
                        break;
                    case nameof(Ethnicity.SortOrder):
                        result.Add(new SqlParameter(nameof(Ethnicity.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Ethnicity.Default):
                        result.Add(new SqlParameter(nameof(Ethnicity.Default), entity.Default));
                        break;
                    case nameof(Ethnicity.Hidden):
                        result.Add(new SqlParameter(nameof(Ethnicity.Hidden), entity.Hidden));
                        break;
                    case nameof(Ethnicity.FreeText):
                        result.Add(new SqlParameter(nameof(Ethnicity.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Ethnicity, EthnicityMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Ethnicity, EthnicityMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Ethnicity, EthnicityMetadata>();
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
        
        public static void Validate(Ethnicity entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static EthnicityRepositoryValidator Validator { get; } = new EthnicityRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class EthnicityRepositoryValidator: AbstractValidator<Ethnicity>
        {
            public EthnicityRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

