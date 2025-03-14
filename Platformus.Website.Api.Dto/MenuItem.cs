// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/menu-items")]
[AuthenticatedOnly]
public class MenuItem : IDto<Domain.Models.MenuItem>
{
  public int Id { get; set; }
  public Menu? Menu { get; set; }
  public IEnumerable<LocalizedMenuItem>? LocalizedMenuItems { get; set; }

  public MenuItem() { }

  public MenuItem(Domain.Models.MenuItem _menuItem)
  {
    this.Id = _menuItem.Id;
    this.Menu = _menuItem.Menu == null ? null : new Menu(_menuItem.Menu);
    this.LocalizedMenuItems = _menuItem.LocalizedMenuItems?.Select(lmi => new LocalizedMenuItem(lmi)).ToList();
  }

  public Domain.Models.MenuItem ToModel()
  {
    return new Domain.Models.MenuItem()
    {
      Id = this.Id,
      Menu = this.Menu?.ToModel(),
      LocalizedMenuItems = this.LocalizedMenuItems?.Select(lmi => lmi.ToModel()),
    };
  }

  public LocalizedMenuItem Localized()
  {
    return this.Localized(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
  }

  public LocalizedMenuItem Localized(string cultureId)
  {
    return this.LocalizedMenuItems?.FirstOrDefault(lmi => lmi.Culture!.Id == cultureId) ?? new LocalizedMenuItem();
  }
}