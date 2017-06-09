// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            applicationBuilder.UseWhen(
              context => context.Request.Path.StartsWithSegments(new PathString("/backend")),
              backendApplicationBuilder =>
              {
                backendApplicationBuilder.UseCookieAuthentication(
                  new CookieAuthenticationOptions()
                  {
                    AccessDeniedPath = new PathString("/backend/account/accessdenied"),
                    AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    CookieName = "PLATFORMUS",
                    ExpireTimeSpan = TimeSpan.FromDays(7),
                    LoginPath = new PathString("/backend/account/signin"),
                    ReturnUrlParameter = "returnurl"
                  }
                );
              }
            );
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