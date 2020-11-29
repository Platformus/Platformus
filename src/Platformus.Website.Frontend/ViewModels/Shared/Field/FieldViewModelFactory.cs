// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public class FieldViewModelFactory : ViewModelFactoryBase
  {
    public FieldViewModel Create(HttpContext httpContext, Field field)
    {
      return new FieldViewModel()
      {
        Id = field.Id,
        FieldType = new FieldTypeViewModel() { Code = field.FieldType.Code },
        Name = field.Name.GetLocalizationValue(httpContext),
        Code = field.Code,
        IsRequired = field.IsRequired,
        MaxLength = field.MaxLength,
        FieldOptions = field.FieldOptions.Select(
          fo => new FieldOptionViewModelFactory().Create(httpContext, fo)
        )
      };
    }
  }
}