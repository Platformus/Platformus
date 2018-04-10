// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.ProductProviders
{
  public class CategoryProductProvider : ProductProviderBase
  {
    public override IEnumerable<ProductProviderParameterGroup> ParameterGroups =>
      new ProductProviderParameterGroup[]
      {
        new ProductProviderParameterGroup(
          "General",
          new ProductProviderParameter("CategoryId", "Category", "category", null, true)
        )
      };

    public override string Description => "Returns products of the current category.";

    protected override IEnumerable<Product> GetProducts()
    {
      int categoryId = this.GetIntParameterValue("CategoryId");

      return this.requestHandler.Storage.GetRepository<IProductRepository>().FilteredByCategoryIdRange(categoryId, "code", "asc", 0, 100, null).ToList();
    }
  }
}