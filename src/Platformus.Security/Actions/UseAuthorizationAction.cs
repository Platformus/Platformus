// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Platformus.Security.Actions
{
  public class UseAuthorizationAction : IConfigureAction
  {
    public int Priority => 2000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
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

      SecurityOptions securityOptions = serviceProvider.GetService<IOptions<SecurityOptions>>()?.Value;

      if (securityOptions != null && securityOptions.EnableAuthentication)
      {
        applicationBuilder.UseWhen(
        context => !context.Request.Path.StartsWithSegments(new PathString("/backend")),
        frontendApplicationBuilder =>
        {
          frontendApplicationBuilder.UseCookieAuthentication(
            new CookieAuthenticationOptions()
            {
              AccessDeniedPath = securityOptions.AccessDeniedPath,
              AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
              AutomaticAuthenticate = true,
              AutomaticChallenge = true,
              CookieName = "PLATFORMUS",
              ExpireTimeSpan = TimeSpan.FromDays(7),
              LoginPath = securityOptions.LoginPath,
              LogoutPath = securityOptions.LogoutPath,
              ReturnUrlParameter = securityOptions.ReturnUrlParameter
            }
          );
        }
      );
      }
    }
  }
}