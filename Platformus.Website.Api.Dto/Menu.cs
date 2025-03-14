// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/menus")]
[AuthenticatedOnly]
public class Menu : IDto<Domain.Models.Menu>
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public IEnumerable<MenuItem>? MenuItems { get; set; }

  public Menu() { }

  public Menu(Domain.Models.Menu _menu)
  {
    this.Id = _menu.Id;
    this.Name = _menu.Name;
    this.MenuItems = _menu.MenuItems?.Select(mi => new MenuItem(mi)).ToList();
  }

  public Domain.Models.Menu ToModel()
  {
    return new Domain.Models.Menu()
    {
      Id = Id,
      Name = Name
    };
  }
}
