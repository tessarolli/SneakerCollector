// <copyright file="IQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using MediatR;

namespace SharedDefinitions.Application.Abstractions.Messaging;

/// <summary>
/// IQueryHandler Interface.
/// </summary>
/// <typeparam name="TQuery">Type of Query.</typeparam>
/// <typeparam name="TResponse">Type of response.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}