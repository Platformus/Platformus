// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.FieldOptions;

public static class CreateOrEditViewModelFactory
{
  public static CreateOrEditViewModel Create(HttpContext httpContext, FieldOption fieldOption)
  {
    if (fieldOption == null)
      return new CreateOrEditViewModel()
      {
        ValueLocalizations = httpContext.GetLocalizations()
      };

    return new CreateOrEditViewModel()
    {
      Id = fieldOption.Id,
      ValueLocalizations = httpContext.GetLocalizations(fieldOption.Value),
      Position = fieldOption.Position
    };
  }
}