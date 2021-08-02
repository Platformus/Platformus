// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public static class TakeSelectorViewModelFactory
  {
    public static TakeSelectorViewModel Create(HttpContext httpContext, int take)
    {
      IStringLocalizer<TakeSelectorViewModel> localizer = httpContext.GetStringLocalizer<TakeSelectorViewModel>();

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