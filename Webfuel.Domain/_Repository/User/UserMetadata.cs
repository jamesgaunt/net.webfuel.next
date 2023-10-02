using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserMetadata: IRepositoryMetadata<User>
    {
        // Data Access
        
        public static string DatabaseTable => "User";
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static User DataReader(SqlDataReader dr) => new User(dr);
        
        public static  IEnumerable<SqlParameter> DataWriter(User entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter>();
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(User.Id):
                        result.Add(new SqlParameter(nameof(User.Id), entity.Id));
                        break;
                    case nameof(User.Email):
                        result.Add(new SqlParameter(nameof(User.Email), entity.Email));
                        break;
                    case nameof(User.FirstName):
                        result.Add(new SqlParameter(nameof(User.FirstName), entity.FirstName));
                        break;
                    case nameof(User.LastName):
                        result.Add(new SqlParameter(nameof(User.LastName), entity.LastName));
                        break;
                    case nameof(User.PasswordHash):
                        result.Add(new SqlParameter(nameof(User.PasswordHash), entity.PasswordHash));
                        break;
                    case nameof(User.PasswordSalt):
                        result.Add(new SqlParameter(nameof(User.PasswordSalt), entity.PasswordSalt));
                        break;
                    case nameof(User.PasswordResetAt):
                        result.Add(new SqlParameter(nameof(User.PasswordResetAt), entity.PasswordResetAt));
                        break;
                    case nameof(User.PasswordResetToken):
                        result.Add(new SqlParameter(nameof(User.PasswordResetToken), entity.PasswordResetToken));
                        break;
                    case nameof(User.PasswordResetValidUntil):
                        result.Add(new SqlParameter(nameof(User.PasswordResetValidUntil), entity.PasswordResetValidUntil));
                        break;
                    case nameof(User.Developer):
                        result.Add(new SqlParameter(nameof(User.Developer), entity.Developer));
                        break;
                    case nameof(User.Birthday):
                        result.Add(new SqlParameter(nameof(User.Birthday), entity.Birthday));
                        break;
                    case nameof(User.CreatedAt):
                        result.Add(new SqlParameter(nameof(User.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(User.UserGroupId):
                        result.Add(new SqlParameter(nameof(User.UserGroupId), entity.UserGroupId));
                        break;
                }
            }
            return result;
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "FirstName";
                yield return "LastName";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "Developer";
                yield return "Birthday";
                yield return "CreatedAt";
                yield return "UserGroupId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "FirstName";
                yield return "LastName";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "Developer";
                yield return "Birthday";
                yield return "CreatedAt";
                yield return "UserGroupId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Email";
                yield return "FirstName";
                yield return "LastName";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "Developer";
                yield return "Birthday";
                yield return "CreatedAt";
                yield return "UserGroupId";
            }
        }
        
        // Validation
        
        public static void Validate(User entity)
        {
            entity.Email = entity.Email ?? String.Empty;
            entity.Email = entity.Email.Trim();
            entity.FirstName = entity.FirstName ?? String.Empty;
            entity.FirstName = entity.FirstName.Trim();
            entity.LastName = entity.LastName ?? String.Empty;
            entity.LastName = entity.LastName.Trim();
            entity.PasswordHash = entity.PasswordHash ?? String.Empty;
            entity.PasswordHash = entity.PasswordHash.Trim();
            entity.PasswordSalt = entity.PasswordSalt ?? String.Empty;
            entity.PasswordSalt = entity.PasswordSalt.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static UserRepositoryValidator Validator { get; } = new UserRepositoryValidator();
        
        public const int Email_MaxLength = 64;
        public const int FirstName_MaxLength = 64;
        public const int LastName_MaxLength = 64;
        public const int PasswordHash_MaxLength = 256;
        public const int PasswordSalt_MaxLength = 256;
        
        public static void Email<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Email_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void FirstName<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FirstName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LastName<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LastName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void PasswordHash<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(PasswordHash_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void PasswordSalt<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(PasswordSalt_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class UserRepositoryValidator: AbstractValidator<User>
        {
            public UserRepositoryValidator()
            {
                RuleFor(x => x.Email).Use(UserRepositoryValidationRules.Email);
                RuleFor(x => x.FirstName).Use(UserRepositoryValidationRules.FirstName);
                RuleFor(x => x.LastName).Use(UserRepositoryValidationRules.LastName);
                RuleFor(x => x.PasswordHash).Use(UserRepositoryValidationRules.PasswordHash);
                RuleFor(x => x.PasswordSalt).Use(UserRepositoryValidationRules.PasswordSalt);
            }
        }
    }
}

