﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters;

public class FieldOptionFilter : IFilter
{
  public int? Id { get; set; }
  public FieldFilter Field { get; set; }

  [FilterShortcut("Value.Localizations[]")]
  public LocalizationFilter Value { get; set; }

  public FieldOptionFilter() { }

  public FieldOptionFilter(int? id = null, FieldFilter field = null, LocalizationFilter value = null)
  {
    Id = id;
    Field = field;
    Value = value;
  }
}