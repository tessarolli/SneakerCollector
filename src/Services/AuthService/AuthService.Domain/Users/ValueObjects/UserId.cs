// <copyright file="UserId.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using SharedDefinitions.Domain.Common.DDD;

namespace AuthService.Domain.Users.ValueObjects;

/// <summary>
/// User Id Value Object.
/// </summary>
/// <param name="id">Id value if exists.</param>
public sealed class UserId(long? id = null) : ValueObject
{
    /// <summary>
    /// Gets the User ID.
    /// </summary>
    public long Value { get; } = id ?? 0;

    /// <summary>
    /// Method required for comparing value objects.
    /// </summary>
    /// <returns>An ienumerable with all the properties of the value object.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
