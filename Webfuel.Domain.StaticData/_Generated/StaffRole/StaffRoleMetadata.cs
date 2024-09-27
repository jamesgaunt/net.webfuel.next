using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class StaffRoleMetadata: IRepositoryMetadata<StaffRole>
    {
        // Data Access
        
        public static string DatabaseTable => "StaffRole";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static StaffRole DataReader(SqlDataReader dr) => new StaffRole(dr);
        
        public static List<SqlParameter> ExtractParameters(StaffRole entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(StaffRole.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(StaffRole.Id):
                        break;
                    case nameof(StaffRole.Name):
                        result.Add(new SqlParameter(nameof(StaffRole.Name), entity.Name));
                        break;
                    case nameof(StaffRole.Alias):
                        result.Add(new SqlParameter(nameof(StaffRole.Alias), entity.Alias));
                        break;
                    case nameof(StaffRole.SortOrder):
                        result.Add(new SqlParameter(nameof(StaffRole.SortOrder), entity.SortOrder));
                        break;
                    case nameof(StaffRole.Default):
                        result.Add(new SqlParameter(nameof(StaffRole.Default), entity.Default));
                        break;
                    case nameof(StaffRole.Hidden):
                        result.Add(new SqlParameter(nameof(StaffRole.Hidden), entity.Hidden));
                        break;
                    case nameof(StaffRole.FreeText):
                        result.Add(new SqlParameter(nameof(StaffRole.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<StaffRole, StaffRoleMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<StaffRole, StaffRoleMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<StaffRole, StaffRoleMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Alias";
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
                yield return "Alias";
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
                yield return "Alias";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "FreeText";
            }
        }
        
        // Validation
        
        public static void Validate(StaffRole entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Alias = entity.Alias ?? String.Empty;
            entity.Alias = entity.Alias.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static StaffRoleRepositoryValidator Validator { get; } = new StaffRoleRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        public const int Alias_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Alias_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Alias_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class StaffRoleRepositoryValidator: AbstractValidator<StaffRole>
    {
        public StaffRoleRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(StaffRoleMetadata.Name_ValidationRules);
            RuleFor(x => x.Alias).Use(StaffRoleMetadata.Alias_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

