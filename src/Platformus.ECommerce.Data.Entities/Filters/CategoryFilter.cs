// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.ECommerce.Filters;

public class CategoryFilter : IFilter
{
  public IntegerFilter Id { get; set; }
  public CategoryFilter Owner { get; set; }
  public string Url { get; set; }

  [FilterShortcut("Name.Localizations[]")]
  public LocalizationFilter Name { get; set; }

  [FilterShortcut("Description.Localizations[]")]
  public LocalizationFilter Description { get; set; }

  public CategoryFilter() { }

  public CategoryFilter(IntegerFilter id = null, CategoryFilter owner = null, string url = null, LocalizationFilter name = null)
  {
    Id = id;
    Owner = owner;
    Url = url;
    Name = name;
  }
}