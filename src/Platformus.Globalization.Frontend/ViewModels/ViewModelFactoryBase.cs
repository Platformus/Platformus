// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Frontend.ViewModels
{
  public abstract class ViewModelFactoryBase : Platformus.Barebone.Frontend.ViewModels.ViewModelFactoryBase
  {
    public ViewModelFactoryBase(IHandler handler)
      : base(handler)
    {
      this.handler = handler;
    }

    public string GetLocalizationValue(int dictionaryId)
    {
      return this.GetLocalizationValue(dictionaryId, CultureManager.GetCurrentCulture(this.handler.Storage).Id);
    }

    public string GetLocalizationValue(int dictionaryId, int cultureId)
    {
      Localization localization = this.handler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(
        dictionaryId, cultureId
      );

      if (localization == null)
        return string.Empty;

      return localization.Value;
    }
  }
}