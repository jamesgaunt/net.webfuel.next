using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsPPIEAndEDIContributionMetadata: IRepositoryMetadata<IsPPIEAndEDIContribution>
    {
        // Data Access
        
        public static string DatabaseTable => "IsPPIEAndEDIContribution";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsPPIEAndEDIContribution DataReader(SqlDataReader dr) => new IsPPIEAndEDIContribution(dr);
        
        public static List<SqlParameter> ExtractParameters(IsPPIEAndEDIContribution entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsPPIEAndEDIContribution.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsPPIEAndEDIContribution.Id):
                        break;
                    case nameof(IsPPIEAndEDIContribution.Name):
                        result.Add(new SqlParameter(nameof(IsPPIEAndEDIContribution.Name), entity.Name));
                        break;
                    case nameof(IsPPIEAndEDIContribution.SortOrder):
                        result.Add(new SqlParameter(nameof(IsPPIEAndEDIContribution.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsPPIEAndEDIContribution.Default):
                        result.Add(new SqlParameter(nameof(IsPPIEAndEDIContribution.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsPPIEAndEDIContribution, IsPPIEAndEDIContributionMetadata>();
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
        
        public static void Validate(IsPPIEAndEDIContribution entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsPPIEAndEDIContributionRepositoryValidator Validator { get; } = new IsPPIEAndEDIContributionRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsPPIEAndEDIContributionRepositoryValidator: AbstractValidator<IsPPIEAndEDIContribution>
    {
        public IsPPIEAndEDIContributionRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsPPIEAndEDIContributionMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

