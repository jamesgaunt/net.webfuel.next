using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class UserDisciplineMetadata: IRepositoryMetadata<UserDiscipline>
    {
        // Data Access
        
        public static string DatabaseTable => "UserDiscipline";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static UserDiscipline DataReader(SqlDataReader dr) => new UserDiscipline(dr);
        
        public static List<SqlParameter> ExtractParameters(UserDiscipline entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(UserDiscipline.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(UserDiscipline.Id):
                        break;
                    case nameof(UserDiscipline.Name):
                        result.Add(new SqlParameter(nameof(UserDiscipline.Name), entity.Name));
                        break;
                    case nameof(UserDiscipline.SortOrder):
                        result.Add(new SqlParameter(nameof(UserDiscipline.SortOrder), entity.SortOrder));
                        break;
                    case nameof(UserDiscipline.Default):
                        result.Add(new SqlParameter(nameof(UserDiscipline.Default), entity.Default));
                        break;
                    case nameof(UserDiscipline.Hidden):
                        result.Add(new SqlParameter(nameof(UserDiscipline.Hidden), entity.Hidden));
                        break;
                    case nameof(UserDiscipline.FreeText):
                        result.Add(new SqlParameter(nameof(UserDiscipline.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<UserDiscipline, UserDisciplineMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<UserDiscipline, UserDisciplineMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<UserDiscipline, UserDisciplineMetadata>();
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
                yield return "FreeText";
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
                yield return "FreeText";
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
                yield return "FreeText";
            }
        }
        
        // Validation
        
        public static void Validate(UserDiscipline entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserDisciplineRepositoryValidator Validator { get; } = new UserDisciplineRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class UserDisciplineRepositoryValidator: AbstractValidator<UserDiscipline>
    {
        public UserDisciplineRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(UserDisciplineMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

