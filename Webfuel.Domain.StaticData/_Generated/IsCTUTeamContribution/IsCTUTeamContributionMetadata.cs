using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsCTUTeamContributionMetadata: IRepositoryMetadata<IsCTUTeamContribution>
    {
        // Data Access
        
        public static string DatabaseTable => "IsCTUTeamContribution";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsCTUTeamContribution DataReader(SqlDataReader dr) => new IsCTUTeamContribution(dr);
        
        public static List<SqlParameter> ExtractParameters(IsCTUTeamContribution entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsCTUTeamContribution.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsCTUTeamContribution.Id):
                        break;
                    case nameof(IsCTUTeamContribution.Name):
                        result.Add(new SqlParameter(nameof(IsCTUTeamContribution.Name), entity.Name));
                        break;
                    case nameof(IsCTUTeamContribution.SortOrder):
                        result.Add(new SqlParameter(nameof(IsCTUTeamContribution.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsCTUTeamContribution.Default):
                        result.Add(new SqlParameter(nameof(IsCTUTeamContribution.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsCTUTeamContribution, IsCTUTeamContributionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsCTUTeamContribution, IsCTUTeamContributionMetadata>();
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
        
        public static void Validate(IsCTUTeamContribution entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsCTUTeamContributionRepositoryValidator Validator { get; } = new IsCTUTeamContributionRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsCTUTeamContributionRepositoryValidator: AbstractValidator<IsCTUTeamContribution>
    {
        public IsCTUTeamContributionRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsCTUTeamContributionMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

