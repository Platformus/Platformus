// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone.Frontend.ViewComponents;
using Platformus.Globalization;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Models;
using Platformus.Menus.Frontend.ViewModels.Shared;

namespace Platformus.Menus.Frontend.ViewComponents
{
  public class MenuViewComponent : ViewComponentBase
  {
    public MenuViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(string code)
    {
      return this.HttpContext.RequestServices.GetService<ICache>().GetMenuViewComponentResultWithDefaultValue(
        code, () => this.GetViewComponentResult(code)
      );
    }

    private IViewComponentResult GetViewComponentResult(string code)
    {
      SerializedMenu serializedMenu = this.Storage.GetRepository<ISerializedMenuRepository>().WithCultureIdAndCode(
        CultureManager.GetCurrentCulture(this.Storage).Id, code
      );

      if (serializedMenu == null)
      {
        Menu menu = this.Storage.GetRepository<IMenuRepository>().WithCode(code);

        if (menu == null)
          return null;

        return this.View(new MenuViewModelFactory(this).Create(menu));
      }

      return this.View(new MenuViewModelFactory(this).Create(serializedMenu));
    }
  }
}