// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.CompletedForms
{
  public class ViewViewModelFactory : ViewModelFactoryBase
  {
    public ViewViewModel Create(HttpContext httpContext, CompletedForm completedForm)
    {
      return new ViewViewModel()
      {
        Id = completedForm.Id,
        CompletedFields = completedForm.CompletedFields.Select(
          cf => new CompletedFieldViewModelFactory().Create(httpContext, cf)
        )
      };
    }
  }
}