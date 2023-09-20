using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class TenantDomainRepositoryValidator: AbstractValidator<TenantDomain>
    {
        public TenantDomainRepositoryValidator()
        {
            RuleFor(x => x.Domain).Use(TenantDomainRepositoryValidationRules.Domain);
            RuleFor(x => x.RedirectTo).Use(TenantDomainRepositoryValidationRules.RedirectTo);
        }
    }
    public static class TenantDomainRepositoryValidationRules
    {
        public const int Domain_MaxLength = 64;
        public const int RedirectTo_MaxLength = 64;
        
        public static void Domain<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Domain_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void RedirectTo<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(RedirectTo_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
}

