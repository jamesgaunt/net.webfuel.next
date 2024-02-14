using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class WillStudyUseCTUMetadata: IRepositoryMetadata<WillStudyUseCTU>
    {
        // Data Access
        
        public static string DatabaseTable => "WillStudyUseCTU";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static WillStudyUseCTU DataReader(SqlDataReader dr) => new WillStudyUseCTU(dr);
        
        public static List<SqlParameter> ExtractParameters(WillStudyUseCTU entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(WillStudyUseCTU.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(WillStudyUseCTU.Id):
                        break;
                    case nameof(WillStudyUseCTU.Name):
                        result.Add(new SqlParameter(nameof(WillStudyUseCTU.Name), entity.Name));
                        break;
                    case nameof(WillStudyUseCTU.SortOrder):
                        result.Add(new SqlParameter(nameof(WillStudyUseCTU.SortOrder), entity.SortOrder));
                        break;
                    case nameof(WillStudyUseCTU.Default):
                        result.Add(new SqlParameter(nameof(WillStudyUseCTU.Default), entity.Default));
                        break;
                    case nameof(WillStudyUseCTU.FreeText):
                        result.Add(new SqlParameter(nameof(WillStudyUseCTU.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<WillStudyUseCTU, WillStudyUseCTUMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<WillStudyUseCTU, WillStudyUseCTUMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<WillStudyUseCTU, WillStudyUseCTUMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
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
                yield return "FreeText";
            }
        }
        
        // Validation
        
        public static void Validate(WillStudyUseCTU entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static WillStudyUseCTURepositoryValidator Validator { get; } = new WillStudyUseCTURepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class WillStudyUseCTURepositoryValidator: AbstractValidator<WillStudyUseCTU>
    {
        public WillStudyUseCTURepositoryValidator()
        {
            RuleFor(x => x.Name).Use(WillStudyUseCTUMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

