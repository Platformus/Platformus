// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Platformus.Core.Api.Constants;

namespace Platformus.Core.Api.Extensions;
public static class AuthorizationOptionsExtensions
{
  public static void AddRPolicies<TDto>(this AuthorizationOptions options, string viewPermissionCode, string managePermissionCode)
  {
    options.AddPolicy($"{typeof(TDto).Name}.{nameof(HttpMethod.Get)}", p => p.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Permission, [viewPermissionCode, managePermissionCode]));
  }

  public static void AddCrudPolicies<TDto>(this AuthorizationOptions options, string viewPermissionCode, string managePermissionCode)
  {
    options.AddPolicy($"{typeof(TDto).Name}.{nameof(HttpMethod.Get)}", p => p.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Permission, [viewPermissionCode, managePermissionCode]));
    options.AddPolicy($"{typeof(TDto).Name}.{nameof(HttpMethod.Post)}", p => p.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Permission, managePermissionCode));
    options.AddPolicy($"{typeof(TDto).Name}.{nameof(HttpMethod.Put)}", p => p.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Permission, managePermissionCode));
    options.AddPolicy($"{typeof(TDto).Name}.{nameof(HttpMethod.Patch)}", p => p.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Permission, managePermissionCode));
    options.AddPolicy($"{typeof(TDto).Name}.{nameof(HttpMethod.Delete)}", p => p.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Permission, managePermissionCode));
  }
}