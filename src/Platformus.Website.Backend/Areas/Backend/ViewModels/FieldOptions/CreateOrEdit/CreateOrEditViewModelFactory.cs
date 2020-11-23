// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.FieldOptions
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModel Create(HttpContext httpContext, FieldOption fieldOption)
    {
      if (fieldOption == null)
        return new CreateOrEditViewModel()
        {
          ValueLocalizations = this.GetLocalizations(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = fieldOption.Id,
        ValueLocalizations = this.GetLocalizations(httpContext, fieldOption.Value),
        Position = fieldOption.Position
      };
    }
  }
}