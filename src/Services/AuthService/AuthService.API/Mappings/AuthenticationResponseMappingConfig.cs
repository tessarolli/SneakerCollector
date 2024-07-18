// <copyright file="AuthenticationResponseMappingConfig.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using AuthService.Application.Authentication.Results;
using AuthService.Contracts.Authentication;
using Mapster;

namespace AuthService.API.Mappings;

/// <summary>
/// Authentication Mapster Config Mapping.
/// </summary>
public class AuthenticationResponseMappingConfig : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest, src => src.User);
    }
}
