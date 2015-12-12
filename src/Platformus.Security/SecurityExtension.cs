// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Security
{
  public class SecurityExtension : IExtension
  {
    private IConfigurationRoot configurationRoot;

    public string Name
    {
      get
      {
        return "Security Extension";
      }
    }

    public IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return new BackendMenuGroup[]
        {
          new BackendMenuGroup(
            "Audience",
            3000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/permissions", "Permissions", 1000),
              new BackendMenuItem("/backend/roles", "Roles", 2000),
              new BackendMenuItem("/backend/users", "Users", 3000)
            }
          )
        };
      }
    }

    public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
    {
      this.configurationRoot = configurationRoot;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<MvcOptions>(options =>
        {
          options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
        }
      );
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
      applicationBuilder.UseCookieAuthentication(options => {
        options.AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.AutomaticAuthenticate = true;
        options.AutomaticChallenge = true;
        options.CookieName = "PLATFORMUS";
        options.ExpireTimeSpan = new System.TimeSpan(1, 0, 0);
        options.LoginPath = new PathString("/backend/account/signin");
      });
    }

    public void RegisterRoutes(IRouteBuilder routeBuilder)
    {
    }
  }
}