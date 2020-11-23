// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class TakeSelectorViewModelFactory : ViewModelFactoryBase
  {
    public TakeSelectorViewModel Create(HttpContext httpContext, int take)
    {
      IStringLocalizer<TakeSelectorViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<TakeSelectorViewModelFactory>>();

      return new TakeSelectorViewModel()
      {
        Take = take,
        TakeOptions = new Option[] {
          new Option(localizer["By 10"], "10"),
          new Option(localizer["By 25"], "25"),
          new Option(localizer["By 50"], "50"),
          new Option(localizer["By 100"], "100")
        }
      };
    }
  }
}