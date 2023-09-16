using FluentValidation;
using MediatR;

namespace Webfuel
{
    public class CreateWidgetCommandValidator : AbstractValidator<CreateWidgetCommand>
    {
        public CreateWidgetCommandValidator()
        {
            RuleFor(x => x.Name)
                .Use(WidgetRepositoryValidationRules.Name)
                .NotEqual("Fred");

            RuleFor(x => x.Age)
                .Use(WidgetRepositoryValidationRules.Age);
        }
    }
}
