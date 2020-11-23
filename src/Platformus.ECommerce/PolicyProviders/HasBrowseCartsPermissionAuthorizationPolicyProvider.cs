// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Platformus.Core;

namespace Platformus.ECommerce
{
  public class HasBrowseCartsPermissionAuthorizationPolicyProvider : Platformus.Core.IAuthorizationPolicyProvider
  {
    public string Name => Policies.HasBrowseCartsPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.BrowseCarts) || context.User.HasClaim(PlatformusClaimTypes.Permission, Platformus.Core.Permissions.DoEverything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}