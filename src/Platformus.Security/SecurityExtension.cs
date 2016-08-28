// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Security
{
  public class SecurityExtension : ExtensionBase, ISecurityExtension
  {
    public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IServiceCollection>>()
        {
          [2000] = services =>
          {
            AuthorizationPolicyBuilder builder = new AuthorizationPolicyBuilder();
            foreach (IExtension extension in ExtCore.Infrastructure.ExtensionManager.Extensions)
            {
              if(extension is ISecurityExtension)
              {
                builder = (extension as ISecurityExtension).ConfigurePolicy(builder);
              }
            }            
            services.Configure<MvcOptions>(options =>
              {
                options.Filters.Add(new AuthorizeFilter(builder.Build()));
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
            applicationBuilder.UseCookieAuthentication(
              new CookieAuthenticationOptions()
              {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                CookieName = "PLATFORMUS",
                ExpireTimeSpan = new System.TimeSpan(1, 0, 0),
                LoginPath = new PathString("/backend/account/signin")
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

    public AuthorizationPolicyBuilder ConfigurePolicy(AuthorizationPolicyBuilder builder)
    {
      return builder.RequireAssertion(handler =>
        {
          var context = handler.Resource as AuthorizationFilterContext;
          return !context.HttpContext.Request.Path.StartsWithSegments(new PathString("/backend")) || handler.User.IsInRole("Administrator");
        });            
    }
  }
}