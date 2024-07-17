// <copyright file="PasswordHashingService.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector.SharedDefinitions. All rights reserved.
// </copyright>

using SneakerCollector.SharedDefinitions.Domain.Common.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace SneakerCollector.SharedDefinitions.Infrastructure.Services;

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
