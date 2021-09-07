// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace Platformus.Core.PolicyProviders
{
  public class HasManageRolesPermissionAuthorizationPolicyProvider : IAuthorizationPolicyProvider
  {
    public string Name => Policies.HasManageRolesPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.ManageRoles) || context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.DoAnything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}