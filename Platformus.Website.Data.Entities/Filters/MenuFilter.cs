// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class MenuFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public StringFilter? Name { get; set; }
  public EnumerableFilter<MenuItemFilter>? MenuItems { get; set; }

  public MenuFilter() { }

  public MenuFilter(IntegerFilter? id = null, StringFilter? name = null, EnumerableFilter<MenuItemFilter>? menuItems = null)
  {
    this.Id = id;
    this.Name = name;
    this.MenuItems = menuItems;
  }
}