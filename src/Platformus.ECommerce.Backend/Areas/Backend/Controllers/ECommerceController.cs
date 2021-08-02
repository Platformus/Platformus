// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.ECommerce;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  public class ECommerceController : Core.Backend.Controllers.ControllerBase
  {
    public ECommerceController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> CategorySelectorFormAsync(int? categoryId)
    {
      return this.PartialView("_CategorySelectorForm", CategorySelectorFormViewModelFactory.Create(
        this.HttpContext,
        await this.Storage.GetRepository<int, Category, CategoryFilter>().GetAllAsync(
          inclusions: new Inclusion<Category>(c => c.Name.Localizations)
        ),
        categoryId
      ));
    }

    public async Task<IActionResult> CategoryAsync(int id)
    {
      Category category = await this.Storage.GetRepository<int, Category, CategoryFilter>().GetByIdAsync(
        id,
        new Inclusion<Category>(c => c.Name.Localizations)
      );

      return this.Json(CategoryViewModelFactory.Create(category));
    }

    public async Task<IActionResult> ProductSelectorFormAsync(int? productId)
    {
      return this.PartialView("_ProductSelectorForm", ProductSelectorFormViewModelFactory.Create(
        this.HttpContext,
        await this.Storage.GetRepository<int, Product, ProductFilter>().GetAllAsync(
          inclusions: new Inclusion<Product>[]
          {
            new Inclusion<Product>(c => c.Category.Name.Localizations),
            new Inclusion<Product>(c => c.Name.Localizations)
          }
        ),
        productId
      ));
    }

    public async Task<IActionResult> ProductAsync(int id)
    {
      Product product = await this.Storage.GetRepository<int, Product, ProductFilter>().GetByIdAsync(
        id,
        new Inclusion<Product>(p => p.Category.Name.Localizations),
        new Inclusion<Product>(p => p.Name.Localizations)
      );

      return this.Json(ProductViewModelFactory.Create(product));
    }
  }
}