using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsCTUAlreadyInvolvedMetadata: IRepositoryMetadata<IsCTUAlreadyInvolved>
    {
        // Data Access
        
        public static string DatabaseTable => "IsCTUAlreadyInvolved";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsCTUAlreadyInvolved DataReader(SqlDataReader dr) => new IsCTUAlreadyInvolved(dr);
        
        public static List<SqlParameter> ExtractParameters(IsCTUAlreadyInvolved entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsCTUAlreadyInvolved.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsCTUAlreadyInvolved.Id):
                        break;
                    case nameof(IsCTUAlreadyInvolved.Name):
                        result.Add(new SqlParameter(nameof(IsCTUAlreadyInvolved.Name), entity.Name));
                        break;
                    case nameof(IsCTUAlreadyInvolved.SortOrder):
                        result.Add(new SqlParameter(nameof(IsCTUAlreadyInvolved.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsCTUAlreadyInvolved.Default):
                        result.Add(new SqlParameter(nameof(IsCTUAlreadyInvolved.Default), entity.Default));
                        break;
                    case nameof(IsCTUAlreadyInvolved.FreeText):
                        result.Add(new SqlParameter(nameof(IsCTUAlreadyInvolved.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsCTUAlreadyInvolved, IsCTUAlreadyInvolvedMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SortOrder";
                yield return "Default";
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
                yield return "FreeText";
            }
        }
        
        // Validation
        
        public static void Validate(IsCTUAlreadyInvolved entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsCTUAlreadyInvolvedRepositoryValidator Validator { get; } = new IsCTUAlreadyInvolvedRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsCTUAlreadyInvolvedRepositoryValidator: AbstractValidator<IsCTUAlreadyInvolved>
    {
        public IsCTUAlreadyInvolvedRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsCTUAlreadyInvolvedMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

