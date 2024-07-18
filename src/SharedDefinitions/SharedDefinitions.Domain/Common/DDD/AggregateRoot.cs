// <copyright file="AggregateRoot.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Domain.Common.DDD;

/// <summary>
/// Base class for Aggregate Roots.
/// </summary>
/// <typeparam name="TId">The Type of the Id value object.</typeparam>
/// <param name="id">Entity Id.</param>
public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id)
    where TId : notnull
{
}