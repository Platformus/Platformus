// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
  private readonly HttpClient httpClient;
  private readonly ICookiesService cookiesService;

  public CustomAuthStateProvider(HttpClient httpClient, ICookiesService cookiesService)
  {
    this.httpClient = httpClient;
    this.cookiesService = cookiesService;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    string? accessToken = await this.cookiesService.GetCookieAsync("accessToken");

    ClaimsIdentity identity = new ClaimsIdentity();
    
    httpClient.DefaultRequestHeaders.Authorization = null;

    if (!string.IsNullOrEmpty(accessToken))
    {
      JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

      if (jwtSecurityTokenHandler.CanReadToken(accessToken))
        identity = new ClaimsIdentity(jwtSecurityTokenHandler.ReadJwtToken(accessToken).Claims, "jwt");

      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
    AuthenticationState authenticationState = new AuthenticationState(claimsPrincipal);

    NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    return authenticationState;
  }
}
