// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Frontend.ViewModels.Shared;

public static class CulturesViewModelFactory
{
  public static CulturesViewModel Create(IEnumerable<Culture> cultures, string partialViewName, string additionalCssClass)
  {
    return new CulturesViewModel()
    {
      Cultures = cultures.Select(CultureViewModelFactory.Create),
      PartialViewName = partialViewName ?? "_Cultures",
      AdditionalCssClass = additionalCssClass
    };
  }
}