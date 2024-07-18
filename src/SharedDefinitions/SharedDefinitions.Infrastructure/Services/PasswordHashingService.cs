// <copyright file="PasswordHashingService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Identity;
using SharedDefinitions.Domain.Common.Abstractions;

namespace SharedDefinitions.Infrastructure.Services;

/// <summary>
/// Password hashing service.
/// </summary>
public class PasswordHashingService : IPasswordHashingService
{
    /// <inheritdoc/>
    public string HashPassword(string password, int iterations = 10000)
    {
        return new PasswordHasher<object>().HashPassword(null!, password);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string passwordToVerify, string hashedPassword)
    {
        var passwordVerificationResult = new PasswordHasher<object>().VerifyHashedPassword(null!, hashedPassword, passwordToVerify);
        return passwordVerificationResult switch
        {
            PasswordVerificationResult.Failed => false,
            PasswordVerificationResult.Success => true,
            PasswordVerificationResult.SuccessRehashNeeded => true,
            _ => false
        };
    }
}
