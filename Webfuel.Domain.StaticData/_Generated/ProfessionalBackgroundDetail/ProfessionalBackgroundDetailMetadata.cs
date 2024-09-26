using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ProfessionalBackgroundDetailMetadata: IRepositoryMetadata<ProfessionalBackgroundDetail>
    {
        // Data Access
        
        public static string DatabaseTable => "ProfessionalBackgroundDetail";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ProfessionalBackgroundDetail DataReader(SqlDataReader dr) => new ProfessionalBackgroundDetail(dr);
        
        public static List<SqlParameter> ExtractParameters(ProfessionalBackgroundDetail entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProfessionalBackgroundDetail.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProfessionalBackgroundDetail.Id):
                        break;
                    case nameof(ProfessionalBackgroundDetail.Name):
                        result.Add(new SqlParameter(nameof(ProfessionalBackgroundDetail.Name), entity.Name));
                        break;
                    case nameof(ProfessionalBackgroundDetail.SortOrder):
                        result.Add(new SqlParameter(nameof(ProfessionalBackgroundDetail.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ProfessionalBackgroundDetail.Default):
                        result.Add(new SqlParameter(nameof(ProfessionalBackgroundDetail.Default), entity.Default));
                        break;
                    case nameof(ProfessionalBackgroundDetail.Hidden):
                        result.Add(new SqlParameter(nameof(ProfessionalBackgroundDetail.Hidden), entity.Hidden));
                        break;
                    case nameof(ProfessionalBackgroundDetail.FreeText):
                        result.Add(new SqlParameter(nameof(ProfessionalBackgroundDetail.FreeText), entity.FreeText));
                        break;
                    case nameof(ProfessionalBackgroundDetail.ProfessionalBackgroundId):
                        result.Add(new SqlParameter(nameof(ProfessionalBackgroundDetail.ProfessionalBackgroundId), entity.ProfessionalBackgroundId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProfessionalBackgroundDetail, ProfessionalBackgroundDetailMetadata>();
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
                yield return "ProfessionalBackgroundId";
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
                yield return "ProfessionalBackgroundId";
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
                yield return "ProfessionalBackgroundId";
            }
        }
        
        // Validation
        
        public static void Validate(ProfessionalBackgroundDetail entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProfessionalBackgroundDetailRepositoryValidator Validator { get; } = new ProfessionalBackgroundDetailRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ProfessionalBackgroundDetailRepositoryValidator: AbstractValidator<ProfessionalBackgroundDetail>
    {
        public ProfessionalBackgroundDetailRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ProfessionalBackgroundDetailMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

