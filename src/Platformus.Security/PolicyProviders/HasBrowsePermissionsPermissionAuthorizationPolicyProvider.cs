// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace Platformus.Security
{
  public class HasBrowsePermissionsPermissionAuthorizationPolicyProvider : IAuthorizationPolicyProvider
  {
    public string Name => Policies.HasBrowsePermissionsPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.BrowsePermissions) || context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.DoEverything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}