// <copyright file="EntityValidator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.SharedDefinitions. All rights reserved.
// </copyright>

using FluentValidation;
using SneakerCollector.SharedDefinitions.Domain.Common.DDD;

namespace SneakerCollector.SharedDefinitions.Domain.Common;

/// <summary>
/// Abstract Base Class for Domain Entities Validators.
/// </summary>
/// <typeparam name="T">Type of the Domain Entity.</typeparam>
/// <typeparam name="TId">Type of the Id Property.</typeparam>
public abstract class EntityValidator<T, TId> : AbstractValidator<Entity<TId>>
where T : Entity<TId>
where TId : notnull
{
}
