// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.ECommerce.Backend.ViewModels.Products;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers;

[Authorize(Policy = Policies.HasManageProductsPermission)]
public class ProductsController : Core.Backend.Controllers.ControllerBase
{
  private IStringLocalizer localizer;

  private IRepository<int, Product, ProductFilter> ProductRepository
  {
    get => this.Storage.GetRepository<int, Product, ProductFilter>();
  }

  private IRepository<int, Photo, PhotoFilter> PhotoRepository
  {
    get => this.Storage.GetRepository<int, Photo, PhotoFilter>();
  }

  public ProductsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
    : base(storage)
  {
    this.localizer = localizer;
  }

  public async Task<IActionResult> IndexAsync([FromQuery] ProductFilter filter = null, string sorting = null, int offset = 0, int limit = 10)
  {
    if (string.IsNullOrEmpty(sorting))
      sorting = "+" + this.HttpContext.CreateLocalizedSorting("Name");

    return this.View(await IndexViewModelFactory.CreateAsync(
      this.HttpContext, sorting, offset, limit, await this.ProductRepository.CountAsync(filter),
      await this.ProductRepository.GetAllAsync(
        filter, sorting, offset, limit,
        new Inclusion<Product>(p => p.Category.Name.Localizations),
        new Inclusion<Product>(p => p.Name.Localizations),
        new Inclusion<Product>(p => p.Units.Localizations)
      )
    ));
  }

  [HttpGet]
  [ImportModelStateFromTempData]
  public async Task<IActionResult> CreateOrEditAsync(int? id)
  {
    return this.View(await CreateOrEditViewModelFactory.CreateAsync(
      this.HttpContext, id == null ? null : await this.ProductRepository.GetByIdAsync(
        (int)id,
        new Inclusion<Product>(p => p.Category.Name.Localizations),
        new Inclusion<Product>(p => p.Name.Localizations),
        new Inclusion<Product>(p => p.Description.Localizations),
        new Inclusion<Product>(p => p.Units.Localizations),
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
    if (!await this.IsUrlUniqueAsync(createOrEdit))
      this.ModelState.AddModelError("url", this.localizer["Value is already in use"]);

    if (!await this.IsCodeUniqueAsync(createOrEdit))
      this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

    if (this.ModelState.IsValid)
    {
      Product product = CreateOrEditViewModelMapper.Map(
        createOrEdit.Id == null ?
          new Product() :
          await this.ProductRepository.GetByIdAsync(
            (int)createOrEdit.Id,
            new Inclusion<Product>(p => p.Name.Localizations),
            new Inclusion<Product>(p => p.Description.Localizations),
            new Inclusion<Product>(p => p.Units.Localizations),
            new Inclusion<Product>(p => p.Title.Localizations),
            new Inclusion<Product>(p => p.MetaDescription.Localizations),
            new Inclusion<Product>(p => p.MetaKeywords.Localizations),
            new Inclusion<Product>(p => p.Photos)
          ),
        createOrEdit
      );

      if (createOrEdit.Id == null)
        this.ProductRepository.Create(product);

      else this.ProductRepository.Edit(product);

      await this.MergeEntityLocalizationsAsync(product);
      this.MergePhotos(product, createOrEdit);
      await this.Storage.SaveAsync();

      if (createOrEdit.Id == null)
        Event<IProductCreatedEventHandler, HttpContext, Product>.Broadcast(this.HttpContext, product);

      else Event<IProductEditedEventHandler, HttpContext, Product>.Broadcast(this.HttpContext, product);

      return this.Redirect(this.Request.CombineUrl("/backend/products"));
    }

    return this.CreateRedirectToSelfResult();
  }

  public async Task<ActionResult> DeleteAsync(int id)
  {
    Product product = await this.ProductRepository.GetByIdAsync(id);

    this.ProductRepository.Delete(product.Id);
    await this.Storage.SaveAsync();
    Event<IProductCreatedEventHandler, HttpContext, Product>.Broadcast(this.HttpContext, product);
    return this.Redirect(this.Request.CombineUrl("/backend/products"));
  }

  private async Task<bool> IsUrlUniqueAsync(CreateOrEditViewModel createOrEdit)
  {
    Product product = (await this.ProductRepository.GetAllAsync(new ProductFilter(url: createOrEdit.Url))).FirstOrDefault();

    return product == null || product.Id == createOrEdit.Id;
  }

  private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
  {
    Product product = (await this.ProductRepository.GetAllAsync(new ProductFilter(code: createOrEdit.Code))).FirstOrDefault();

    return product == null || product.Id == createOrEdit.Id;
  }

  private void MergePhotos(Product product, CreateOrEditViewModel createOrEdit)
  {
    this.MergePhoto(product, createOrEdit.Photo1Url, 1);
    this.MergePhoto(product, createOrEdit.Photo2Url, 2);
    this.MergePhoto(product, createOrEdit.Photo3Url, 3);
    this.MergePhoto(product, createOrEdit.Photo4Url, 4);
    this.MergePhoto(product, createOrEdit.Photo5Url, 5);
  }

  private void MergePhoto(Product product, string url, int position)
  {
    Photo photo = product.Photos?.FirstOrDefault(p => p.Position == position);

    if (string.IsNullOrEmpty(url))
    {
      if (photo != null)
        this.PhotoRepository.Delete(photo.Id);
    }

    else
    {
      string filename = this.GetPhotoFilename(url);

      if (photo == null)
      {
        photo = new Photo();
        photo.Product = product;
        photo.Filename = filename;
        photo.Position = position;
        this.PhotoRepository.Create(photo);
      }

      else if (photo.Filename != filename)
      {
        photo.Filename = filename;
        this.PhotoRepository.Edit(photo);
      }
    }
  }

  private string GetPhotoFilename(string url)
  {
    return url.Substring(url.LastIndexOf("/") + 1);
  }
}