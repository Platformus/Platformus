// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Products;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseProductsPermission)]
  public class ProductsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public ProductsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "code", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take, filter));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Product product = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(product);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IProductRepository>().Create(product);

        else this.Storage.GetRepository<IProductRepository>().Edit(product);

        this.Storage.Save();
        this.CreateOrEditPhotos(product, createOrEdit.RemovedPhotoIds);

        if (createOrEdit.Id == null)
          Event<IProductCreatedEventHandler, IRequestHandler, Product>.Broadcast(this, product);

        else Event<IProductEditedEventHandler, IRequestHandler, Product>.Broadcast(this, product);

        return this.Redirect(this.Request.CombineUrl("/backend/products"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Product product = this.Storage.GetRepository<IProductRepository>().WithKey(id);

      this.Storage.GetRepository<IProductRepository>().Delete(product);
      this.Storage.Save();
      Event<IProductCreatedEventHandler, IRequestHandler, Product>.Broadcast(this, product);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IProductRepository>().WithCode(code) == null;
    }

    private void CreateOrEditPhotos(Product product, string removedPhotoIds)
    {
      this.CreatePhotos(product);
      this.EditPhotos(product);
      this.RemovePhotos(product, removedPhotoIds);
    }

    // TODO: refactor this method
    private void CreatePhotos(Product product)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("newPhoto"))
        {
          Photo photo = new Photo();

          photo.ProductId = product.Id;
          photo.Filename = this.Request.Form[key];
          photo.IsCover = this.GetPhotoIsCover("_" + key.Replace("Filename", string.Empty) + "IsCover");
          photo.Position = this.GetPhotoPosition("_" + key.Replace("Filename", string.Empty) + "Position");
          this.Storage.GetRepository<IPhotoRepository>().Create(photo);
        }
      }

      this.Storage.Save();
    }

    // TODO: refactor this method
    private void EditPhotos(Product product)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("_photo") && key.EndsWith("IsCover"))
        {
          int photoId = int.Parse(key.Replace("_photo", string.Empty).Replace("IsCover", string.Empty));
          Photo photo = this.Storage.GetRepository<IPhotoRepository>().WithKey(photoId);

          photo.IsCover = this.GetPhotoIsCover("_photo" + photoId + "IsCover");
          photo.Position = this.GetPhotoPosition("_photo" + photoId + "Position");
          this.Storage.GetRepository<IPhotoRepository>().Edit(photo);
        }
      }

      this.Storage.Save();
    }

    private void RemovePhotos(Product product, string removedPhotoIds)
    {
      if (string.IsNullOrEmpty(removedPhotoIds))
        return;

      foreach (string photoIdToRemove in removedPhotoIds.Split(','))
      {
        Photo photo = this.Storage.GetRepository<IPhotoRepository>().WithKey(int.Parse(photoIdToRemove));

        try
        {
          //System.IO.File.Delete();
        }

        catch { }

        this.Storage.GetRepository<IPhotoRepository>().Delete(photo);
      }

      this.Storage.Save();
    }

    private bool GetPhotoIsCover(string key)
    {
      return string.Equals(this.Request.Form[key], true.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    private int? GetPhotoPosition(string key)
    {
      string value = this.Request.Form[key];

      if (string.IsNullOrEmpty(value))
        return null;

      int result = 0;

      if (!int.TryParse(value, out result))
        return null;

      return result;
    }
  }
}