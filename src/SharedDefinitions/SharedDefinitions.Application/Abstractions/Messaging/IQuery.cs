// <copyright file="IQuery.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using MediatR;

namespace SharedDefinitions.Application.Abstractions.Messaging;

/// <summary>
/// IQuery Interface.
/// </summary>
/// <typeparam name="TResponse">Type of response.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}