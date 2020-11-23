// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public class FieldOptionViewModelFactory : ViewModelFactoryBase
  {
    public FieldOptionViewModel Create(HttpContext httpContext, FieldOption fieldOption)
    {
      return new FieldOptionViewModel()
      {
        Value = fieldOption.Value.GetLocalizationValue(httpContext)
      };
    }
  }
}