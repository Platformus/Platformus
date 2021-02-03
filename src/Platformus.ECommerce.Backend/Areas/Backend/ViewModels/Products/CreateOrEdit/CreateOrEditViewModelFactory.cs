// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Product product)
    {
      if (product == null)
        return new CreateOrEditViewModel()
        {
          CategoryOptions = await this.GetCategoryOptionsAsync(httpContext),
          Url = "/",
          NameLocalizations = this.GetLocalizations(httpContext),
          DescriptionLocalizations = this.GetLocalizations(httpContext),
          TitleLocalizations = this.GetLocalizations(httpContext),
          MetaDescriptionLocalizations = this.GetLocalizations(httpContext),
          MetaKeywordsLocalizations = this.GetLocalizations(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = product.Id,
        CategoryId = product.CategoryId,
        CategoryOptions = await this.GetCategoryOptionsAsync(httpContext),
        Url = product.Url,
        Code = product.Code,
        NameLocalizations = this.GetLocalizations(httpContext, product.Name),
        DescriptionLocalizations = this.GetLocalizations(httpContext, product.Description),
        Price = product.Price,
        Photo1Filename = product.Photos.FirstOrDefault(p => p.Position == 1)?.Filename,
        Photo2Filename = product.Photos.FirstOrDefault(p => p.Position == 2)?.Filename,
        Photo3Filename = product.Photos.FirstOrDefault(p => p.Position == 3)?.Filename,
        Photo4Filename = product.Photos.FirstOrDefault(p => p.Position == 4)?.Filename,
        Photo5Filename = product.Photos.FirstOrDefault(p => p.Position == 5)?.Filename,
        TitleLocalizations = this.GetLocalizations(httpContext, product.Title),
        MetaDescriptionLocalizations = this.GetLocalizations(httpContext, product.MetaDescription),
        MetaKeywordsLocalizations = this.GetLocalizations(httpContext, product.MetaKeywords)
      };
    }

    private async Task<IEnumerable<Option>> GetCategoryOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, Category, CategoryFilter>().GetAllAsync(inclusions: new Inclusion<Category>(c => c.Name.Localizations))).Select(
        c => new Option(c.Name.GetLocalizationValue(), c.Id.ToString())
      );
    }
  }
}