using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsLeadApplicantNHSMetadata: IRepositoryMetadata<IsLeadApplicantNHS>
    {
        // Data Access
        
        public static string DatabaseTable => "IsLeadApplicantNHS";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsLeadApplicantNHS DataReader(SqlDataReader dr) => new IsLeadApplicantNHS(dr);
        
        public static List<SqlParameter> ExtractParameters(IsLeadApplicantNHS entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsLeadApplicantNHS.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsLeadApplicantNHS.Id):
                        break;
                    case nameof(IsLeadApplicantNHS.Name):
                        result.Add(new SqlParameter(nameof(IsLeadApplicantNHS.Name), entity.Name));
                        break;
                    case nameof(IsLeadApplicantNHS.SortOrder):
                        result.Add(new SqlParameter(nameof(IsLeadApplicantNHS.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsLeadApplicantNHS.Default):
                        result.Add(new SqlParameter(nameof(IsLeadApplicantNHS.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsLeadApplicantNHS, IsLeadApplicantNHSMetadata>();
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
        
        public static void Validate(IsLeadApplicantNHS entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsLeadApplicantNHSRepositoryValidator Validator { get; } = new IsLeadApplicantNHSRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsLeadApplicantNHSRepositoryValidator: AbstractValidator<IsLeadApplicantNHS>
    {
        public IsLeadApplicantNHSRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsLeadApplicantNHSMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

