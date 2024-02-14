using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class RSSHubMetadata: IRepositoryMetadata<RSSHub>
    {
        // Data Access
        
        public static string DatabaseTable => "RSSHub";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static RSSHub DataReader(SqlDataReader dr) => new RSSHub(dr);
        
        public static List<SqlParameter> ExtractParameters(RSSHub entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(RSSHub.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(RSSHub.Id):
                        break;
                    case nameof(RSSHub.Name):
                        result.Add(new SqlParameter(nameof(RSSHub.Name), entity.Name));
                        break;
                    case nameof(RSSHub.SortOrder):
                        result.Add(new SqlParameter(nameof(RSSHub.SortOrder), entity.SortOrder));
                        break;
                    case nameof(RSSHub.Default):
                        result.Add(new SqlParameter(nameof(RSSHub.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<RSSHub, RSSHubMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<RSSHub, RSSHubMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<RSSHub, RSSHubMetadata>();
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
        
        public static void Validate(RSSHub entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static RSSHubRepositoryValidator Validator { get; } = new RSSHubRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class RSSHubRepositoryValidator: AbstractValidator<RSSHub>
    {
        public RSSHubRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(RSSHubMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

