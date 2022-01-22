// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
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
        new StyleSheet("/wwwroot.areas.backend.css.platformus.ecommerce.min.css", 1000)
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
      IStringLocalizer<Metadata> localizer = httpContext.GetStringLocalizer<Metadata>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Ecommerce"],
          2000,
          new MenuItem[]
          {
            new MenuItem("icon--categories", "/backend/categories", localizer["Categories"], 1000, Permissions.ManageCategories),
            new MenuItem("icon--products", "/backend/products", localizer["Products"], 2000, Permissions.ManageProducts),
            new MenuItem("icon--carts", "/backend/carts", localizer["Carts"], 3000, Permissions.ManageCarts),
            new MenuItem("icon--orders", "/backend/orders", localizer["Orders"], 4000, Permissions.ManageOrders),
          }
        ),
        new MenuGroup(
          localizer["Administration"],
          4000,
          new MenuItem[]
          {
            new MenuItem("icon--order-states", "/backend/orderstates", localizer["Order states"], 3000, Permissions.ManageOrderStates),
            new MenuItem("icon--delivery-methods", "/backend/deliverymethods", localizer["Delivery methods"], 4000, Permissions.ManageDeliveryMethods),
            new MenuItem("icon--payment-methods", "/backend/paymentmethods", localizer["Payment methods"], 5000, Permissions.ManagePaymentMethods),
          }
        )
      };
    }
  }
}