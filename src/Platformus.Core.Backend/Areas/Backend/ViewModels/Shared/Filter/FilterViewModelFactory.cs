// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class FilterViewModelFactory : ViewModelFactoryBase
  {
    public FilterViewModel Create(HttpContext httpContext, string filteringProperty)
    {
      return new FilterViewModel()
      {
        FilteringProperty = filteringProperty,
        Value = httpContext.Request.Query[filteringProperty]
      };
    }
  }
}