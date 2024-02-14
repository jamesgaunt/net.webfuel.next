using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ProfessionalBackgroundMetadata: IRepositoryMetadata<ProfessionalBackground>
    {
        // Data Access
        
        public static string DatabaseTable => "ProfessionalBackground";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ProfessionalBackground DataReader(SqlDataReader dr) => new ProfessionalBackground(dr);
        
        public static List<SqlParameter> ExtractParameters(ProfessionalBackground entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProfessionalBackground.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProfessionalBackground.Id):
                        break;
                    case nameof(ProfessionalBackground.Name):
                        result.Add(new SqlParameter(nameof(ProfessionalBackground.Name), entity.Name));
                        break;
                    case nameof(ProfessionalBackground.SortOrder):
                        result.Add(new SqlParameter(nameof(ProfessionalBackground.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ProfessionalBackground.Default):
                        result.Add(new SqlParameter(nameof(ProfessionalBackground.Default), entity.Default));
                        break;
                    case nameof(ProfessionalBackground.Hidden):
                        result.Add(new SqlParameter(nameof(ProfessionalBackground.Hidden), entity.Hidden));
                        break;
                    case nameof(ProfessionalBackground.FreeText):
                        result.Add(new SqlParameter(nameof(ProfessionalBackground.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProfessionalBackground, ProfessionalBackgroundMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProfessionalBackground, ProfessionalBackgroundMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProfessionalBackground, ProfessionalBackgroundMetadata>();
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
        
        public static void Validate(ProfessionalBackground entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProfessionalBackgroundRepositoryValidator Validator { get; } = new ProfessionalBackgroundRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ProfessionalBackgroundRepositoryValidator: AbstractValidator<ProfessionalBackground>
    {
        public ProfessionalBackgroundRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ProfessionalBackgroundMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

