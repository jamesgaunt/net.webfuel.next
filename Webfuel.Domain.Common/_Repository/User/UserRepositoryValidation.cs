using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class UserRepositoryValidator: AbstractValidator<User>
    {
        public UserRepositoryValidator()
        {
            RuleFor(x => x.Email).Use(UserRepositoryValidationRules.Email);
            RuleFor(x => x.FirstName).Use(UserRepositoryValidationRules.FirstName);
            RuleFor(x => x.LastName).Use(UserRepositoryValidationRules.LastName);
        }
    }
    public static class UserRepositoryValidationRules
    {
        public const int Email_MaxLength = 64;
        public const int FirstName_MaxLength = 64;
        public const int LastName_MaxLength = 64;
        
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
    }
}

