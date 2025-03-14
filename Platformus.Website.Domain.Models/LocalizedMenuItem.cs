// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class LocalizedMenuItem : IModel<Data.Entities.LocalizedMenuItem, LocalizedMenuItemFilter>
{
  public MenuItem? MenuItem { get; set; }
  public Culture? Culture { get; set; }
  public string? Name { get; set; }
  
  public LocalizedMenuItem() { }

  public LocalizedMenuItem(Data.Entities.LocalizedMenuItem _localizedMenuItem) : this(_localizedMenuItem, mapMenuItem: true, mapCulture: true) { }

  public LocalizedMenuItem(Data.Entities.LocalizedMenuItem _localizedMenuItem, bool mapMenuItem = true, bool mapCulture = true)
  {
    this.MenuItem = mapMenuItem ? _localizedMenuItem.MenuItem == null ? new MenuItem { Id = _localizedMenuItem.MenuItemId } : new MenuItem(_localizedMenuItem.MenuItem, mapLocalizedMenuItems: false) : null;
    this.Culture = mapCulture ? _localizedMenuItem.Culture == null ? new Culture { Id = _localizedMenuItem.CultureId } : new Culture(_localizedMenuItem.Culture) : null;
    this.Name = _localizedMenuItem.Name;
  }

  public Data.Entities.LocalizedMenuItem ToEntity()
  {
    return new Data.Entities.LocalizedMenuItem
    {
      MenuItemId = this.MenuItem?.Id ?? 0,
      CultureId = this.Culture?.Id,
      Name = this.Name
    };
  }
}