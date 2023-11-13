using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SupportTeamMetadata: IRepositoryMetadata<SupportTeam>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportTeam";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SupportTeam DataReader(SqlDataReader dr) => new SupportTeam(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportTeam entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportTeam.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportTeam.Id):
                        break;
                    case nameof(SupportTeam.Name):
                        result.Add(new SqlParameter(nameof(SupportTeam.Name), entity.Name));
                        break;
                    case nameof(SupportTeam.SortOrder):
                        result.Add(new SqlParameter(nameof(SupportTeam.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SupportTeam.Default):
                        result.Add(new SqlParameter(nameof(SupportTeam.Default), entity.Default));
                        break;
                    case nameof(SupportTeam.Hidden):
                        result.Add(new SqlParameter(nameof(SupportTeam.Hidden), entity.Hidden));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportTeam, SupportTeamMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportTeam, SupportTeamMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportTeam, SupportTeamMetadata>();
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
            }
        }
        
        // Validation
        
        public static void Validate(SupportTeam entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportTeamRepositoryValidator Validator { get; } = new SupportTeamRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class SupportTeamRepositoryValidator: AbstractValidator<SupportTeam>
    {
        public SupportTeamRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(SupportTeamMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

