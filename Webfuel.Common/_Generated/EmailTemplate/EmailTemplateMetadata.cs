using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class EmailTemplateMetadata: IRepositoryMetadata<EmailTemplate>
    {
        // Data Access
        
        public static string DatabaseTable => "EmailTemplate";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static EmailTemplate DataReader(SqlDataReader dr) => new EmailTemplate(dr);
        
        public static List<SqlParameter> ExtractParameters(EmailTemplate entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(EmailTemplate.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(EmailTemplate.Id):
                        break;
                    case nameof(EmailTemplate.Name):
                        result.Add(new SqlParameter(nameof(EmailTemplate.Name), entity.Name));
                        break;
                    case nameof(EmailTemplate.SendTo):
                        result.Add(new SqlParameter(nameof(EmailTemplate.SendTo), entity.SendTo));
                        break;
                    case nameof(EmailTemplate.SendCc):
                        result.Add(new SqlParameter(nameof(EmailTemplate.SendCc), entity.SendCc));
                        break;
                    case nameof(EmailTemplate.SendBcc):
                        result.Add(new SqlParameter(nameof(EmailTemplate.SendBcc), entity.SendBcc));
                        break;
                    case nameof(EmailTemplate.SentBy):
                        result.Add(new SqlParameter(nameof(EmailTemplate.SentBy), entity.SentBy));
                        break;
                    case nameof(EmailTemplate.ReplyTo):
                        result.Add(new SqlParameter(nameof(EmailTemplate.ReplyTo), entity.ReplyTo));
                        break;
                    case nameof(EmailTemplate.Subject):
                        result.Add(new SqlParameter(nameof(EmailTemplate.Subject), entity.Subject));
                        break;
                    case nameof(EmailTemplate.HtmlTemplate):
                        result.Add(new SqlParameter(nameof(EmailTemplate.HtmlTemplate), entity.HtmlTemplate));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<EmailTemplate, EmailTemplateMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<EmailTemplate, EmailTemplateMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<EmailTemplate, EmailTemplateMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SendTo";
                yield return "SendCc";
                yield return "SendBcc";
                yield return "SentBy";
                yield return "ReplyTo";
                yield return "Subject";
                yield return "HtmlTemplate";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "SendTo";
                yield return "SendCc";
                yield return "SendBcc";
                yield return "SentBy";
                yield return "ReplyTo";
                yield return "Subject";
                yield return "HtmlTemplate";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "SendTo";
                yield return "SendCc";
                yield return "SendBcc";
                yield return "SentBy";
                yield return "ReplyTo";
                yield return "Subject";
                yield return "HtmlTemplate";
            }
        }
        
        // Validation
        
        public static void Validate(EmailTemplate entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.SendTo = entity.SendTo ?? String.Empty;
            entity.SendTo = entity.SendTo.Trim();
            entity.SendCc = entity.SendCc ?? String.Empty;
            entity.SendCc = entity.SendCc.Trim();
            entity.SendBcc = entity.SendBcc ?? String.Empty;
            entity.SendBcc = entity.SendBcc.Trim();
            entity.SentBy = entity.SentBy ?? String.Empty;
            entity.SentBy = entity.SentBy.Trim();
            entity.ReplyTo = entity.ReplyTo ?? String.Empty;
            entity.ReplyTo = entity.ReplyTo.Trim();
            entity.Subject = entity.Subject ?? String.Empty;
            entity.Subject = entity.Subject.Trim();
            entity.HtmlTemplate = entity.HtmlTemplate ?? String.Empty;
            entity.HtmlTemplate = entity.HtmlTemplate.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static EmailTemplateRepositoryValidator Validator { get; } = new EmailTemplateRepositoryValidator();
        
        public const int Name_MaxLength = 128;
        public const int SentBy_MaxLength = 128;
        public const int ReplyTo_MaxLength = 128;
        
        public static void Name_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SendTo_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void SendCc_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void SendBcc_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void SentBy_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SentBy_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ReplyTo_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ReplyTo_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Subject_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public static void HtmlTemplate_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
    
    public partial class EmailTemplateRepositoryValidator: AbstractValidator<EmailTemplate>
    {
        public EmailTemplateRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(EmailTemplateMetadata.Name_ValidationRules);
            RuleFor(x => x.SendTo).Use(EmailTemplateMetadata.SendTo_ValidationRules);
            RuleFor(x => x.SendCc).Use(EmailTemplateMetadata.SendCc_ValidationRules);
            RuleFor(x => x.SendBcc).Use(EmailTemplateMetadata.SendBcc_ValidationRules);
            RuleFor(x => x.SentBy).Use(EmailTemplateMetadata.SentBy_ValidationRules);
            RuleFor(x => x.ReplyTo).Use(EmailTemplateMetadata.ReplyTo_ValidationRules);
            RuleFor(x => x.Subject).Use(EmailTemplateMetadata.Subject_ValidationRules);
            RuleFor(x => x.HtmlTemplate).Use(EmailTemplateMetadata.HtmlTemplate_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

