// <copyright file="DeleteUserCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.Services.AuthService. All rights reserved.
// </copyright>

using FluentResults;
using SneakerCollector.Services.AuthService.Application.Abstractions.Repositories;
using SneakerCollector.Services.AuthService.Domain.Users.ValueObjects;
using SneakerCollector.SharedDefinitions.Application.Abstractions.Messaging;

namespace SneakerCollector.Services.AuthService.Application.Users.Commands.DeleteUser;

/// <summary>
/// Mediator Handler for the <see cref="DeleteUserCommand"/>.
/// </summary>
public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Injected UserRepository.</param>
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.RemoveAsync(new UserId(request.Id));
    }
}
