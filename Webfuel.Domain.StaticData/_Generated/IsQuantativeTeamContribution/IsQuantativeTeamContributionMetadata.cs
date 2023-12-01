using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsQuantativeTeamContributionMetadata: IRepositoryMetadata<IsQuantativeTeamContribution>
    {
        // Data Access
        
        public static string DatabaseTable => "IsQuantativeTeamContribution";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsQuantativeTeamContribution DataReader(SqlDataReader dr) => new IsQuantativeTeamContribution(dr);
        
        public static List<SqlParameter> ExtractParameters(IsQuantativeTeamContribution entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsQuantativeTeamContribution.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsQuantativeTeamContribution.Id):
                        break;
                    case nameof(IsQuantativeTeamContribution.Name):
                        result.Add(new SqlParameter(nameof(IsQuantativeTeamContribution.Name), entity.Name));
                        break;
                    case nameof(IsQuantativeTeamContribution.SortOrder):
                        result.Add(new SqlParameter(nameof(IsQuantativeTeamContribution.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsQuantativeTeamContribution.Default):
                        result.Add(new SqlParameter(nameof(IsQuantativeTeamContribution.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsQuantativeTeamContribution, IsQuantativeTeamContributionMetadata>();
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
        
        public static void Validate(IsQuantativeTeamContribution entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsQuantativeTeamContributionRepositoryValidator Validator { get; } = new IsQuantativeTeamContributionRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsQuantativeTeamContributionRepositoryValidator: AbstractValidator<IsQuantativeTeamContribution>
    {
        public IsQuantativeTeamContributionRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsQuantativeTeamContributionMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

