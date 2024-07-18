// <copyright file="EntityValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentValidation;
using SharedDefinitions.Domain.Common.DDD;

namespace SharedDefinitions.Domain.Common;

/// <summary>
/// Abstract Base Class for Domain Entities Validators.
/// </summary>
/// <typeparam name="TId">Type of the Id Property.</typeparam>
public abstract class EntityValidator<TId> : AbstractValidator<Entity<TId>>
    where TId : notnull
{
}