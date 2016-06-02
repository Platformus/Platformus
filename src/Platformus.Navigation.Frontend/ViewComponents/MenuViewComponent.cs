// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Frontend.ViewComponents;
using Platformus.Globalization;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;
using Platformus.Navigation.Frontend.ViewModels.Shared;

namespace Platformus.Navigation.Frontend.ViewComponents
{
  public class MenuViewComponent : ViewComponentBase
  {
    public MenuViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(string code)
    {
      CachedMenu cachedMenu = this.Storage.GetRepository<ICachedMenuRepository>().WithCultureIdAndCode(
        CultureProvider.GetCulture(this.Storage).Id, code
      );

      if (cachedMenu == null)
      {
        Menu menu = this.Storage.GetRepository<IMenuRepository>().WithCode(code);

        if (menu == null)
          return null;

        return this.View(new MenuViewModelBuilder(this).Build(menu));
      }

      return this.View(new MenuViewModelBuilder(this).Build(cachedMenu));
    }
  }
}