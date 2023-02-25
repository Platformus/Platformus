// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Shared;

public static class PagerViewModelFactory
{
  public static PagerViewModel Create(HttpContext httpContext, int offset, int limit, int total)
  {
    IStringLocalizer<PagerViewModel> localizer = httpContext.GetStringLocalizer<PagerViewModel>();

    return new PagerViewModel()
    {
      LimitOptions = new Option[] {
        new Option(localizer["By 10"], "10"),
        new Option(localizer["By 25"], "25"),
        new Option(localizer["By 50"], "50"),
        new Option(localizer["By 100"], "100")
      },
      Offset = offset,
      Limit = limit,
      Total = total
    };
  }
}