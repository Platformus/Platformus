// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Frontend.ViewComponents;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewComponents
{
  public class CartViewComponent : ViewComponentBase
  {
    public CartViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(string code, string additionalCssClass)
    {
      return this.GetService<ICache>().GetCartViewComponentResultWithDefaultValue(
        additionalCssClass, () => this.GetViewComponentResult(additionalCssClass)
      );
    }

    private IViewComponentResult GetViewComponentResult(string additionalCssClass)
    {
      return this.View(new CartViewModelFactory(this).Create(additionalCssClass));
    }
  }
}