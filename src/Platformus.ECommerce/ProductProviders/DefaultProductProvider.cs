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
  public class DefaultProductProvider : IProductProvider
  {
    public IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };
    public string Description => "Returns products of the current category.";

    public async Task<IEnumerable<Product>> GetProductsAsync(HttpContext httpContext, Category category)
    {
      return await httpContext.GetStorage().GetRepository<int, Product, ProductFilter>().GetAllAsync(
        new ProductFilter(category: new CategoryFilter(id: new IntegerFilter(equals: category.Id))),
        inclusions: new Inclusion<Product>[]
        {
          new Inclusion<Product>(p => p.Name.Localizations),
          new Inclusion<Product>(p => p.Units.Localizations),
          new Inclusion<Product>(p => p.Photos)
        }
      );
    }
  }
}