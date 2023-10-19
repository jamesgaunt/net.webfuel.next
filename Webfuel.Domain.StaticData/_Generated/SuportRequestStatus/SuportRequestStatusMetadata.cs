using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SuportRequestStatusMetadata: IRepositoryMetadata<SuportRequestStatus>
    {
        // Data Access
        
        public static string DatabaseTable => "SuportRequestStatus";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SuportRequestStatus DataReader(SqlDataReader dr) => new SuportRequestStatus(dr);
        
        public static List<SqlParameter> ExtractParameters(SuportRequestStatus entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SuportRequestStatus.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SuportRequestStatus.Id):
                        break;
                    case nameof(SuportRequestStatus.Name):
                        result.Add(new SqlParameter(nameof(SuportRequestStatus.Name), entity.Name));
                        break;
                    case nameof(SuportRequestStatus.SortOrder):
                        result.Add(new SqlParameter(nameof(SuportRequestStatus.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SuportRequestStatus.Default):
                        result.Add(new SqlParameter(nameof(SuportRequestStatus.Default), entity.Default));
                        break;
                    case nameof(SuportRequestStatus.Hidden):
                        result.Add(new SqlParameter(nameof(SuportRequestStatus.Hidden), entity.Hidden));
                        break;
                    case nameof(SuportRequestStatus.FreeText):
                        result.Add(new SqlParameter(nameof(SuportRequestStatus.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SuportRequestStatus, SuportRequestStatusMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SuportRequestStatus, SuportRequestStatusMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SuportRequestStatus, SuportRequestStatusMetadata>();
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
        
        public static void Validate(SuportRequestStatus entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SuportRequestStatusRepositoryValidator Validator { get; } = new SuportRequestStatusRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class SuportRequestStatusRepositoryValidator: AbstractValidator<SuportRequestStatus>
        {
            public SuportRequestStatusRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

