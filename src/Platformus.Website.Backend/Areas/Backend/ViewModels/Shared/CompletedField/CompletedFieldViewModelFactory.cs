// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class CompletedFieldViewModelFactory : ViewModelFactoryBase
  {
    public CompletedFieldViewModel Create(HttpContext httpContext, CompletedField completedField)
    {
      return new CompletedFieldViewModel()
      {
        Id = completedField.Id,
        Field = new FieldViewModelFactory().Create(httpContext, completedField.Field),
        Value = completedField.Value
      };
    }
  }
}