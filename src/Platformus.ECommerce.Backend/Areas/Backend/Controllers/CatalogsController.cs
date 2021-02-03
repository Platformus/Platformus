// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.Catalogs;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageCatalogsPermission)]
  public class CatalogsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Catalog, CatalogFilter> Repository
    {
      get => this.Storage.GetRepository<int, Catalog, CatalogFilter>();
    }

    public CatalogsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync()
    {
      return this.View(new IndexViewModelFactory().Create(
        await this.Repository.GetAllAsync(
          new CatalogFilter() { Owner = new CatalogFilter { Id = new IntegerFilter() { IsNull = true } } },
          inclusions: new Inclusion<Catalog>[] {
            new Inclusion<Catalog>(c => c.Name.Localizations),
            new Inclusion<Catalog>("Catalogs.Name.Localizations"),
            new Inclusion<Catalog>("Catalogs.Catalogs.Name.Localizations"),
            new Inclusion<Catalog>("Catalogs.Catalogs.Catalogs.Name.Localizations")
          }
        )
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<Catalog>(c => c.Name.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]CatalogFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Catalog catalog = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new Catalog() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(catalog);

        if (createOrEdit.Id == null)
          this.Repository.Create(catalog);

        else this.Repository.Edit(catalog);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<ICatalogCreatedEventHandler, HttpContext, Catalog>.Broadcast(this.HttpContext, catalog);

        else Event<ICatalogEditedEventHandler, HttpContext, Catalog>.Broadcast(this.HttpContext, catalog);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Catalog catalog = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(catalog.Id);
      await this.Storage.SaveAsync();
      Event<ICatalogDeletedEventHandler, HttpContext, Catalog>.Broadcast(this.HttpContext, catalog);
      return this.RedirectToAction("Index");
    }
  }
}