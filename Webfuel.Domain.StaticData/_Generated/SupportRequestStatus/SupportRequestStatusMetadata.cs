using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SupportRequestStatusMetadata: IRepositoryMetadata<SupportRequestStatus>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportRequestStatus";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SupportRequestStatus DataReader(SqlDataReader dr) => new SupportRequestStatus(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportRequestStatus entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportRequestStatus.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportRequestStatus.Id):
                        break;
                    case nameof(SupportRequestStatus.Name):
                        result.Add(new SqlParameter(nameof(SupportRequestStatus.Name), entity.Name));
                        break;
                    case nameof(SupportRequestStatus.SortOrder):
                        result.Add(new SqlParameter(nameof(SupportRequestStatus.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SupportRequestStatus.Default):
                        result.Add(new SqlParameter(nameof(SupportRequestStatus.Default), entity.Default));
                        break;
                    case nameof(SupportRequestStatus.Hidden):
                        result.Add(new SqlParameter(nameof(SupportRequestStatus.Hidden), entity.Hidden));
                        break;
                    case nameof(SupportRequestStatus.FreeText):
                        result.Add(new SqlParameter(nameof(SupportRequestStatus.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportRequestStatus, SupportRequestStatusMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportRequestStatus, SupportRequestStatusMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportRequestStatus, SupportRequestStatusMetadata>();
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
        
        public static void Validate(SupportRequestStatus entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportRequestStatusRepositoryValidator Validator { get; } = new SupportRequestStatusRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class SupportRequestStatusRepositoryValidator: AbstractValidator<SupportRequestStatus>
    {
        public SupportRequestStatusRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(SupportRequestStatusMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

