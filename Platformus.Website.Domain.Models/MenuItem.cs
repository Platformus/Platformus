// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class MenuItem : IModel<Data.Entities.MenuItem, MenuItemFilter>
{
  public int Id { get; set; }
  public Menu? Menu { get; set; }
  public IEnumerable<LocalizedMenuItem>? LocalizedMenuItems { get; set; }

  public MenuItem() { }

  public MenuItem(Data.Entities.MenuItem _menuItem) : this(_menuItem, mapMenu: true, mapLocalizedMenuItems: true) { }

  public MenuItem(Data.Entities.MenuItem _menuItem, bool mapMenu = true, bool mapLocalizedMenuItems = true)
  {
    this.Id = _menuItem.Id;
    this.Menu = mapMenu ? _menuItem.Menu == null ? new Menu { Id = _menuItem.MenuId } : new Menu(_menuItem.Menu, mapMenuItems: false) : null;
    this.LocalizedMenuItems = mapLocalizedMenuItems ? _menuItem.LocalizedMenuItems?.Select(lmi => new LocalizedMenuItem(lmi, mapMenuItem: false)).ToList() : null;
  }

  public Data.Entities.MenuItem ToEntity()
  {
    return new Data.Entities.MenuItem
    {
      Id = this.Id,
      MenuId = this.Menu?.Id ?? 0,
      LocalizedMenuItems = this.LocalizedMenuItems?.Select(lmi => lmi.ToEntity()).ToList(),
    };
  }
}