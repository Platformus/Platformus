// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public static class ClassSelectorFormViewModelFactory
  {
    public static ClassSelectorFormViewModel Create(IEnumerable<Class> classes, int? classId)
    {
      return new ClassSelectorFormViewModel()
      {
        Classes = classes.Select(ClassViewModelFactory.Create).ToList(),
        ClassId = classId
      };
    }
  }
}