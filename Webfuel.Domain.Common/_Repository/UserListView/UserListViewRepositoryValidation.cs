using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class UserListViewRepositoryValidator: AbstractValidator<UserListView>
    {
        public UserListViewRepositoryValidator()
        {
            RuleFor(x => x.Email).Use(UserListViewRepositoryValidationRules.Email);
        }
    }
    public static class UserListViewRepositoryValidationRules
    {
        public const int Email_MaxLength = 64;
        
        public static void Email<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Email_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
}

