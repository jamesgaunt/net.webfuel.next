using FluentValidation;
using MediatR;

namespace Webfuel
{
    public class UpdateWidgetCommandValidator : AbstractValidator<CreateWidgetCommand>
    {
        public UpdateWidgetCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(64)
                .NotEmpty();

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
        }
    }
}
