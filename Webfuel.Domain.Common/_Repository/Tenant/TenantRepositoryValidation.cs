using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class TenantRepositoryValidator: AbstractValidator<Tenant>
    {
        public TenantRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(TenantRepositoryValidationRules.Name);
        }
    }
    public static class TenantRepositoryValidationRules
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

