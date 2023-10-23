using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SubmissionOutcomeMetadata: IRepositoryMetadata<SubmissionOutcome>
    {
        // Data Access
        
        public static string DatabaseTable => "SubmissionOutcome";
        
        public static string DefaultOrderBy => "ORDER BY SortOrder ASC";
        
        public static SubmissionOutcome DataReader(SqlDataReader dr) => new SubmissionOutcome(dr);
        
        public static List<SqlParameter> ExtractParameters(SubmissionOutcome entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SubmissionOutcome.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SubmissionOutcome.Id):
                        break;
                    case nameof(SubmissionOutcome.Name):
                        result.Add(new SqlParameter(nameof(SubmissionOutcome.Name), entity.Name));
                        break;
                    case nameof(SubmissionOutcome.SortOrder):
                        result.Add(new SqlParameter(nameof(SubmissionOutcome.SortOrder), entity.SortOrder));
                        break;
                    case nameof(SubmissionOutcome.Default):
                        result.Add(new SqlParameter(nameof(SubmissionOutcome.Default), entity.Default));
                        break;
                    case nameof(SubmissionOutcome.Hidden):
                        result.Add(new SqlParameter(nameof(SubmissionOutcome.Hidden), entity.Hidden));
                        break;
                    case nameof(SubmissionOutcome.FreeText):
                        result.Add(new SqlParameter(nameof(SubmissionOutcome.FreeText), entity.FreeText));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SubmissionOutcome, SubmissionOutcomeMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SubmissionOutcome, SubmissionOutcomeMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SubmissionOutcome, SubmissionOutcomeMetadata>();
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
        
        public static void Validate(SubmissionOutcome entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SubmissionOutcomeRepositoryValidator Validator { get; } = new SubmissionOutcomeRepositoryValidator();
        
        public const int Name_MaxLength = 64;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class SubmissionOutcomeRepositoryValidator: AbstractValidator<SubmissionOutcome>
        {
            public SubmissionOutcomeRepositoryValidator()
            {
                RuleFor(x => x.Name).Use(Name_ValidationRules);
            }
        }
    }
}

