// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class DictionaryFilter : IFilter
  {
    public int? Id { get; set; }
    public CultureFilter Culture { get; set; }

    [FilterShortcut("Localizations[]")]
    public LocalizationFilter Localization { get; set; }

    public DictionaryFilter() { }

    public DictionaryFilter(int? id = null, CultureFilter culture = null, LocalizationFilter localization = null)
    {
      Id = id;
      Culture = culture;
      Localization = localization;
    }
  }
}