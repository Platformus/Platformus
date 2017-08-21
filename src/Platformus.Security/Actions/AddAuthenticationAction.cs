// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Platformus.Security.Actions
{
  public class AddAuthenticationAction : IConfigureServicesAction
  {
    public int Priority => 2000;

    public void Execute(IServiceCollection serviceCollection, IServiceProvider serviceProvider)
    {
      SecurityOptions securityOptions = serviceProvider.GetService<IOptions<SecurityOptions>>()?.Value;

      if (securityOptions.EnableAuthentication)
        serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
            {
              options.AccessDeniedPath = securityOptions.AccessDeniedPath;
              options.LoginPath = securityOptions.LoginPath;
              options.LogoutPath = securityOptions.LogoutPath;
              options.ReturnUrlParameter = securityOptions.ReturnUrlParameter;
              options.ExpireTimeSpan = TimeSpan.FromDays(7);
            }
          );

      else serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
          {
            options.AccessDeniedPath = "/backend/account/accessdenied";
            options.LoginPath = "/backend/account/signin";
            options.LogoutPath = "/backend/account/signout";
            options.ReturnUrlParameter = "next";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
          }
        );
    }
  }
}