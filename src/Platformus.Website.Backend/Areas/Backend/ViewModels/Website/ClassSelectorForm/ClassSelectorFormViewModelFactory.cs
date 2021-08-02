// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public static class ClassSelectorFormViewModelFactory
  {
    public static ClassSelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Class> classes, int? classId)
    {
      IStringLocalizer<ClassSelectorFormViewModel> localizer = httpContext.GetStringLocalizer<ClassSelectorFormViewModel>();

      return new ClassSelectorFormViewModel()
      {
        GridColumns = new[] {
          GridColumnViewModelFactory.Create(localizer["Parent Class"]),
          GridColumnViewModelFactory.Create(localizer["Name"])
        },
        Classes = classes.Select(ClassViewModelFactory.Create),
        ClassId = classId
      };
    }
  }
}