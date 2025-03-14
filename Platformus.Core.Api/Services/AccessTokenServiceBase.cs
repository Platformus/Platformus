// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Services.Abstractions;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Core.Filters;

namespace Platformus.Core.Api.Services;

public abstract class AccessTokenServiceBase : IAccessTokenService
{
  protected readonly IService<string, RefreshToken, RefreshTokenFilter> refreshTokenService;
  protected readonly IAccessTokenGenerator accessTokenGenerator;
  protected readonly IRefreshTokenGenerator refreshTokenGenerator;

  public AccessTokenServiceBase(
    IService<string, RefreshToken, RefreshTokenFilter> refreshTokenService,
    IAccessTokenGenerator accessTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator
    )
  {
    this.refreshTokenService = refreshTokenService;
    this.accessTokenGenerator = accessTokenGenerator;
    this.refreshTokenGenerator = refreshTokenGenerator;
  }

  public abstract Task<Dto.AccessToken.Get.AccessToken?> CreateAsync(Dto.AccessToken.Post.AccessToken accessToken);
  
  protected async Task<RefreshToken> CreateRefreshTokenAsync(User _user)
  {
    RefreshToken _refreshToken = new RefreshToken();

    _refreshToken.Id = this.refreshTokenGenerator.Generate();
    _refreshToken.User = _user;
    _refreshToken.Created = DateTime.Now.ToUniversalTime();
    await this.refreshTokenService.CreateAsync(_refreshToken);
    return _refreshToken;
  }
}
