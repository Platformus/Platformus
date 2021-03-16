// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.Metadata;

namespace Platformus.ECommerce.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.platformus.ecommerce.min.css", 3000),
      };
    }

    public override IEnumerable<Script> GetScripts(HttpContext httpContext)
    {
      return new Script[]
      {
        new Script("/wwwroot.areas.backend.js.platformus.ecommerce.min.js", 3000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
    {
      IStringLocalizer<Metadata> localizer = httpContext.RequestServices.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Ecommerce"],
          1500,
          new MenuItem[]
          {
            new MenuItem("/backend/categories", localizer["Categories"], 1000, new string[] { Permissions.ManageCategories }),
            new MenuItem("/backend/products", localizer["Products"], 2000, new string[] { Permissions.ManageProducts }),
            new MenuItem("/backend/carts", localizer["Carts"], 3000, new string[] { Permissions.ManageCarts }),
            new MenuItem("/backend/orders", localizer["Orders"], 4000, new string[] { Permissions.ManageOrders }),
          }
        ),
        new MenuGroup(
          localizer["Administration"],
          3000,
          new MenuItem[]
          {
            new MenuItem("/backend/orderstates", localizer["Order states"], 3000, new string[] { Permissions.ManageOrderStates }),
            new MenuItem("/backend/deliverymethods", localizer["Delivery methods"], 4000, new string[] { Permissions.ManageDeliveryMethods }),
            new MenuItem("/backend/paymentmethods", localizer["Payment methods"], 5000, new string[] { Permissions.ManagePaymentMethods }),
          }
        )
      };
    }
  }
}