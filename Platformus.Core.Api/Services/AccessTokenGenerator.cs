// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Domain.Models;

namespace Platformus.Core.Api.Services;

public class AccessTokenGenerator : IAccessTokenGenerator
{
  private readonly AccessTokenGeneratorOptions accessTokenGeneratorOptions;

  public AccessTokenGenerator(IOptions<AccessTokenGeneratorOptions> accessTokenGeneratorOptions)
  {
    this.accessTokenGeneratorOptions = accessTokenGeneratorOptions.Value;
  }

  public string Generate(User user)
  {
    SigningCredentials signingCredentials = new SigningCredentials(this.CreateSecurityKey(), SecurityAlgorithms.HmacSha256);
    JwtSecurityToken jwt = new JwtSecurityToken(
      issuer: accessTokenGeneratorOptions.Issuer,
      audience: accessTokenGeneratorOptions.Audience,
      claims: this.GetUserClaims(user),
      expires: DateTime.Now.ToUniversalTime().AddMinutes(accessTokenGeneratorOptions.Expires),
      signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler().WriteToken(jwt);
  }

  public SecurityKey CreateSecurityKey()
  {
    return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.accessTokenGeneratorOptions.ServerKey!));
  }

  private IEnumerable<Claim> GetUserClaims(User user)
  {
    List<Claim> claims = 
    [
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(ClaimTypes.Name, user.Name!),
      ..user.UserRoles!.Select(ur => new Claim("Role", ur.Role!.Code!)).ToList(),
      ..user.UserRoles!.SelectMany(ur => ur.Role!.RolePermissions!.Select(rp => new Claim(Constants.ClaimTypes.Permission, rp.Permission!.Code!))).ToList(),
    ];

    return claims;
  }
}
