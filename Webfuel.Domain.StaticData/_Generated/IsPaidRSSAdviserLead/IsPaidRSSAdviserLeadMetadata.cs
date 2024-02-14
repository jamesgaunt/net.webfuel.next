using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsPaidRSSAdviserLeadMetadata: IRepositoryMetadata<IsPaidRSSAdviserLead>
    {
        // Data Access
        
        public static string DatabaseTable => "IsPaidRSSAdviserLead";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsPaidRSSAdviserLead DataReader(SqlDataReader dr) => new IsPaidRSSAdviserLead(dr);
        
        public static List<SqlParameter> ExtractParameters(IsPaidRSSAdviserLead entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsPaidRSSAdviserLead.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsPaidRSSAdviserLead.Id):
                        break;
                    case nameof(IsPaidRSSAdviserLead.Name):
                        result.Add(new SqlParameter(nameof(IsPaidRSSAdviserLead.Name), entity.Name));
                        break;
                    case nameof(IsPaidRSSAdviserLead.SortOrder):
                        result.Add(new SqlParameter(nameof(IsPaidRSSAdviserLead.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsPaidRSSAdviserLead.Default):
                        result.Add(new SqlParameter(nameof(IsPaidRSSAdviserLead.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsPaidRSSAdviserLead, IsPaidRSSAdviserLeadMetadata>();
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
        
        public static void Validate(IsPaidRSSAdviserLead entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsPaidRSSAdviserLeadRepositoryValidator Validator { get; } = new IsPaidRSSAdviserLeadRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsPaidRSSAdviserLeadRepositoryValidator: AbstractValidator<IsPaidRSSAdviserLead>
    {
        public IsPaidRSSAdviserLeadRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsPaidRSSAdviserLeadMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

