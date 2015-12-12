// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Backend.ViewModels
{
  public abstract class ViewModelBuilderBase : Platformus.Barebone.Backend.ViewModels.ViewModelBuilderBase
  {
    public ViewModelBuilderBase(IHandler handler)
      : base(handler)
    {
      this.handler = handler;
    }

    protected IEnumerable<Platformus.Barebone.Backend.Localization> GetLocalizations(Dictionary dictionary = null)
    {
      List<Platformus.Barebone.Backend.Localization> localizations = new List<Platformus.Barebone.Backend.Localization>();

      foreach (Platformus.Globalization.Data.Models.Culture culture in this.handler.Storage.GetRepository<ICultureRepository>().All())
      {
        Platformus.Globalization.Data.Models.Localization localization = null;

        if (dictionary != null)
          localization = this.handler.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(dictionary.Id).FirstOrDefault(l => l.CultureId == culture.Id);

        localizations.Add(
          new Platformus.Barebone.Backend.Localization(
            new Platformus.Barebone.Backend.Culture(culture.Code),
            localization == null ? null : localization.Value
          )
        );
      }

      return localizations;
    }
  }
}