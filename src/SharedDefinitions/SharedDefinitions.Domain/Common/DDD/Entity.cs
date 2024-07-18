// <copyright file="Entity.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using FluentResults;
using FluentValidation;
using SharedDefinitions.Domain.Common.Abstractions;

namespace SharedDefinitions.Domain.Common.DDD;

/// <summary>
/// An abstract class that should be implemented to represent an Entity of the Domain.
/// </summary>
/// <typeparam name="TId">The Type of the Id value object.</typeparam>
/// <param name="id">Entity Id.</param>
public abstract class Entity<TId>(TId id) : IEntity, IEqualityComparer<Entity<TId>>
    where TId : notnull
{
    /// <summary>
    /// Gets or sets the Identificator of this entity.
    /// </summary>
    public TId Id { get; protected set; } = id;

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    /// <inheritdoc/>
    public bool Equals(Entity<TId>? x, Entity<TId>? y)
    {
        if (x is null || y is null)
        {
            return false;
        }

        return x.Id.Equals(y.Id);
    }

    /// <inheritdoc/>
    public int GetHashCode([DisallowNull] Entity<TId> obj)
    {
        return obj.Id.GetHashCode();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <inheritdoc/>
    public object GetId()
    {
        return Id;
    }

    /// <summary>
    /// Gets the IValidator for this entity.
    /// </summary>
    /// <returns>IValidator for this entity.</returns>
    protected abstract object GetValidator();

    /// <summary>
    /// Perform Validation on the entity.
    /// </summary>
    /// <returns>Result with Success or Failure status.</returns>
    protected Result Validate()
    {
        var validatorObject = GetValidator();
        if (validatorObject is not AbstractValidator<Entity<TId>> validator)
        {
            return Result.Ok();
        }

        var validationResult = validator.Validate(this);

        if (validationResult.IsValid)
        {
            return Result.Ok();
        }
        else
        {
            return Result.Fail(
                validationResult.Errors
                    .Where(validationFailure => validationFailure is not null)
                    .Select(failure => new ValidationError(
                       failure.ErrorMessage,
                       new Error(failure.PropertyName)))
                    .Distinct());
        }
    }
}