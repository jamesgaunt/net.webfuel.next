using FluentValidation;

namespace Webfuel
{
    internal class WidgetRepositoryValidator: AbstractValidator<Widget>
    {
        public WidgetRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(WidgetRepositoryValidationRules.Name);
            RuleFor(x => x.Age).Use(WidgetRepositoryValidationRules.Age);
            RuleFor(x => x.NullableInt).Use(WidgetRepositoryValidationRules.NullableInt);
            RuleFor(x => x.NullableString).Use(WidgetRepositoryValidationRules.NullableString);
            RuleFor(x => x.DayOfWeek).Use(WidgetRepositoryValidationRules.DayOfWeek);
        }
    }
    public static class WidgetRepositoryValidationRules
    {
        public const int Name_MaxLength = 64;
        public const int Age_MinValue = 0;
        public const int Age_MaxValue = 120;
        public const int NullableInt_MinValue = 0;
        public const int NullableInt_MaxValue = 10;
        public const int NullableString_MinLength = 0;
        public const int NullableString_MaxLength = 10;
        
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
        
        public static void NullableInt<T>(IRuleBuilder<T, int?> ruleBuilder)
        {
            ruleBuilder
                .GreaterThanOrEqualTo(NullableInt_MinValue).When(x => x != null, ApplyConditionTo.CurrentValidator)
                .LessThanOrEqualTo(NullableInt_MaxValue).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void NullableString<T>(IRuleBuilder<T, string?> ruleBuilder)
        {
            ruleBuilder
                .MinimumLength(NullableString_MinLength).When(x => x != null, ApplyConditionTo.CurrentValidator)
                .MaximumLength(NullableString_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void DayOfWeek<T>(IRuleBuilder<T, DayOfWeek> ruleBuilder)
        {
            ruleBuilder
                .IsInEnum();
        }
    }
}

