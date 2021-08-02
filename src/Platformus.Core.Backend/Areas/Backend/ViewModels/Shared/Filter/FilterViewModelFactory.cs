// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public static class FilterViewModelFactory
  {
    public static FilterViewModel Create(HttpContext httpContext, string filteringProperty, string title, IEnumerable<Option> options = null)
    {
      return new FilterViewModel()
      {
        FilteringProperty = filteringProperty,
        Title = title,
        Options = options,
        Value = httpContext.Request.Query[filteringProperty]
      };
    }
  }
}