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
using Microsoft.AspNetCore.Routing;
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

    public IDictionary<int, Action<IRouteBuilder>> RouteRegistrarsByPriorities
    {
      get
      {
        return null;
      }
    }

    public IFrontendMetadata FrontendMetadata
    {
      get
      {
        return null;
      }
    }

    public IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
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
  }
}