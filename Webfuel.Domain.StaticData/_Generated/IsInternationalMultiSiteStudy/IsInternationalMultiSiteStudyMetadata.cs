using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsInternationalMultiSiteStudyMetadata: IRepositoryMetadata<IsInternationalMultiSiteStudy>
    {
        // Data Access
        
        public static string DatabaseTable => "IsInternationalMultiSiteStudy";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsInternationalMultiSiteStudy DataReader(SqlDataReader dr) => new IsInternationalMultiSiteStudy(dr);
        
        public static List<SqlParameter> ExtractParameters(IsInternationalMultiSiteStudy entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsInternationalMultiSiteStudy.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsInternationalMultiSiteStudy.Id):
                        break;
                    case nameof(IsInternationalMultiSiteStudy.Name):
                        result.Add(new SqlParameter(nameof(IsInternationalMultiSiteStudy.Name), entity.Name));
                        break;
                    case nameof(IsInternationalMultiSiteStudy.SortOrder):
                        result.Add(new SqlParameter(nameof(IsInternationalMultiSiteStudy.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsInternationalMultiSiteStudy.Default):
                        result.Add(new SqlParameter(nameof(IsInternationalMultiSiteStudy.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsInternationalMultiSiteStudy, IsInternationalMultiSiteStudyMetadata>();
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
        
        public static void Validate(IsInternationalMultiSiteStudy entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsInternationalMultiSiteStudyRepositoryValidator Validator { get; } = new IsInternationalMultiSiteStudyRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsInternationalMultiSiteStudyRepositoryValidator: AbstractValidator<IsInternationalMultiSiteStudy>
    {
        public IsInternationalMultiSiteStudyRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsInternationalMultiSiteStudyMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

