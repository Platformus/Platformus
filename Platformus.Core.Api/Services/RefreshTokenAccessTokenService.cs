// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain;
using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Filters.Abstractions;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Core.Filters;

namespace Platformus.Core.Api.Services;

public class RefreshTokenAccessTokenService : AccessTokenServiceBase
{
  public RefreshTokenAccessTokenService(
    IService<string, RefreshToken, RefreshTokenFilter> refreshTokenService,
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator
    ) : base(refreshTokenService, accessTokenGenerator, refreshTokenGenerator)
  {
  }

  public override async Task<Dto.AccessToken.Get.AccessToken?> CreateAsync(Dto.AccessToken.Post.AccessToken accessToken)
  {
    if (string.IsNullOrEmpty(accessToken.RefreshToken)) return null;

    IEnumerable<RefreshToken>? refreshTokens = await this.refreshTokenService.GetAllAsync(
    new RefreshTokenFilter(
        id: new StringFilter(equals: accessToken.RefreshToken)
      ),
      inclusions: new InclusionBuilder<RefreshToken>()
        .Add(c => c.User)
        .ThenAdd(u => u.UserRoles)
        .ThenAdd(ur => ur.Role)
        .ThenAdd(r => r.RolePermissions)
        .ThenAdd(rp => rp.Permission)
        .Build()
    );

    if (refreshTokens?.Any() != true) return null;

    RefreshToken refreshToken = refreshTokens.First();

    if (refreshToken == null)
      return null;

    if (refreshToken.Created < DateTime.Now.AddMonths(-1).ToUniversalTime() || refreshToken.Used != null)
      return null;

    refreshToken.Used = DateTime.Now.ToUniversalTime();
    await this.refreshTokenService.EditAsync(refreshToken);

    User user = refreshToken.User!;

    refreshToken = await this.CreateRefreshTokenAsync(user);
    return new Dto.AccessToken.Get.AccessToken
    {
      Id = this.accessTokenGenerator.Generate(user),
      RefreshToken = refreshToken.Id
    };
  }
}
