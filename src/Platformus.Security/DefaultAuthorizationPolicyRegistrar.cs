// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Platformus.Security
{
  public class DefaultAuthorizationPolicyRegistrar : IAuthorizationPolicyRegistrar
  {
    public void RegisterAuthorizationPolicy(AuthorizationPolicyBuilder authorizationPolicyBuilder)
    {
      authorizationPolicyBuilder.RequireAssertion(
        handler =>
        {
          AuthorizationFilterContext authorizationFilterContext = handler.Resource as AuthorizationFilterContext;

          return !authorizationFilterContext.HttpContext.Request.Path.StartsWithSegments(new PathString("/backend")) || handler.User.IsInRole("Administrator");
        }
      );
    }
  }
}