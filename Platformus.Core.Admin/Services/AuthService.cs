// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components.Authorization;
using Platformus.Core.Admin.ApiServices;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Services;

public class AuthService : IAuthService
{
  private readonly AuthenticationStateProvider authenticationStateProvider;
  private readonly ICookiesService cookiesService;
  private readonly AccessTokenApiClient accessTokenApiClient;

  public AuthService(AuthenticationStateProvider authenticationStateProvider, ICookiesService cookiesService, AccessTokenApiClient accessTokenApiClient)
  {
    this.authenticationStateProvider = authenticationStateProvider;
    this.cookiesService = cookiesService;
    this.accessTokenApiClient = accessTokenApiClient;
  }

  public async Task<bool> IsAuthenticatedAsync()
  {
    return !string.IsNullOrEmpty(await cookiesService.GetCookieAsync("accessToken"));
  }

  public async Task<bool> SignInAsync(string email, string password)
  {
    Api.Dto.AccessToken.Get.AccessToken? accessToken = await this.accessTokenApiClient.PostAsync(
      new Api.Dto.AccessToken.Post.AccessToken { Identifier = email, Secret = password }
    );

    if (accessToken != null)
    {
      await this.cookiesService.SetCookieAsync("accessToken", accessToken.Id!, 365);
      await this.cookiesService.SetCookieAsync("refreshToken", accessToken.RefreshToken!, 365);
      await this.authenticationStateProvider.GetAuthenticationStateAsync();
      return true;
    }

    return false;
  }

  public async Task SignOutAsync()
  {
    await this.cookiesService.DeleteCookieAsync("accessToken");
    await this.cookiesService.DeleteCookieAsync("refreshToken");
    await this.authenticationStateProvider.GetAuthenticationStateAsync();
  }
}
