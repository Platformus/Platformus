// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;
using Platformus.Website.Frontend.ViewModels.Shared;

namespace Platformus.Website.Frontend.ViewComponents;

public class MenuViewComponent : ViewComponent
{
  private IStorage storage;
  private ICache cache;

  public MenuViewComponent(IStorage storage, ICache cache)
  {
    this.storage = storage;
    this.cache = cache;
  }

  public async Task<IViewComponentResult> InvokeAsync(string code, string partialViewName = "_Menu", string additionalCssClass = null)
  {
    Menu menu = await this.cache.GetMenuWithDefaultValue(
      code,
      async () => (await this.storage.GetRepository<int, Menu, MenuFilter>().GetAllAsync(
        new MenuFilter(code: code),
        inclusions: new Inclusion<Menu>[] {
          new Inclusion<Menu>("MenuItems.Name.Localizations"),
          new Inclusion<Menu>("MenuItems.MenuItems.Name.Localizations"),
          new Inclusion<Menu>("MenuItems.MenuItems.MenuItems.Name.Localizations") }
      )).FirstOrDefault()
    );

    if (menu == null)
      return this.Content($"There is no menu with code “{code}” defined.");

    return this.View(MenuViewModelFactory.Create(this.HttpContext, menu, partialViewName, additionalCssClass));
  }
}