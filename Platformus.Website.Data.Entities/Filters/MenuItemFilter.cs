// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class MenuItemFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public MenuFilter? Menu { get; set; }
  public StringFilter? Name { get; set; }

  public MenuItemFilter() { }

  public MenuItemFilter(IntegerFilter? id = null, MenuFilter? menu = null, StringFilter? name = null)
  {
    this.Id = id;
    this.Menu = menu;
    this.Name = name;
  }
}