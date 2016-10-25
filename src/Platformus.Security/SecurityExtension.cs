// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Security
{
  public class SecurityExtension : Platformus.Infrastructure.ExtensionBase
  {
    public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IServiceCollection>>()
        {
          [2000] = services =>
          {
            AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

            foreach (IAuthorizationPolicyRegistrar authorizationPolicyRegistrar in ExtCore.Infrastructure.ExtensionManager.GetInstances<IAuthorizationPolicyRegistrar>())
              authorizationPolicyRegistrar.RegisterAuthorizationPolicy(authorizationPolicyBuilder);

            services.Configure<MvcOptions>(options =>
              {
                options.Filters.Add(new AuthorizeFilter(authorizationPolicyBuilder.Build()));
              }
            );
          }
        };
      }
    }

    public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IApplicationBuilder>>()
        {
          [2000] = applicationBuilder =>
          {              
             applicationBuilder.UsePlatformusCookie(x => x.Request.Path.StartsWithSegments(new PathString("/backend")), "/backend/account/signin");
             applicationBuilder.UsePlatformusCookie(x => !x.Request.Path.StartsWithSegments(new PathString("/backend")), "/frontend/account/signin");
          }
        };
      }
    }

    public override IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
      }
    }
  }
}