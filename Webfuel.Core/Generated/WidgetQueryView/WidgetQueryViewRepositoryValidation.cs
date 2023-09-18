using FluentValidation;

namespace Webfuel
{
    internal class WidgetQueryViewRepositoryValidator: AbstractValidator<WidgetQueryView>
    {
        public WidgetQueryViewRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(WidgetQueryViewRepositoryValidationRules.Name);
            RuleFor(x => x.Age).Use(WidgetQueryViewRepositoryValidationRules.Age);
        }
    }
    public static class WidgetQueryViewRepositoryValidationRules
    {
        public const int Name_MaxLength = 64;
        public const int Age_MinValue = 0;
        public const int Age_MaxValue = 120;
        
        public static void Name<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Age<T>(IRuleBuilder<T, int> ruleBuilder)
        {
            ruleBuilder
                .GreaterThanOrEqualTo(Age_MinValue)
                .LessThanOrEqualTo(Age_MaxValue);
        }
    }
}

