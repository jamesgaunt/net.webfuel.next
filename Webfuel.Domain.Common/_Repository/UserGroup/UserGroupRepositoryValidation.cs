using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class UserGroupRepositoryValidator: AbstractValidator<UserGroup>
    {
        public UserGroupRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(UserGroupRepositoryValidationRules.Name);
        }
    }
    public static class UserGroupRepositoryValidationRules
    {
        public const int Name_MaxLength = 64;
        
        public static void Name<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
}

