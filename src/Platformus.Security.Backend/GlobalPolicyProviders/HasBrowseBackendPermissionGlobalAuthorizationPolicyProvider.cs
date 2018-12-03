// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Platformus.Security.Backend
{
  public class HasBrowseBackendPermissionGlobalAuthorizationPolicyProvider : IGlobalAuthorizationPolicyProvider
  {
    public AuthorizationPolicy GetGlobalAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          AuthorizationFilterContext authorizationFilterContext = context.Resource as AuthorizationFilterContext;

          return !authorizationFilterContext.HttpContext.Request.Path.StartsWithSegments(new PathString("/backend")) || context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.BrowseBackend);
        }
      );

      authorizationPolicyBuilder.AddAuthenticationSchemes(BackendCookieAuthenticationDefaults.AuthenticationScheme);
      return authorizationPolicyBuilder.Build();
    }
  }
}