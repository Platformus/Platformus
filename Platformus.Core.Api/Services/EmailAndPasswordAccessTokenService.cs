// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain;
using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Filters.Abstractions;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Core.Filters;

namespace Platformus.Core.Api.Services;

public class EmailAndPasswordAccessTokenService : AccessTokenServiceBase
{
  private readonly IService<int, Credential, CredentialFilter> credentialService;
  private readonly IPasswordHasher passwordHasher;

  public EmailAndPasswordAccessTokenService(
    IService<int, Credential, CredentialFilter> credentialService,
    IService<string, RefreshToken, RefreshTokenFilter> refreshTokenService,
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator,
    IPasswordHasher passwordHasher
    ) : base(refreshTokenService, accessTokenGenerator, refreshTokenGenerator)
  {
    this.credentialService = credentialService;
    this.passwordHasher = passwordHasher;
  }

  public override async Task<Dto.AccessToken.Get.AccessToken?> CreateAsync(Dto.AccessToken.Post.AccessToken accessToken)
  {
    if (string.IsNullOrEmpty(accessToken.Identifier) || string.IsNullOrEmpty(accessToken.Secret)) return null;

    IEnumerable<Credential>? credentials = await this.credentialService.GetAllAsync(
      new CredentialFilter(
        credentialType: new CredentialTypeFilter(id: new StringFilter(equals: "emailAndPassword")),
        identifier: new StringFilter(equals: accessToken.Identifier)
      ),
      inclusions: new InclusionBuilder<Credential>()
        .Add(c => c.User)
        .ThenAdd(u => u.UserRoles)
        .ThenAdd(ur => ur.Role)
        .ThenAdd(r => r.RolePermissions)
        .ThenAdd(rp => rp.Permission)
        .Build()
    );

    if (credentials?.Any() != true) return null;

    Credential credential = credentials.First();

    if (credential.Secret != this.passwordHasher.ComputeHash(accessToken.Secret, Convert.FromBase64String(credential.Extra ?? string.Empty))) return null;

    User user = credential.User!;
    RefreshToken _refreshToken = await this.CreateRefreshTokenAsync(user);

    return new Dto.AccessToken.Get.AccessToken
    {
      Id = this.accessTokenGenerator.Generate(user),
      RefreshToken = _refreshToken.Id
    };
  }
}
