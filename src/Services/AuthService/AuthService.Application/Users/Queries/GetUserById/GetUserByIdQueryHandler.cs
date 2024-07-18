// <copyright file="GetUserByIdQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Users.Dtos;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Queries.GetUserById;

/// <summary>
/// Mediator Handler for the <see cref="GetUserByIdQuery"/>.
/// </summary>
/// <param name="userRepository">Injected UserRepository.</param>
public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    /// <inheritdoc/>
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var userDomainModel = await _userRepository.GetByIdAsync(query.Id);
        if (!userDomainModel.IsSuccess)
        {
            return Result.Fail(userDomainModel.Errors);
        }

        return Result.Ok(new UserDto(
            userDomainModel.Value.Id.Value,
            userDomainModel.Value.FirstName,
            userDomainModel.Value.LastName,
            userDomainModel.Value.Email,
            userDomainModel.Value.Password.Value,
            (int)userDomainModel.Value.Role,
            userDomainModel.Value.CreatedAtUtc));
    }
}