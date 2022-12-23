namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
                IPipelineBehavior<TRequest, TResponse>
                    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationFailures = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(error => error is not null);

            if (validationFailures.Any())
                throw new ValidationException(validationFailures);
        }

        return await next();
    }
}
