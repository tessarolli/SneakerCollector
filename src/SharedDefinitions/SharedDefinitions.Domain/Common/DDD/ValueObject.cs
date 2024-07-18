// <copyright file="ValueObject.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Domain.Common.DDD;

/// <summary>
/// An abstract class that should be implemented to represent a Value Object and provide equality comparison.
/// </summary>
public abstract class ValueObject : IEqualityComparer<ValueObject>
{
    /// <summary>
    /// Equality operator.
    /// </summary>
    /// <param name="left">Left value object.</param>
    /// <param name="right">Right value object.</param>
    /// <returns>True if both value objects are equal.</returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator.
    /// </summary>
    /// <param name="left">Left value object.</param>
    /// <param name="right">Right value object.</param>
    /// <returns>True if both value objects are not equal.</returns>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Determines whether two objects of type T are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>true if the specified objects are equal; otherwise, false.</returns>
    public bool Equals(ValueObject? x, ValueObject? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.GetEqualityComponents().SequenceEqual(y.GetEqualityComponents());
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is ValueObject other)
        {
            return Equals(this, other);
        }

        return false;
    }

    /// <summary>
    /// Returns a hash code for the specified object.
    /// </summary>
    /// <param name="obj">The object for which to get a hash code.</param>
    /// <returns>A hash code for the specified object.</returns>
    public int GetHashCode(ValueObject obj)
    {
        return obj.GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    /// <summary>
    /// Abstract method to get all properties from the value object to calculate equality.
    /// Disclaimer: A value object is only equal when all of its properties have equal values.
    /// </summary>
    /// <returns>Should be implemented to yield return propertyName for each property of the value object.</returns>
    public abstract IEnumerable<object> GetEqualityComponents();
}