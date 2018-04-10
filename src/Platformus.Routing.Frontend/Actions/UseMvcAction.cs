// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Configurations;
using Platformus.Globalization;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Routing.Frontend.Actions
{
  public class UseMvcAction : IUseMvcAction
  {
    public int Priority => 10000;

    public void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider)
    {
      string template = string.Empty;

      if (this.MustSpecifyCultureInUrl(serviceProvider))
        template = "{culture=" + this.GetDefaultCultureCode(serviceProvider) + "}/{*url}";

      else template = "{*url}";

      routeBuilder.MapRoute(name: "Default", template: template, defaults: new { controller = "Default", action = "Index" });
    }

    private bool MustSpecifyCultureInUrl(IServiceProvider serviceProvider)
    {
      return serviceProvider.GetService<IConfigurationManager>()["Globalization", "SpecifyCultureInUrl"] != "no";
    }

    private string GetDefaultCultureCode(IServiceProvider serviceProvider)
    {
      Culture frontendDefaultCulture = serviceProvider.GetService<ICultureManager>().GetFrontendDefaultCulture();

      if (frontendDefaultCulture == null)
        return DefaultCulture.Code;

      return frontendDefaultCulture.Code;
    }
  }
}