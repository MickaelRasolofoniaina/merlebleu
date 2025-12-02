using Merlebleu.Foundation.CQRS;
using FluentValidation;
using MediatR;

namespace Merlebleu.Foundation.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse> where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        return await next(cancellationToken);
    }

    public async Task Handle(TRequest request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        await next(cancellationToken);
    }

    private async Task Validate(TRequest request, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(r => r.Errors.Count != 0).SelectMany(r => r.Errors).ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(string.Join(" \n ", failures.Select(f => f.ErrorMessage)), failures);
        }
    }
}
