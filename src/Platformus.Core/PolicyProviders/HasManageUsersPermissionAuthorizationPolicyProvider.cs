// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace Platformus.Core
{
  public class HasManageUsersPermissionAuthorizationPolicyProvider : IAuthorizationPolicyProvider
  {
    public string Name => Policies.HasManageUsersPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.ManageUsers) || context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.DoAnything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}