// <copyright file="JwtTokenGenerator.cs" company="SneakerCollector">
// Copyright (c) SneakerCollector. All rights reserved.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedDefinitions.Infrastructure.Authentication;

namespace AuthService.Infrastructure.Authentication;

/// <summary>
/// Jwt Token Generator.
/// </summary>
/// <param name="optionsJwtSettings">JwtSetting injected.</param>
public class JwtTokenGenerator(IOptions<JwtSettings> optionsJwtSettings) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = optionsJwtSettings.Value;

    /// <inheritdoc/>
    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpireDays));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.Value.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expires,
            claims: claims,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
