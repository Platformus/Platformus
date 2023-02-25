// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Products;

public static class CreateOrEditViewModelFactory
{
  public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Product product)
  {
    if (product == null)
      return new CreateOrEditViewModel()
      {
        CategoryOptions = await GetCategoryOptionsAsync(httpContext),
        Url = "/",
        NameLocalizations = httpContext.GetLocalizations(),
        DescriptionLocalizations = httpContext.GetLocalizations(),
        UnitsLocalizations = httpContext.GetLocalizations(),
        TitleLocalizations = httpContext.GetLocalizations(),
        MetaDescriptionLocalizations = httpContext.GetLocalizations(),
        MetaKeywordsLocalizations = httpContext.GetLocalizations()
      };

    return new CreateOrEditViewModel()
    {
      Id = product.Id,
      CategoryId = product.CategoryId,
      CategoryOptions = await GetCategoryOptionsAsync(httpContext),
      Url = product.Url,
      Code = product.Code,
      NameLocalizations = httpContext.GetLocalizations(product.Name),
      DescriptionLocalizations = httpContext.GetLocalizations(product.Description),
      UnitsLocalizations = httpContext.GetLocalizations(product.Units),
      Price = product.Price,
      Photo1Url = GetPhotoUrl(product.Photos.FirstOrDefault(p => p.Position == 1)?.Filename),
      Photo2Url = GetPhotoUrl(product.Photos.FirstOrDefault(p => p.Position == 2)?.Filename),
      Photo3Url = GetPhotoUrl(product.Photos.FirstOrDefault(p => p.Position == 3)?.Filename),
      Photo4Url = GetPhotoUrl(product.Photos.FirstOrDefault(p => p.Position == 4)?.Filename),
      Photo5Url = GetPhotoUrl(product.Photos.FirstOrDefault(p => p.Position == 5)?.Filename),
      TitleLocalizations = httpContext.GetLocalizations(product.Title),
      MetaDescriptionLocalizations = httpContext.GetLocalizations(product.MetaDescription),
      MetaKeywordsLocalizations = httpContext.GetLocalizations(product.MetaKeywords)
    };
  }

  private static async Task<IEnumerable<Option>> GetCategoryOptionsAsync(HttpContext httpContext)
  {
    return (await httpContext.GetStorage().GetRepository<int, Category, CategoryFilter>().GetAllAsync(inclusions: new Inclusion<Category>(c => c.Name.Localizations))).Select(
      c => new Option(c.Name.GetLocalizationValue(), c.Id.ToString())
    ).ToList();
  }

  private static string GetPhotoUrl(string filename)
  {
    if (string.IsNullOrEmpty(filename))
      return null;

    return "/images/products/" + filename;
  }
}