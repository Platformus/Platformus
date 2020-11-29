// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Core.Parameters;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.ProductProviders
{
  public class CategoryProductProvider : IProductProvider
  {
    public IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("CategoryId", "Category", "categorySelector", null, true)
        )
      };

    public string Description => "Returns products of the current category.";

    public async Task<IEnumerable<Product>> GetProductsAsync(HttpContext httpContext, Catalog catalog)
    {
      int categoryId = new ParametersParser(catalog.Parameters).GetIntParameterValue("CategoryId");

      return await httpContext.GetStorage().GetRepository<int, Product, ProductFilter>().GetAllAsync(
        new ProductFilter() { Category = new CategoryFilter() { Id = new IntegerFilter(equals: categoryId) } },
        inclusions: new Inclusion<Product>[]
        {
          new Inclusion<Product>(p => p.Name.Localizations),
          new Inclusion<Product>(p => p.Photos)
        }
      );
    }
  }
}