using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserLoginMetadata: IRepositoryMetadata<UserLogin>
    {
        // Data Access
        
        public static string DatabaseTable => "UserLogin";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
        public static UserLogin DataReader(SqlDataReader dr) => new UserLogin(dr);
        
        public static List<SqlParameter> ExtractParameters(UserLogin entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(UserLogin.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(UserLogin.Id):
                        break;
                    case nameof(UserLogin.UserId):
                        result.Add(new SqlParameter(nameof(UserLogin.UserId), entity.UserId));
                        break;
                    case nameof(UserLogin.Email):
                        result.Add(new SqlParameter(nameof(UserLogin.Email), entity.Email));
                        break;
                    case nameof(UserLogin.IPAddress):
                        result.Add(new SqlParameter(nameof(UserLogin.IPAddress), entity.IPAddress));
                        break;
                    case nameof(UserLogin.Successful):
                        result.Add(new SqlParameter(nameof(UserLogin.Successful), entity.Successful));
                        break;
                    case nameof(UserLogin.Reason):
                        result.Add(new SqlParameter(nameof(UserLogin.Reason), entity.Reason));
                        break;
                    case nameof(UserLogin.CreatedAt):
                        result.Add(new SqlParameter(nameof(UserLogin.CreatedAt), entity.CreatedAt));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<UserLogin, UserLoginMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<UserLogin, UserLoginMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<UserLogin, UserLoginMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "UserId";
                yield return "Email";
                yield return "IPAddress";
                yield return "Successful";
                yield return "Reason";
                yield return "CreatedAt";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "UserId";
                yield return "Email";
                yield return "IPAddress";
                yield return "Successful";
                yield return "Reason";
                yield return "CreatedAt";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "UserId";
                yield return "Email";
                yield return "IPAddress";
                yield return "Successful";
                yield return "Reason";
                yield return "CreatedAt";
            }
        }
        
        // Validation
        
        public static void Validate(UserLogin entity)
        {
            entity.Email = entity.Email ?? String.Empty;
            entity.Email = entity.Email.Trim();
            entity.IPAddress = entity.IPAddress ?? String.Empty;
            entity.IPAddress = entity.IPAddress.Trim();
            entity.Reason = entity.Reason ?? String.Empty;
            entity.Reason = entity.Reason.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserLoginRepositoryValidator Validator { get; } = new UserLoginRepositoryValidator();
        
        public const int Email_MaxLength = 128;
        public const int IPAddress_MaxLength = 128;
        public const int Reason_MaxLength = 128;
        
        public static void Email_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Email_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void IPAddress_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(IPAddress_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Reason_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Reason_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class UserLoginRepositoryValidator: AbstractValidator<UserLogin>
    {
        public UserLoginRepositoryValidator()
        {
            RuleFor(x => x.Email).Use(UserLoginMetadata.Email_ValidationRules);
            RuleFor(x => x.IPAddress).Use(UserLoginMetadata.IPAddress_ValidationRules);
            RuleFor(x => x.Reason).Use(UserLoginMetadata.Reason_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

