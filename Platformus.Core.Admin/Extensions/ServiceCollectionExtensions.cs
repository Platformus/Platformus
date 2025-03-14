// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Admin.ApiServices;
using Platformus.Core.Admin.Services;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Extensions;

public static class ServiceCollectionExtensions
{
  public static void AddPlatformusCoreAdmin(this IServiceCollection services)
  {
    services.AddBlazorBootstrap();
    services.AddAuthorizationCore();
    services.AddLocalization();
    services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<ICookiesService, CookiesService>();
    services.AddScoped<IUrl, Url>();
    services.AddScoped<IFilterMapper, FilterMapper>();
    services.AddScoped(typeof(IApiClient1K<,,>), typeof(ApiClient1K<,,>));
    services.AddScoped(typeof(IApiClient2K<,,,>), typeof(ApiClient2K<,,,>));
    services.AddScoped<AccessTokenApiClient>();
    services.AddScoped<ImageApiClient>();
    services.AddScoped<ICultureCache, CultureCache>();
    services.AddScoped<IMetadata, Metadata>();
  }

  public static void AddPlatformusAdmin(this IServiceCollection services)
  {
  }
}