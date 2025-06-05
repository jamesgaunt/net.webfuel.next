using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearcherProfessionalBackgroundMetadata: IRepositoryMetadata<ResearcherProfessionalBackground>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearcherProfessionalBackground";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearcherProfessionalBackground DataReader(SqlDataReader dr) => new ResearcherProfessionalBackground(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearcherProfessionalBackground entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearcherProfessionalBackground.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearcherProfessionalBackground.Id):
                        break;
                    case nameof(ResearcherProfessionalBackground.Name):
                        result.Add(new SqlParameter(nameof(ResearcherProfessionalBackground.Name), entity.Name));
                        break;
                    case nameof(ResearcherProfessionalBackground.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearcherProfessionalBackground.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearcherProfessionalBackground.Default):
                        result.Add(new SqlParameter(nameof(ResearcherProfessionalBackground.Default), entity.Default));
                        break;
                    case nameof(ResearcherProfessionalBackground.Hidden):
                        result.Add(new SqlParameter(nameof(ResearcherProfessionalBackground.Hidden), entity.Hidden));
                        break;
                    case nameof(ResearcherProfessionalBackground.FreeText):
                        result.Add(new SqlParameter(nameof(ResearcherProfessionalBackground.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearcherProfessionalBackground, ResearcherProfessionalBackgroundMetadata>();
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
        
        public static void Validate(ResearcherProfessionalBackground entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearcherProfessionalBackgroundRepositoryValidator Validator { get; } = new ResearcherProfessionalBackgroundRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearcherProfessionalBackgroundRepositoryValidator: AbstractValidator<ResearcherProfessionalBackground>
    {
        public ResearcherProfessionalBackgroundRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ResearcherProfessionalBackgroundMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

