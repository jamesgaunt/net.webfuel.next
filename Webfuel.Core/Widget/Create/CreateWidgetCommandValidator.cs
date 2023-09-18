using FluentValidation;
using MediatR;

namespace Webfuel
{
    [CommandValidator]
    public class CreateWidgetCommandValidator : AbstractValidator<CreateWidgetCommand>
    {
        public CreateWidgetCommandValidator()
        {
            RuleFor(x => x.Name)
                .Use(WidgetRepositoryValidationRules.Name);

            RuleFor(x => x.Age)
                .Use(WidgetRepositoryValidationRules.Age);
        }
    }
}
