// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public class FormViewModelFactory : ViewModelFactoryBase
  {
    public FormViewModel Create(HttpContext httpContext, Form form, string partialViewName, string additionalCssClass)
    {
      return new FormViewModel()
      {
        Id = form.Id,
        Name = form.Name.GetLocalizationValue(httpContext),
        SubmitButtonTitle = form.SubmitButtonTitle.GetLocalizationValue(httpContext),
        Fields = form.Fields.Select(
          f => new FieldViewModelFactory().Create(httpContext, f)
        ).ToList(),
        PartialViewName = partialViewName ?? "_Form",
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}