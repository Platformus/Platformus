// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Platformus.Core;

namespace Platformus.Website.PolicyProviders;

public class HasManageClassesPermissionAuthorizationPolicyProvider : Core.IAuthorizationPolicyProvider
{
  public string Name => Policies.HasManageClassesPermission;

  public AuthorizationPolicy GetAuthorizationPolicy()
  {
    AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

    authorizationPolicyBuilder.RequireAssertion(context =>
      {
        return context.User.HasClaim(PlatformusClaimTypes.Permission, Permissions.ManageClasses) || context.User.HasClaim(PlatformusClaimTypes.Permission, Core.Permissions.DoAnything);
      }
    );

    return authorizationPolicyBuilder.Build();
  }
}