// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Data.Abstractions;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Configurations;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Actions
{
  public class UseMvcAction : IUseMvcAction
  {
    public int Priority => 10000;

    public void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider)
    {
      IStorage storage = serviceProvider.GetService<IStorage>();

      if (storage == null)
      {
        routeBuilder.MapRoute(name: "Default", template: "", defaults: new { controller = "Installation", action = "Index" });
        return;
      }

      string template = string.Empty;

      if (this.MustSpecifyCultureInUrl(storage))
        template = "{culture=" + this.GetDefaultCultureCode(storage) + "}/{*url}";

      else template = "{*url}";

      routeBuilder.MapRoute(name: "Default", template: template, defaults: new { controller = "Default", action = "Index" });
    }

    private bool MustSpecifyCultureInUrl(IStorage storage)
    {
      return new ConfigurationManager(storage)["Globalization", "SpecifyCultureInUrl"] != "no";
    }

    private string GetDefaultCultureCode(IStorage storage)
    {
      Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

      if (defaultCulture == null)
        return DefaultCulture.Code;

      return defaultCulture.Code;
    }
  }
}