// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ProductFilter filter, IEnumerable<Product> products, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext,
          new[] {
            FilterViewModelFactory.Create(httpContext, "Category.Id.Equals", localizer["Category"], await GetCategoryOptionsAsync(httpContext)),
            FilterViewModelFactory.Create(httpContext, "Name.Value.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Category"]),
            GridColumnViewModelFactory.Create(localizer["Name"], httpContext.CreateLocalizedOrderBy("Name")),
            GridColumnViewModelFactory.Create(localizer["Units"], httpContext.CreateLocalizedOrderBy("Units")),
            GridColumnViewModelFactory.Create(localizer["Price"], "Price"),
            GridColumnViewModelFactory.CreateEmpty()
          },
          products.Select(ProductViewModelFactory.Create),
          "_Product"
        )
      };
    }

    private static async Task<IEnumerable<Option>> GetCategoryOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["All categories"], string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, Category, CategoryFilter>().GetAllAsync(inclusions: new Inclusion<Category>(c => c.Name.Localizations))).Select(
          c => new Option(c.Name.GetLocalizationValue(), c.Id.ToString())
        )
      );

      return options;
    }
  }
}