using FluentValidation;
using MediatR;

namespace Webfuel
{
    [CommandValidator]
    public class UpdateWidgetCommandValidator : AbstractValidator<UpdateWidgetCommand>
    {
        public UpdateWidgetCommandValidator()
        {
            RuleFor(x => x.Name)
                .Use(WidgetRepositoryValidationRules.Name);

            RuleFor(x => x.Age)
                .Use(WidgetRepositoryValidationRules.Age);
        }
    }
}
