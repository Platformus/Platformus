// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.ECommerce.Backend.ViewModels.Categories;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageCategoriesPermission)]
  public class CategoriesController : Core.Backend.Controllers.ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, Category, CategoryFilter> Repository
    {
      get => this.Storage.GetRepository<int, Category, CategoryFilter>();
    }

    public CategoriesController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync()
    {
      return this.View(IndexViewModelFactory.Create(
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
      return this.View(CreateOrEditViewModelFactory.Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<Category>(c => c.Name.Localizations),
          new Inclusion<Category>(c => c.Description.Localizations),
          new Inclusion<Category>(c => c.Title.Localizations),
          new Inclusion<Category>(c => c.MetaDescription.Localizations),
          new Inclusion<Category>(c => c.MetaKeywords.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]CategoryFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (!await this.IsUrlUniqueAsync(createOrEdit))
        this.ModelState.AddModelError("url", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        Category category = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ?
            new Category() :
            await this.Repository.GetByIdAsync(
              (int)createOrEdit.Id,
              new Inclusion<Category>(c => c.Name.Localizations),
              new Inclusion<Category>(c => c.Description.Localizations),
              new Inclusion<Category>(c => c.Title.Localizations),
              new Inclusion<Category>(c => c.MetaDescription.Localizations),
              new Inclusion<Category>(c => c.MetaKeywords.Localizations)
            ),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(category);

        else this.Repository.Edit(category);

        await this.MergeEntityLocalizationsAsync(category);
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

    private async Task<bool> IsUrlUniqueAsync(CreateOrEditViewModel createOrEdit)
    {
      Category category = (await this.Repository.GetAllAsync(new CategoryFilter(url: createOrEdit.Url))).FirstOrDefault();

      return category == null || category.Id == createOrEdit.Id;
    }
  }
}