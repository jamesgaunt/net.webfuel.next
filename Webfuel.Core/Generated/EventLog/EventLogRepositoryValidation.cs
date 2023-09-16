using FluentValidation;

namespace Webfuel
{
    internal class EventLogRepositoryValidator: AbstractValidator<EventLog>
    {
        public EventLogRepositoryValidator()
        {
            RuleFor(x => x.Message).Use(EventLogRepositoryValidationRules.Message);
        }
    }
    public static class EventLogRepositoryValidationRules
    {
        
        public static void Message<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull();
        }
    }
}

