// <copyright file="RoleAuthorizeAttribute.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Contracts.Enums;
using Microsoft.AspNetCore.Authorization;

namespace SharedDefinitions.Presentation.Attributes;

/// <summary>
/// Role Based Authorization support.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class RoleAuthorizeAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleAuthorizeAttribute"/> class.
    /// Use this to authorize for Any role.
    /// </summary>
    public RoleAuthorizeAttribute()
    {
        Roles = string.Join(',', Enum.GetNames(typeof(Roles)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleAuthorizeAttribute"/> class.
    /// </summary>
    /// <param name="role">The required role.</param>
    public RoleAuthorizeAttribute(Roles role)
    {
        Roles = role.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleAuthorizeAttribute"/> class.
    /// </summary>
    /// <param name="roles">The array of required roles.</param>
    public RoleAuthorizeAttribute(Roles[] roles)
    {
        Roles = string.Join(',', roles);
    }
}