// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Frontend.ViewModels.Shared;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Frontend.ViewComponents
{
  public class CulturesViewComponent : ViewComponent
  {
    private ICultureManager cultureManager;

    public CulturesViewComponent(ICultureManager cultureManager)
    {
      this.cultureManager = cultureManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(string partialViewName = "_Cultures", string additionalCssClass = null)
    {
      return this.View(CulturesViewModelFactory.Create(
        await this.cultureManager.GetNotNeutralCulturesAsync(),
        partialViewName,
        additionalCssClass
      ));
    }
  }
}