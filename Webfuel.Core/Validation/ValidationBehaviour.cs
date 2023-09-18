using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Webfuel.MediatR
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly List<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators.ToList();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Count > 0) 
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = new List<ValidationFailure>();

                foreach (var validator in _validators)
                {
                    var result = await validator.ValidateAsync(context, cancellationToken);
                    if (result.Errors != null)
                        failures.AddRange(result.Errors);
                }

                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
