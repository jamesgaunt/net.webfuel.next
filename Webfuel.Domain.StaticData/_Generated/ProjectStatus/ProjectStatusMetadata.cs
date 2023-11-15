using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ProjectStatusMetadata: IRepositoryMetadata<ProjectStatus>
    {
        // Data Access
        
        public static string DatabaseTable => "ProjectStatus";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ProjectStatus DataReader(SqlDataReader dr) => new ProjectStatus(dr);
        
        public static List<SqlParameter> ExtractParameters(ProjectStatus entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ProjectStatus.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ProjectStatus.Id):
                        break;
                    case nameof(ProjectStatus.Name):
                        result.Add(new SqlParameter(nameof(ProjectStatus.Name), entity.Name));
                        break;
                    case nameof(ProjectStatus.SortOrder):
                        result.Add(new SqlParameter(nameof(ProjectStatus.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ProjectStatus.Default):
                        result.Add(new SqlParameter(nameof(ProjectStatus.Default), entity.Default));
                        break;
                    case nameof(ProjectStatus.Hidden):
                        result.Add(new SqlParameter(nameof(ProjectStatus.Hidden), entity.Hidden));
                        break;
                    case nameof(ProjectStatus.Locked):
                        result.Add(new SqlParameter(nameof(ProjectStatus.Locked), entity.Locked));
                        break;
                    case nameof(ProjectStatus.Discarded):
                        result.Add(new SqlParameter(nameof(ProjectStatus.Discarded), entity.Discarded));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ProjectStatus, ProjectStatusMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ProjectStatus, ProjectStatusMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ProjectStatus, ProjectStatusMetadata>();
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
                yield return "Locked";
                yield return "Discarded";
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
                yield return "Locked";
                yield return "Discarded";
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
                yield return "Locked";
                yield return "Discarded";
            }
        }
        
        // Validation
        
        public static void Validate(ProjectStatus entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectStatusRepositoryValidator Validator { get; } = new ProjectStatusRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ProjectStatusRepositoryValidator: AbstractValidator<ProjectStatus>
    {
        public ProjectStatusRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(ProjectStatusMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

