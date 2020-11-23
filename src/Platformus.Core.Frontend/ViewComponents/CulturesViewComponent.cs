// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Frontend.ViewModels.Shared;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Frontend.ViewComponents
{
  public class CulturesViewComponent : ViewComponentBase
  {
    private ICultureManager cultureManager;

    public CulturesViewComponent(IStorage storage, ICultureManager cultureManager)
      : base(storage)
    {
      this.cultureManager = cultureManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(string partialViewName = null, string additionalCssClass = null)
    {
      return this.View(new CulturesViewModelFactory().Create(
        await this.cultureManager.GetNotNeutralCulturesAsync(),
        partialViewName,
        additionalCssClass
      ));
    }
  }
}