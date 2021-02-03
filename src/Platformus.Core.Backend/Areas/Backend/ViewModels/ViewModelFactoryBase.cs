// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;

namespace Platformus.Core.Backend.ViewModels
{
  public abstract class ViewModelFactoryBase : Platformus.Core.ViewModels.ViewModelFactoryBase
  {
    protected IEnumerable<Primitives.Localization> GetLocalizations(HttpContext httpContext, Dictionary dictionary = null)
    {
      List<Primitives.Localization> localizations = new List<Primitives.Localization>();

      foreach (Culture culture in httpContext.GetCultureManager().GetCulturesAsync().Result)
      {
        Primitives.Localization localization;

        if (dictionary == null)
          localization = new Primitives.Localization(
            new Primitives.Culture(culture.Id)
          );

        else localization = new Primitives.Localization(
          new Primitives.Culture(culture.Id),
          dictionary.Localizations.FirstOrDefault(l => l.CultureId == culture.Id)?.Value
        );

        localizations.Add(localization);
      }

      return localizations;
    }
  }
}