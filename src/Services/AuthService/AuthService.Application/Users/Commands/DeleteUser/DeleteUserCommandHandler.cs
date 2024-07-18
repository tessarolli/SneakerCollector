// <copyright file="DeleteUserCommandHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Users.ValueObjects;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Commands.DeleteUser;

/// <summary>
/// Mediator Handler for the <see cref="DeleteUserCommand"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
/// </remarks>
/// <param name="userRepository">Injected UserRepository.</param>
public class DeleteUserCommandHandler(IUserRepository userRepository)
    : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.RemoveAsync(new UserId(request.Id));
    }
}
