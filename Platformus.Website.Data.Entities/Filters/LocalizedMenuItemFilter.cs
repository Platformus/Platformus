// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters;

public class LocalizedMenuItemFilter : IFilter
{
  public MenuItemFilter? MenuItem { get; set; }
  public CultureFilter? Culture { get; set; }
  public StringFilter? Name { get; set; }

  public LocalizedMenuItemFilter() { }

  public LocalizedMenuItemFilter(MenuItemFilter? menuItem = null, CultureFilter? culture = null, StringFilter? name = null)
  {
    this.MenuItem = menuItem;
    this.Culture = culture;
    this.Name = name;
  }
}