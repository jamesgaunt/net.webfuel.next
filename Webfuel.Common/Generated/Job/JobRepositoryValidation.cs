using FluentValidation;

namespace Webfuel.Common
{
    internal class JobRepositoryValidator: AbstractValidator<Job>
    {
        public JobRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(JobRepositoryValidationRules.Name);
        }
    }
    public static class JobRepositoryValidationRules
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

