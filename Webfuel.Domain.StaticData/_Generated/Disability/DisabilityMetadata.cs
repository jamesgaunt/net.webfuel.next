using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class DisabilityMetadata: IRepositoryMetadata<Disability>
    {
        // Data Access
        
        public static string DatabaseTable => "Disability";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static Disability DataReader(SqlDataReader dr) => new Disability(dr);
        
        public static List<SqlParameter> ExtractParameters(Disability entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Disability.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Disability.Id):
                        break;
                    case nameof(Disability.Name):
                        result.Add(new SqlParameter(nameof(Disability.Name), entity.Name));
                        break;
                    case nameof(Disability.SortOrder):
                        result.Add(new SqlParameter(nameof(Disability.SortOrder), entity.SortOrder));
                        break;
                    case nameof(Disability.Default):
                        result.Add(new SqlParameter(nameof(Disability.Default), entity.Default));
                        break;
                    case nameof(Disability.Hidden):
                        result.Add(new SqlParameter(nameof(Disability.Hidden), entity.Hidden));
                        break;
                    case nameof(Disability.FreeText):
                        result.Add(new SqlParameter(nameof(Disability.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Disability, DisabilityMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Disability, DisabilityMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Disability, DisabilityMetadata>();
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
        
        public static void Validate(Disability entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static DisabilityRepositoryValidator Validator { get; } = new DisabilityRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class DisabilityRepositoryValidator: AbstractValidator<Disability>
    {
        public DisabilityRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(DisabilityMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

