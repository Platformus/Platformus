// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, string sorting, int offset, int limit, int total, IEnumerable<Product> products)
    {
      return new IndexViewModel()
      {
        CategoryOptions = await GetCategoryOptionsAsync(httpContext),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        Products = products.Select(ProductViewModelFactory.Create)
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