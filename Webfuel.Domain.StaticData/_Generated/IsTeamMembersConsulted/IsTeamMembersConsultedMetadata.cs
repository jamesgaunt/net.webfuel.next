using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsTeamMembersConsultedMetadata: IRepositoryMetadata<IsTeamMembersConsulted>
    {
        // Data Access
        
        public static string DatabaseTable => "IsTeamMembersConsulted";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsTeamMembersConsulted DataReader(SqlDataReader dr) => new IsTeamMembersConsulted(dr);
        
        public static List<SqlParameter> ExtractParameters(IsTeamMembersConsulted entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsTeamMembersConsulted.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsTeamMembersConsulted.Id):
                        break;
                    case nameof(IsTeamMembersConsulted.Name):
                        result.Add(new SqlParameter(nameof(IsTeamMembersConsulted.Name), entity.Name));
                        break;
                    case nameof(IsTeamMembersConsulted.SortOrder):
                        result.Add(new SqlParameter(nameof(IsTeamMembersConsulted.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsTeamMembersConsulted.Default):
                        result.Add(new SqlParameter(nameof(IsTeamMembersConsulted.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsTeamMembersConsulted, IsTeamMembersConsultedMetadata>();
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
        
        public static void Validate(IsTeamMembersConsulted entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsTeamMembersConsultedRepositoryValidator Validator { get; } = new IsTeamMembersConsultedRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class IsTeamMembersConsultedRepositoryValidator: AbstractValidator<IsTeamMembersConsulted>
        {
            public IsTeamMembersConsultedRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

