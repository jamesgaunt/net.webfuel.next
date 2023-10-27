using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingCallTypeMetadata: IRepositoryMetadata<FundingCallType>
    {
        // Data Access
        
        public static string DatabaseTable => "FundingCallType";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static FundingCallType DataReader(SqlDataReader dr) => new FundingCallType(dr);
        
        public static List<SqlParameter> ExtractParameters(FundingCallType entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(FundingCallType.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(FundingCallType.Id):
                        break;
                    case nameof(FundingCallType.Name):
                        result.Add(new SqlParameter(nameof(FundingCallType.Name), entity.Name));
                        break;
                    case nameof(FundingCallType.SortOrder):
                        result.Add(new SqlParameter(nameof(FundingCallType.SortOrder), entity.SortOrder));
                        break;
                    case nameof(FundingCallType.Default):
                        result.Add(new SqlParameter(nameof(FundingCallType.Default), entity.Default));
                        break;
                    case nameof(FundingCallType.Hidden):
                        result.Add(new SqlParameter(nameof(FundingCallType.Hidden), entity.Hidden));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<FundingCallType, FundingCallTypeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<FundingCallType, FundingCallTypeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<FundingCallType, FundingCallTypeMetadata>();
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
        
        public static void Validate(FundingCallType entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static FundingCallTypeRepositoryValidator Validator { get; } = new FundingCallTypeRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class FundingCallTypeRepositoryValidator: AbstractValidator<FundingCallType>
        {
            public FundingCallTypeRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

