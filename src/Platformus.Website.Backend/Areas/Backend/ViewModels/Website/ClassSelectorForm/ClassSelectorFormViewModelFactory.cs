// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public class ClassSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public ClassSelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Class> classes, int? classId)
    {
      IStringLocalizer<ClassSelectorFormViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<ClassSelectorFormViewModelFactory>>();

      return new ClassSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory().Create(localizer["Parent Class"]),
          new GridColumnViewModelFactory().Create(localizer["Name"])
        },
        Classes = classes.Select(c => new ClassViewModelFactory().Create(c)),
        ClassId = classId
      };
    }
  }
}