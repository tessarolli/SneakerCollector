// <copyright file="IEntity.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

namespace SharedDefinitions.Domain.Common.Abstractions;

/// <summary>
/// IEntity interface.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the Id of the Entity.
    /// </summary>
    /// <returns>Returns the Entity's Id Value Object.</returns>
    object GetId();
}
