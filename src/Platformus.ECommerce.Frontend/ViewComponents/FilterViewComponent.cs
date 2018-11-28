// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Frontend.ViewComponents;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewComponents
{
  public class FilterViewComponent : ViewComponentBase
  {
    public FilterViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(string additionalCssClass)
    {
      return this.GetService<ICache>().GetFilterViewComponentResultWithDefaultValue(
        additionalCssClass, () => this.GetViewComponentResult(additionalCssClass)
      );
    }

    private IViewComponentResult GetViewComponentResult(string additionalCssClass)
    {
      return this.View(new FilterViewModelFactory(this).Create(additionalCssClass));
    }
  }
}