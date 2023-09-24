using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    internal class UserRepositoryValidator: AbstractValidator<User>
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
    public static class UserRepositoryValidationRules
    {
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
    }
}

