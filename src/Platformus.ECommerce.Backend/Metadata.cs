// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.Metadata;

namespace Platformus.ECommerce.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.platformus.ecommerce.min.css", 5000),
      };
    }

    public override IEnumerable<Script> GetScripts(IRequestHandler requestHandler)
    {
      return new Script[]
      {
        new Script("/wwwroot.areas.backend.js.platformus.ecommerce.min.js", 5000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler)
    {
      IStringLocalizer<Metadata> localizer = requestHandler.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Ecommerce"],
          1500,
          new MenuItem[]
          {
            new MenuItem("/backend/catalogs", localizer["Catalogs"], 1000, new string[] { Permissions.BrowseCatalogs }),
            new MenuItem("/backend/categories", localizer["Categories"], 2000, new string[] { Permissions.BrowseCategories }),
            new MenuItem("/backend/features", localizer["Features"], 3000, new string[] { Permissions.BrowseFeatures }),
            new MenuItem("/backend/products", localizer["Products"], 4000, new string[] { Permissions.BrowseProducts }),
            new MenuItem("/backend/carts", localizer["Carts"], 5000, new string[] { Permissions.BrowseCarts }),
            new MenuItem("/backend/orderstates", localizer["Order states"], 6000, new string[] { Permissions.BrowseOrderStates }),
            new MenuItem("/backend/paymentmethods", localizer["Payment methods"], 7000, new string[] { Permissions.BrowsePaymentMethods }),
            new MenuItem("/backend/deliverymethods", localizer["Delivery methods"], 8000, new string[] { Permissions.BrowseDeliveryMethods }),
            new MenuItem("/backend/orders", localizer["Orders"], 9000, new string[] { Permissions.BrowseOrders })
          }
        )
      };
    }
  }
}