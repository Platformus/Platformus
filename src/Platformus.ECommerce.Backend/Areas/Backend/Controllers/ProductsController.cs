// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

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
      Event<IProductDeletedEventHandler, IRequestHandler, Product>.Broadcast(this, product);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IProductRepository>().WithCode(code) == null;
    }
  }
}