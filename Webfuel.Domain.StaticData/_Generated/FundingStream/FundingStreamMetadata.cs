using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingStreamMetadata: IRepositoryMetadata<FundingStream>
    {
        // Data Access
        
        public static string DatabaseTable => "FundingStream";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static FundingStream DataReader(SqlDataReader dr) => new FundingStream(dr);
        
        public static List<SqlParameter> ExtractParameters(FundingStream entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FundingStream.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FundingStream.Id):
                        break;
                    case nameof(FundingStream.Name):
                        result.Add(new SqlParameter(nameof(FundingStream.Name), entity.Name));
                        break;
                    case nameof(FundingStream.SortOrder):
                        result.Add(new SqlParameter(nameof(FundingStream.SortOrder), entity.SortOrder));
                        break;
                    case nameof(FundingStream.Default):
                        result.Add(new SqlParameter(nameof(FundingStream.Default), entity.Default));
                        break;
                    case nameof(FundingStream.Hidden):
                        result.Add(new SqlParameter(nameof(FundingStream.Hidden), entity.Hidden));
                        break;
                    case nameof(FundingStream.FreeText):
                        result.Add(new SqlParameter(nameof(FundingStream.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FundingStream, FundingStreamMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FundingStream, FundingStreamMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FundingStream, FundingStreamMetadata>();
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
        
        public static void Validate(FundingStream entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FundingStreamRepositoryValidator Validator { get; } = new FundingStreamRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class FundingStreamRepositoryValidator: AbstractValidator<FundingStream>
    {
        public FundingStreamRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(FundingStreamMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

