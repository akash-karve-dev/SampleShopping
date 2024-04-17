using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Inventory.Application.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context)));

            var errros = validationResults
                  .Where(v => !v.IsValid)
                  .SelectMany(v => v.Errors)
                  .Select(validationFailure => new ValidationFailure(
                      validationFailure.PropertyName,
                      validationFailure.ErrorMessage))
                  .ToList();

            if (errros.Count != 0)
            {
                throw new ValidationException("Validation errors in request", errros);
            }

            return await next();
        }
    }
}