// <copyright file="ICommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using FluentResults;
using MediatR;

namespace SharedDefinitions.Application.Abstractions.Messaging;

/// <summary>
/// ICommandHandler Interface.
/// </summary>
/// <typeparam name="TCommand">Command Type.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

/// <summary>
/// ICommandHandler Interface.
/// </summary>
/// <typeparam name="TCommand">Command Type.</typeparam>
/// <typeparam name="TResponse">Response Type.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}