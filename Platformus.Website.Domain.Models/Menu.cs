// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Menu : IModel<Data.Entities.Menu, MenuFilter>
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public IEnumerable<MenuItem>? MenuItems { get; set; }

  public Menu() { }

  public Menu(Data.Entities.Menu menu) : this(menu, mapMenuItems: true) { }

  public Menu(Data.Entities.Menu menu, bool mapMenuItems = true)
  {
    this.Id = menu.Id;
    this.Name = menu.Name;
    this.MenuItems = mapMenuItems ? menu.MenuItems?.Select(mi => new MenuItem(mi, mapMenu: false)).ToList() : null;
  }

  public Data.Entities.Menu ToEntity()
  {
    return new Data.Entities.Menu
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}