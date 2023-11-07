using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class EmailLogMetadata: IRepositoryMetadata<EmailLog>
    {
        // Data Access
        
        public static string DatabaseTable => "EmailLog";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
        public static EmailLog DataReader(SqlDataReader dr) => new EmailLog(dr);
        
        public static List<SqlParameter> ExtractParameters(EmailLog entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(EmailLog.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(EmailLog.Id):
                        break;
                    case nameof(EmailLog.EntityId):
                        result.Add(new SqlParameter(nameof(EmailLog.EntityId), entity.EntityId));
                        break;
                    case nameof(EmailLog.SendTo):
                        result.Add(new SqlParameter(nameof(EmailLog.SendTo), entity.SendTo));
                        break;
                    case nameof(EmailLog.SendCc):
                        result.Add(new SqlParameter(nameof(EmailLog.SendCc), entity.SendCc));
                        break;
                    case nameof(EmailLog.SendBcc):
                        result.Add(new SqlParameter(nameof(EmailLog.SendBcc), entity.SendBcc));
                        break;
                    case nameof(EmailLog.SentBy):
                        result.Add(new SqlParameter(nameof(EmailLog.SentBy), entity.SentBy));
                        break;
                    case nameof(EmailLog.ReplyTo):
                        result.Add(new SqlParameter(nameof(EmailLog.ReplyTo), entity.ReplyTo));
                        break;
                    case nameof(EmailLog.Subject):
                        result.Add(new SqlParameter(nameof(EmailLog.Subject), entity.Subject));
                        break;
                    case nameof(EmailLog.HtmlBody):
                        result.Add(new SqlParameter(nameof(EmailLog.HtmlBody), entity.HtmlBody));
                        break;
                    case nameof(EmailLog.SentAt):
                        result.Add(new SqlParameter(nameof(EmailLog.SentAt), entity.SentAt));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<EmailLog, EmailLogMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<EmailLog, EmailLogMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<EmailLog, EmailLogMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "EntityId";
                yield return "SendTo";
                yield return "SendCc";
                yield return "SendBcc";
                yield return "SentBy";
                yield return "ReplyTo";
                yield return "Subject";
                yield return "HtmlBody";
                yield return "SentAt";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "EntityId";
                yield return "SendTo";
                yield return "SendCc";
                yield return "SendBcc";
                yield return "SentBy";
                yield return "ReplyTo";
                yield return "Subject";
                yield return "HtmlBody";
                yield return "SentAt";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "EntityId";
                yield return "SendTo";
                yield return "SendCc";
                yield return "SendBcc";
                yield return "SentBy";
                yield return "ReplyTo";
                yield return "Subject";
                yield return "HtmlBody";
                yield return "SentAt";
            }
        }
        
        // Validation
        
        public static void Validate(EmailLog entity)
        {
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
            entity.HtmlBody = entity.HtmlBody ?? String.Empty;
            entity.HtmlBody = entity.HtmlBody.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static EmailLogRepositoryValidator Validator { get; } = new EmailLogRepositoryValidator();
        
        public const int SentBy_MaxLength = 64;
        public const int ReplyTo_MaxLength = 64;
        
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
        
        public static void HtmlBody_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
        
        public class EmailLogRepositoryValidator: AbstractValidator<EmailLog>
        {
            public EmailLogRepositoryValidator()
            {
                RuleFor(x => x.SendTo).Use(SendTo_ValidationRules);
                RuleFor(x => x.SendCc).Use(SendCc_ValidationRules);
                RuleFor(x => x.SendBcc).Use(SendBcc_ValidationRules);
                RuleFor(x => x.SentBy).Use(SentBy_ValidationRules);
                RuleFor(x => x.ReplyTo).Use(ReplyTo_ValidationRules);
                RuleFor(x => x.Subject).Use(Subject_ValidationRules);
                RuleFor(x => x.HtmlBody).Use(HtmlBody_ValidationRules);
            }
        }
    }
}

