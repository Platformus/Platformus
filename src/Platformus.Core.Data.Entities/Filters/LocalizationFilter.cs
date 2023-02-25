// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class LocalizationFilter : IFilter
{
  public int? Id { get; set; }
  public DictionaryFilter Dictionary { get; set; }
  public CultureFilter Culture { get; set; }
  public StringFilter Value { get; set; }

  public LocalizationFilter() { }

  public LocalizationFilter(int? id = null, DictionaryFilter dictionary = null, CultureFilter culture = null, StringFilter value = null)
  {
    Id = id;
    Dictionary = dictionary;
    Culture = culture;
    Value = value;
  }
}