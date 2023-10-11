using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearchMethodologyMetadata: IRepositoryMetadata<ResearchMethodology>
    {
        // Data Access
        
        public static string DatabaseTable => "ResearchMethodology";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static ResearchMethodology DataReader(SqlDataReader dr) => new ResearchMethodology(dr);
        
        public static List<SqlParameter> ExtractParameters(ResearchMethodology entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(ResearchMethodology.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(ResearchMethodology.Id):
                        break;
                    case nameof(ResearchMethodology.Name):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Name), entity.Name));
                        break;
                    case nameof(ResearchMethodology.Code):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Code), entity.Code));
                        break;
                    case nameof(ResearchMethodology.SortOrder):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.SortOrder), entity.SortOrder));
                        break;
                    case nameof(ResearchMethodology.Default):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Default), entity.Default));
                        break;
                    case nameof(ResearchMethodology.Hidden):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Hidden), entity.Hidden));
                        break;
                    case nameof(ResearchMethodology.Hint):
                        result.Add(new SqlParameter(nameof(ResearchMethodology.Hint), entity.Hint));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<ResearchMethodology, ResearchMethodologyMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<ResearchMethodology, ResearchMethodologyMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<ResearchMethodology, ResearchMethodologyMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "Hint";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "Hint";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
                yield return "Hint";
            }
        }
        
        // Validation
        
        public static void Validate(ResearchMethodology entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Code = entity.Code ?? String.Empty;
            entity.Code = entity.Code.Trim();
            entity.Hint = entity.Hint ?? String.Empty;
            entity.Hint = entity.Hint.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ResearchMethodologyRepositoryValidator Validator { get; } = new ResearchMethodologyRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        public const int Code_MaxLength = 64;
        public const int Hint_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Code_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Code_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Hint_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Hint_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ResearchMethodologyRepositoryValidator: AbstractValidator<ResearchMethodology>
        {
            public ResearchMethodologyRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
                RuleFor(x => x.Code).Use(Code_ValidationRules);
                RuleFor(x => x.Hint).Use(Hint_ValidationRules);
            }
        }
    }
}

