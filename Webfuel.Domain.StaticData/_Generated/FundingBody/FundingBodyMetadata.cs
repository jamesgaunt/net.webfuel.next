using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingBodyMetadata: IRepositoryMetadata<FundingBody>
    {
        // Data Access
        
        public static string DatabaseTable => "FundingBody";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static FundingBody DataReader(SqlDataReader dr) => new FundingBody(dr);
        
        public static List<SqlParameter> ExtractParameters(FundingBody entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FundingBody.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FundingBody.Id):
                        break;
                    case nameof(FundingBody.Name):
                        result.Add(new SqlParameter(nameof(FundingBody.Name), entity.Name));
                        break;
                    case nameof(FundingBody.SortOrder):
                        result.Add(new SqlParameter(nameof(FundingBody.SortOrder), entity.SortOrder));
                        break;
                    case nameof(FundingBody.Default):
                        result.Add(new SqlParameter(nameof(FundingBody.Default), entity.Default));
                        break;
                    case nameof(FundingBody.Hidden):
                        result.Add(new SqlParameter(nameof(FundingBody.Hidden), entity.Hidden));
                        break;
                    case nameof(FundingBody.FreeText):
                        result.Add(new SqlParameter(nameof(FundingBody.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FundingBody, FundingBodyMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FundingBody, FundingBodyMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FundingBody, FundingBodyMetadata>();
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
        
        public static void Validate(FundingBody entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FundingBodyRepositoryValidator Validator { get; } = new FundingBodyRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class FundingBodyRepositoryValidator: AbstractValidator<FundingBody>
        {
            public FundingBodyRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

