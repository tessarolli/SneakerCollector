// <copyright file="PipelineRequestValidationBehavior.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using FluentValidation;
using MediatR;
using SharedDefinitions.Domain.Common;

namespace SharedDefinitions.Application.Common.Behaviors;

/// <summary>
/// Provides a generic Validation Behavior for incoming requests.
/// </summary>
/// <typeparam name="TRequest">The type of the incoming request contract.</typeparam>
/// <typeparam name="TResponse">The type of the request's response contract.</typeparam>
/// <param name="validators">Injected validators.</param>
public class PipelineRequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> validators = validators;

    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        ValidationError[] errors = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new ValidationError(
                failure.ErrorMessage,
                new Error(failure.PropertyName)))
            .Distinct()
            .ToArray();

        if (errors.Length != 0)
        {
            return (TResponse)typeof(Result<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResponse).GenericTypeArguments[0])
                .GetMethods()
                .First(x => x.Name == "WithErrors")
                .Invoke(Activator.CreateInstance(typeof(TResponse)), [errors])!;
        }

        return await next();
    }
}
