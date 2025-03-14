// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using Platformus.Core.Api.Dto;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/localized-menu-items")]
[AuthenticatedOnly]
public class LocalizedMenuItem : IDto<Domain.Models.LocalizedMenuItem>
{
  public MenuItem? MenuItem { get; set; }
  public Culture? Culture { get; set; }
  public string? Name { get; set; }

  public LocalizedMenuItem() { }

  public LocalizedMenuItem(Domain.Models.LocalizedMenuItem _localizedMenuItem)
  {
    this.MenuItem = _localizedMenuItem.MenuItem == null ? null : new MenuItem(_localizedMenuItem.MenuItem);
    this.Culture = _localizedMenuItem.Culture == null ? null : new Culture(_localizedMenuItem.Culture);
    this.Name = _localizedMenuItem.Name;
  }

  public Domain.Models.LocalizedMenuItem ToModel()
  {
    return new Domain.Models.LocalizedMenuItem()
    {
      MenuItem = this.MenuItem?.ToModel(),
      Culture = this.Culture?.ToModel(),
      Name = this.Name
    };
  }
}