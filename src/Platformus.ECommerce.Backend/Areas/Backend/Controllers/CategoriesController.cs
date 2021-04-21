// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.Categories;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageCategoriesPermission)]
  public class CategoriesController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Category, CategoryFilter> Repository
    {
      get => this.Storage.GetRepository<int, Category, CategoryFilter>();
    }

    public CategoriesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync()
    {
      return this.View(new IndexViewModelFactory().Create(
        await this.Repository.GetAllAsync(
          new CategoryFilter(owner: new CategoryFilter(id: new IntegerFilter(isNull: true))),
          inclusions: new Inclusion<Category>[] {
            new Inclusion<Category>(c => c.Name.Localizations),
            new Inclusion<Category>("Categories.Name.Localizations"),
            new Inclusion<Category>("Categories.Categories.Name.Localizations"),
            new Inclusion<Category>("Categories.Categories.Categories.Name.Localizations")
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
          new Inclusion<Category>(c => c.Name.Localizations),
          new Inclusion<Category>(p => p.Description.Localizations),
          new Inclusion<Category>(p => p.Title.Localizations),
          new Inclusion<Category>(p => p.MetaDescription.Localizations),
          new Inclusion<Category>(p => p.MetaKeywords.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]CategoryFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Category category = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new Category() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(category);

        if (createOrEdit.Id == null)
          this.Repository.Create(category);

        else this.Repository.Edit(category);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<ICategoryCreatedEventHandler, HttpContext, Category>.Broadcast(this.HttpContext, category);

        else Event<ICategoryEditedEventHandler, HttpContext, Category>.Broadcast(this.HttpContext, category);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Category category = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(category.Id);
      await this.Storage.SaveAsync();
      Event<ICategoryDeletedEventHandler, HttpContext, Category>.Broadcast(this.HttpContext, category);
      return this.RedirectToAction("Index");
    }
  }
}