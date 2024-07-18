// <copyright file="GetUsersListQueryHandler.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Users.Dtos;
using FluentResults;
using SharedDefinitions.Application.Abstractions.Messaging;

namespace AuthService.Application.Users.Queries.GetUsersList;

/// <summary>
/// Mediator Handler for the <see cref="GetUsersListQuery"/>.
/// </summary>
public class GetUsersListQueryHandler : IQueryHandler<GetUsersListQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersListQueryHandler"/> class.
    /// </summary>
    /// <param name="userRepository">Injected UserRepository.</param>
    public GetUsersListQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<List<UserDto>>> Handle(GetUsersListQuery query, CancellationToken cancellationToken)
    {
        var result = new List<UserDto>();

        var usersResult = await _userRepository.GetAllAsync();
        if (usersResult.IsSuccess)
        {
            foreach (var userResult in usersResult.Value)
            {
                result.Add(new UserDto(
                    userResult.Id.Value,
                    userResult.FirstName,
                    userResult.LastName,
                    userResult.Email,
                    userResult.Password.Value,
                    (int)userResult.Role,
                    userResult.CreatedAtUtc));
            }
        }

        return Result.Ok(result);
    }
}