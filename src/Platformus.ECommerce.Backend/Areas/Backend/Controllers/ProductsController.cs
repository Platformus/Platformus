// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Backend.ViewModels.Products;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageProductsPermission)]
  public class ProductsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Product, ProductFilter> Repository
    {
      get => this.Storage.GetRepository<int, Product, ProductFilter>();
    }

    public ProductsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]ProductFilter filter = null, string orderBy = null, int skip = 0, int take = 10)
    {
      if (string.IsNullOrEmpty(orderBy))
        orderBy = "+" + this.HttpContext.CreateLocalizedOrderBy("Name");

      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(
          filter, orderBy, skip, take,
          new Inclusion<Product>(p => p.Category.Name.Localizations),
          new Inclusion<Product>(p => p.Name.Localizations)
        ),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await new CreateOrEditViewModelFactory().CreateAsync(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<Product>(p => p.Category.Name.Localizations),
          new Inclusion<Product>(p => p.Name.Localizations),
          new Inclusion<Product>(p => p.Description.Localizations),
          new Inclusion<Product>(p => p.Title.Localizations),
          new Inclusion<Product>(p => p.MetaDescription.Localizations),
          new Inclusion<Product>(p => p.MetaKeywords.Localizations),
          new Inclusion<Product>(p => p.Photos)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Product product = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Product() : await this.Repository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<Product>(p => p.Photos)),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(product);

        if (createOrEdit.Id == null)
          this.Repository.Create(product);

        else this.Repository.Edit(product);

        await this.Storage.SaveAsync();
        await this.CreateOrEditPhotosAsync(product, createOrEdit);

        if (createOrEdit.Id == null)
          Event<IProductCreatedEventHandler, HttpContext, Product>.Broadcast(this.HttpContext, product);

        else Event<IProductEditedEventHandler, HttpContext, Product>.Broadcast(this.HttpContext, product);

        return this.Redirect(this.Request.CombineUrl("/backend/products"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
      Product product = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(product.Id);
      await this.Storage.SaveAsync();
      Event<IProductCreatedEventHandler, HttpContext, Product>.Broadcast(this.HttpContext, product);
      return this.RedirectToAction("Index");
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new ProductFilter() { Code = code }) == 0;
    }

    private async Task CreateOrEditPhotosAsync(Product product, CreateOrEditViewModel createOrEdit)
    {
      await this.DeletePhotosAsync(product);
      await this.CreatePhotosAsync(product, createOrEdit);
    }

    private async Task DeletePhotosAsync(Product product)
    {
      if (product.Photos != null)
        foreach (Photo photo in product.Photos)
          this.Storage.GetRepository<int, Photo, PhotoFilter>().Delete(photo.Id);

      await this.Storage.SaveAsync();
    }

    private async Task CreatePhotosAsync(Product product, CreateOrEditViewModel createOrEdit)
    {
      if (!string.IsNullOrEmpty(createOrEdit.Photo1Filename))
      {
        Photo photo = new Photo();

        photo.ProductId = product.Id;
        photo.Filename = createOrEdit.Photo1Filename;
        photo.IsCover = true;
        photo.Position = 1;
        this.Storage.GetRepository<int, Photo, PhotoFilter>().Create(photo);
      }

      if (!string.IsNullOrEmpty(createOrEdit.Photo2Filename))
      {
        Photo photo = new Photo();

        photo.ProductId = product.Id;
        photo.Filename = createOrEdit.Photo2Filename;
        photo.Position = 2;
        this.Storage.GetRepository<int, Photo, PhotoFilter>().Create(photo);
      }

      if (!string.IsNullOrEmpty(createOrEdit.Photo3Filename))
      {
        Photo photo = new Photo();

        photo.ProductId = product.Id;
        photo.Filename = createOrEdit.Photo3Filename;
        photo.Position = 3;
        this.Storage.GetRepository<int, Photo, PhotoFilter>().Create(photo);
      }

      if (!string.IsNullOrEmpty(createOrEdit.Photo4Filename))
      {
        Photo photo = new Photo();

        photo.ProductId = product.Id;
        photo.Filename = createOrEdit.Photo4Filename;
        photo.Position = 4;
        this.Storage.GetRepository<int, Photo, PhotoFilter>().Create(photo);
      }

      if (!string.IsNullOrEmpty(createOrEdit.Photo5Filename))
      {
        Photo photo = new Photo();

        photo.ProductId = product.Id;
        photo.Filename = createOrEdit.Photo5Filename;
        photo.Position = 5;
        this.Storage.GetRepository<int, Photo, PhotoFilter>().Create(photo);
      }

      await this.Storage.SaveAsync();
    }
  }
}