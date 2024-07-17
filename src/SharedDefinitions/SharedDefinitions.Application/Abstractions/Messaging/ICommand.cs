// <copyright file="ICommand.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.SharedDefinitions. All rights reserved.
// </copyright>

using FluentResults;
using MediatR;

namespace SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

/// <summary>
/// ICommand Interface.
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// ICommand Interface with Result of TResponse.
/// </summary>
/// <typeparam name="TResponse">Type of response.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}