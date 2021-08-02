// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.MenuItems
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, MenuItem menuItem)
    {
      if (menuItem == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations()
        };

      return new CreateOrEditViewModel()
      {
        Id = menuItem.Id,
        NameLocalizations = httpContext.GetLocalizations(menuItem.Name),
        Url = menuItem.Url,
        Position = menuItem.Position
      };
    }
  }
}