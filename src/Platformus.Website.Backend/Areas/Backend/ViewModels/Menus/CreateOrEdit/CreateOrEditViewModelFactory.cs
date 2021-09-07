// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Menus
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, Menu menu)
    {
      if (menu == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations()
        };

      return new CreateOrEditViewModel()
      {
        Id = menu.Id,
        Code = menu.Code,
        NameLocalizations = httpContext.GetLocalizations(menu.Name)
      };
    }
  }
}