using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearcherRoleMetadata: IRepositoryMetadata<ResearcherRole>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearcherRole";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearcherRole DataReader(SqlDataReader dr) => new ResearcherRole(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearcherRole entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearcherRole.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearcherRole.Id):
                        break;
                    case nameof(ResearcherRole.Name):
                        result.Add(new SqlParameter(nameof(ResearcherRole.Name), entity.Name));
                        break;
                    case nameof(ResearcherRole.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearcherRole.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearcherRole.Default):
                        result.Add(new SqlParameter(nameof(ResearcherRole.Default), entity.Default));
                        break;
                    case nameof(ResearcherRole.Hidden):
                        result.Add(new SqlParameter(nameof(ResearcherRole.Hidden), entity.Hidden));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearcherRole, ResearcherRoleMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearcherRole, ResearcherRoleMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearcherRole, ResearcherRoleMetadata>();
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
        
        public static void Validate(ResearcherRole entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearcherRoleRepositoryValidator Validator { get; } = new ResearcherRoleRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ResearcherRoleRepositoryValidator: AbstractValidator<ResearcherRole>
    {
        public ResearcherRoleRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ResearcherRoleMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

