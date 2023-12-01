using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsFellowshipMetadata: IRepositoryMetadata<IsFellowship>
    {
        // Data Access
        
        public static string DatabaseTable => "IsFellowship";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static IsFellowship DataReader(SqlDataReader dr) => new IsFellowship(dr);
        
        public static List<SqlParameter> ExtractParameters(IsFellowship entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(IsFellowship.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(IsFellowship.Id):
                        break;
                    case nameof(IsFellowship.Name):
                        result.Add(new SqlParameter(nameof(IsFellowship.Name), entity.Name));
                        break;
                    case nameof(IsFellowship.SortOrder):
                        result.Add(new SqlParameter(nameof(IsFellowship.SortOrder), entity.SortOrder));
                        break;
                    case nameof(IsFellowship.Default):
                        result.Add(new SqlParameter(nameof(IsFellowship.Default), entity.Default));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<IsFellowship, IsFellowshipMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<IsFellowship, IsFellowshipMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<IsFellowship, IsFellowshipMetadata>();
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
        
        public static void Validate(IsFellowship entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static IsFellowshipRepositoryValidator Validator { get; } = new IsFellowshipRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class IsFellowshipRepositoryValidator: AbstractValidator<IsFellowship>
    {
        public IsFellowshipRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(IsFellowshipMetadata.Name_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

