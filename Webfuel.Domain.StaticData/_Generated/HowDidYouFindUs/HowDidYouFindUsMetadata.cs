using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class HowDidYouFindUsMetadata: IRepositoryMetadata<HowDidYouFindUs>
    {
        // Data Access
        
        public static string DatabaseTable => "HowDidYouFindUs";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static HowDidYouFindUs DataReader(SqlDataReader dr) => new HowDidYouFindUs(dr);
        
        public static List<SqlParameter> ExtractParameters(HowDidYouFindUs entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(HowDidYouFindUs.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(HowDidYouFindUs.Id):
                        break;
                    case nameof(HowDidYouFindUs.Name):
                        result.Add(new SqlParameter(nameof(HowDidYouFindUs.Name), entity.Name));
                        break;
                    case nameof(HowDidYouFindUs.SortOrder):
                        result.Add(new SqlParameter(nameof(HowDidYouFindUs.SortOrder), entity.SortOrder));
                        break;
                    case nameof(HowDidYouFindUs.Default):
                        result.Add(new SqlParameter(nameof(HowDidYouFindUs.Default), entity.Default));
                        break;
                    case nameof(HowDidYouFindUs.Hidden):
                        result.Add(new SqlParameter(nameof(HowDidYouFindUs.Hidden), entity.Hidden));
                        break;
                    case nameof(HowDidYouFindUs.FreeText):
                        result.Add(new SqlParameter(nameof(HowDidYouFindUs.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<HowDidYouFindUs, HowDidYouFindUsMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<HowDidYouFindUs, HowDidYouFindUsMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<HowDidYouFindUs, HowDidYouFindUsMetadata>();
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
        
        public static void Validate(HowDidYouFindUs entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static HowDidYouFindUsRepositoryValidator Validator { get; } = new HowDidYouFindUsRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class HowDidYouFindUsRepositoryValidator: AbstractValidator<HowDidYouFindUs>
    {
        public HowDidYouFindUsRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(HowDidYouFindUsMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

