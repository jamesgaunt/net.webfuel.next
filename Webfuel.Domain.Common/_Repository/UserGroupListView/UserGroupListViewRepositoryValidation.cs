using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class UserGroupListViewRepositoryValidator: AbstractValidator<UserGroupListView>
    {
        public UserGroupListViewRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(UserGroupListViewRepositoryValidationRules.Name);
        }
    }
    public static class UserGroupListViewRepositoryValidationRules
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

