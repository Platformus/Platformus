// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Security.Actions
{
  public class AddAuthorizationAction : IConfigureServicesAction
  {
    public int Priority => 3010;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.Configure<MvcOptions>(options =>
        {
          foreach (IGlobalAuthorizationPolicyProvider globalAuthorizationPolicyProvider in ExtCore.Infrastructure.ExtensionManager.GetInstances<IGlobalAuthorizationPolicyProvider>())
            options.Filters.Add(new AuthorizeFilter(globalAuthorizationPolicyProvider.GetGlobalAuthorizationPolicy()));
        }
      );

      services.AddAuthorization(options =>
        {
          foreach (IAuthorizationPolicyProvider authorizationPolicyProvider in ExtCore.Infrastructure.ExtensionManager.GetInstances<IAuthorizationPolicyProvider>())
            options.AddPolicy(authorizationPolicyProvider.Name, authorizationPolicyProvider.GetAuthorizationPolicy());
        }
      );
    }
  }
}